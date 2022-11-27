using System.Text.Json;
using System.Text.Json.Serialization;

using AppContext.Services.Interfaces;

using Matroos.Resources.Classes.Commands;

namespace Matroos.Resources.Classes.Mappers;

/// <summary>
/// JSON mapper for List<UserCommand> objects.
/// 
/// Used internally by ASP.NET when mapping data in the controllers.
/// </summary>
public class JsonMapper : JsonConverter<List<UserCommand>>
{
    /// <summary>
    /// The <see cref="IDataContextService"/>.
    /// </summary>
    private readonly IDataContextService _dataContextService;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="dataContextService">The <see cref="IDataContextService"/>.</param>
    public JsonMapper(IDataContextService dataContextService)
    {
        _dataContextService = dataContextService ?? throw new ArgumentException("Missing DataContextService.");
    }

    /// <summary>
    /// Parses a list of identifiers (UUID strings) to a list of user commands.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">Extra options</param>
    /// <returns>A list containing the parsed user commands.</returns>
    public override List<UserCommand>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        List<UserCommand> output = new();
        int startingDepth = reader.CurrentDepth;

        // While there are existent elements.
        while (reader.Read() && reader.CurrentDepth > startingDepth)
        {
            // Get the identifier.
            string? value = reader.GetString();
            if (value == null)
            {
                continue;
            }

            // Parse it and get the user command.
            Guid id = Guid.Parse(value);
            Task<UserCommand?>? r = _dataContextService.Get<UserCommand>(id);
            r.Wait();

            // Add the user command if existent.
            UserCommand? found = r.Result;
            if (found != null)
            {
                output.Add(found);
            }
        }

        return output;
    }

    /// <summary>
    /// Parses a list of user commands to a list of idenfiers (UUID strings).
    /// </summary>
    /// <param name="writer">The writer.</param>
    /// <param name="commands">The elements to be parsed.</param>
    /// <param name="options">Extra options.</param>
    public override void Write(Utf8JsonWriter writer, List<UserCommand> commands, JsonSerializerOptions options)
    {
        string? output = JsonSerializer.Serialize(commands, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        writer.WriteRawValue(output);
    }
}
