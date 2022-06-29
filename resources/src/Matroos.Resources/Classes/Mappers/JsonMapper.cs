using System.Text.Json;
using System.Text.Json.Serialization;

using Matroos.Resources.Classes.Commands;
using Matroos.Resources.Services.Interfaces;

namespace Matroos.Resources.Classes.Mappers;

public class JsonMapper : JsonConverter<List<UserCommand>>
{
    private readonly IDataContextService _dataContextService;

    public JsonMapper(IDataContextService? dataContextService)
    {
        _dataContextService = dataContextService ?? throw new ArgumentException("Missing DataContextService.");
    }

    public override List<UserCommand>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        List<UserCommand> output = new();
        int startingDepth = reader.CurrentDepth;
        while (reader.Read() && reader.CurrentDepth > startingDepth)
        {
            string? value = reader.GetString();
            if (value == null)
            {
                continue;
            }

            Guid id = Guid.Parse(value);
            Task<UserCommand?>? r = _dataContextService.Get<UserCommand>(id);
            r.Wait();

            UserCommand? found = r.Result;
            if (found == null)
            {
                continue;
            }

            output.Add(found);
        }

        return output;
    }

    public override void Write(Utf8JsonWriter writer, List<UserCommand> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (UserCommand? command in value)
        {
            writer.WriteStringValue(command.Id.ToString());
        }
        writer.WriteEndArray();
    }
}
