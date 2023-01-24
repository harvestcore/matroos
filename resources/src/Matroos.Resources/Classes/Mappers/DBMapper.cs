using System.Runtime.Serialization;

using AppContext.Services.Interfaces;

using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Classes.Commands;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Matroos.Resources.Classes.Mappers;

/// <summary>
/// Database mapper for List<UserCommand> objects.
/// 
/// In the Bot collection (DB) the user commands are stored as a list of identifiers (UUID strings). Those identifiers are mapped
/// to UserCommand objects with this serializer.
/// 
/// This serializer also does the opposite conversion, from UserCommand to UUID.
/// </summary>
public class CustomSerializer : SerializerBase<List<UserCommand>>, IBsonArraySerializer
{
    /// <summary>
    /// The <see cref="IDataContextService"/>.
    /// </summary>
    private readonly IDataContextService _dataContextService;

    /// <summary>
    /// The <see cref="IBsonSerializer"/> for <see cref="UserCommand"/> objects.
    /// </summary>
    private static readonly IBsonSerializer<UserCommand> itemSerializer = BsonSerializer.LookupSerializer<UserCommand>();

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="dataContextService">The <see cref="IDataContextService"/>.</param>
    public CustomSerializer(IDataContextService dataContextService)
    {
        _dataContextService = dataContextService;
    }

    /// <summary>
    /// Serializes a list of user commands to a list of identifiers (strings).
    /// </summary>
    /// <param name="ctx">The context.</param>
    /// <param name="args">The extra arguments.</param>
    /// <param name="list">The list to be serialized.</param>
    public override void Serialize(BsonSerializationContext ctx, BsonSerializationArgs args, List<UserCommand> list)
    {
        // Start writing the array.
        ctx.Writer.WriteStartArray();

        if (list is null)
        {
            // No items. Stop writing the array.
            ctx.Writer.WriteEndArray();
            return;
        }

        // Iterate the commands and add its identifiers to the array.
        foreach (UserCommand? item in list)
        {
            ctx.Writer.WriteString(item.Id.ToString());
        }

        // Stop writing the array.
        ctx.Writer.WriteEndArray();
    }

    /// <summary>
    /// Deserializes the user commands, from identifiers (UUID strings) to UserCommand objects.
    /// </summary>
    /// <param name="ctx">The context.</param>
    /// <param name="args">The extra arguments.</param>
    /// <returns>A list containing all the serialized user commands.</returns>
    public override List<UserCommand> Deserialize(BsonDeserializationContext ctx, BsonDeserializationArgs args)
    {
        switch (ctx.Reader.CurrentBsonType)
        {
            case BsonType.Array:
                List<UserCommand>? list = new();

                // Start reading the array containing UUID strings.
                ctx.Reader.ReadStartArray();

                // Iterate while existent.
                while (ctx.Reader.ReadBsonType() != BsonType.EndOfDocument)
                {
                    // Parse the identifier and get the user command from DB.
                    string id = ctx.Reader.ReadString();
                    Task<UserCommand?>? r = _dataContextService.Get<UserCommand>(Guid.Parse(id));
                    r.Wait();

                    // Add the user command if existent.
                    UserCommand? uc = r.Result;
                    if (uc != null)
                    {
                        list.Add(uc);
                    }
                }

                // Stop reading the array.
                ctx.Reader.ReadEndArray();
                return list;

            default:
                throw new SerializationException("Cannot deserialize!");
        }
    }

    public bool TryGetItemSerializationInfo(out BsonSerializationInfo serializationInfo)
    {
        serializationInfo = new BsonSerializationInfo(null, itemSerializer, itemSerializer.ValueType);
        return true;
    }
}

/// <summary>
/// Database mapper for Bot objects.
/// </summary>
public static class DBMapper
{
    public static void RegisterClassMappers(IDataContextService dataContextService)
    {
        // Register a mapper for the Bot class.
        BsonClassMap.RegisterClassMap<Bot>(cm =>
        {
            // Default mapping.
            cm.AutoMap();

            // Ignore extra elements if existent.
            cm.SetIgnoreExtraElements(true);

            // User commands are mapped using the CustomSerializer.
            cm.MapMember(b => b.UserCommands).SetSerializer(new CustomSerializer(dataContextService));
        });
    }
}
