using System.Runtime.Serialization;

using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Classes.Commands;
using Matroos.Resources.Services.Interfaces;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Matroos.Resources.Classes.Mappers;

public class CustomSerializer : SerializerBase<List<UserCommand>>, IBsonArraySerializer
{
    private readonly IDataContextService _dataContextService;
    private static readonly IBsonSerializer<UserCommand> itemSerializer = BsonSerializer.LookupSerializer<UserCommand>();

    public CustomSerializer(IDataContextService dataContextService)
    {
        _dataContextService = dataContextService;
    }

    public override void Serialize(BsonSerializationContext ctx, BsonSerializationArgs args, List<UserCommand> list)
    {
        ctx.Writer.WriteStartArray();

        if (list is null)
        {
            ctx.Writer.WriteEndArray();
            return;
        }

        foreach (UserCommand? item in list)
        {
            ctx.Writer.WriteString(item.Id.ToString());
        }

        ctx.Writer.WriteEndArray();
    }

    public override List<UserCommand> Deserialize(BsonDeserializationContext ctx, BsonDeserializationArgs args)
    {
        switch (ctx.Reader.CurrentBsonType)
        {
            case BsonType.Array:
                List<UserCommand>? list = new();
                ctx.Reader.ReadStartArray();
                while (ctx.Reader.ReadBsonType() != BsonType.EndOfDocument)
                {
                    string id = ctx.Reader.ReadString();
                    Task<UserCommand?>? r = _dataContextService.Get<UserCommand>(Guid.Parse(id));
                    r.Wait();

                    UserCommand? uc = r.Result;
                    if (uc != null)
                    {
                        list.Add(uc);
                    }
                }
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

public static class DBMapper
{
    public static void RegisterClassMappers(IDataContextService dataContextService)
    {
        BsonClassMap.RegisterClassMap<Bot>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
            cm.MapMember(b => b.UserCommands).SetSerializer(new CustomSerializer(dataContextService));
        });
    }
}
