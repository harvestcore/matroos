using System.Reflection;

using Matroos.Resources.Attributes;
using Matroos.Resources.Extensions;
using Matroos.Resources.Interfaces;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Matroos.Resources.Classes.Commands;

public class UserCommand : IBaseItem
{
    /// <inheritdoc />
    public static string CollectionName { get; } = "commands";

    /// <summary>
    /// Comand identifier.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    /// <summary>
    /// Command friendly name.
    /// </summary>
    [BsonElement("name")]
    public string Name { get; set; }

    /// <summary>
    /// Command description.
    /// </summary>
    [BsonElement("description")]
    public string Description { get; set; }

    /// <summary>
    /// The command type.
    /// </summary>
    [BsonElement("type")]
    public CommandType Type { get; set; }

    /// <summary>
    /// The trigger used to run the command.
    /// </summary>
    [BsonElement("trigger")]
    public string Trigger { get; set; }

    /// <summary>
    /// The command execution mode.
    /// </summary>
    [BsonElement("mode")]
    public CommandMode Mode { get; set; }

    /// <summary>
    /// Command particular parameters.
    /// </summary>
    [BsonElement("parameters")]
    public Dictionary<string, object> Parameters { get; set; }

    /// <summary>
    /// The creation time of the bot.
    /// </summary>
    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The update time of the bot.
    /// </summary>
    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public UserCommand()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Description = string.Empty;
        Trigger = string.Empty;
        Parameters = new();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="name">The command friendly name.</param>
    /// <param name="description">The command description.</param>
    /// <param name="trigger">The trigger used to run the command.</param>
    /// <param name="parameters">Command particular parameters.</param>
    /// <param name="commandType">The command type.</param>
    /// <param name="commandMode">The command execution mode.</param>
    public UserCommand(string name, string description, string trigger, Dictionary<string, object> parameters, CommandType commandType, CommandMode commandMode)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentException("The UserCommand name must not be empty.");
        Description = description;
        Trigger = trigger ?? throw new ArgumentException("The UserCommand trigger must not be empty.");

        if (!commandType.GetAllowedCommandModes().Contains(commandMode))
        {
            throw new ArgumentException("The execution mode (CommandMode) is not allowed.");
        }

        Type = commandType;
        Mode = commandMode;
        Parameters = new();
        UpdateParameters(parameters);
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    /// <summary>
    /// Update the command parameters.
    /// </summary>
    /// <param name="parameters">The new parameters.</param>
    private void UpdateParameters(Dictionary<string, object> parameters)
    {
        if (parameters == null)
        {
            return;
        }

        // This will throw if the parameters are not correct.
        if (!Type.ValidateParameters(parameters))
        {
            return;
        }

        List<ParameterSignature> parameterSignatures = Type.GetParametersSignature();
        foreach (KeyValuePair<string, object> param in parameters)
        {
            ParameterSignature? signature = parameterSignatures.Find(item =>
                item.Name.ToLower().Equals(param.Key.ToLower())
            );

            Type? parameterType = signature?.DataType.GetAttribute<ATypeAttribute>().Type;
            MethodInfo? method = typeof(ObjectExtensions).GetMethod("GetValue");
            MethodInfo? genericMethod = method?.MakeGenericMethod(parameterType ?? typeof(object));
            object? value = genericMethod?.Invoke(this, new object[] { param.Value });

            if (Parameters.ContainsKey(param.Key) && value != null)
            {
                Parameters[param.Key] = value;
            }
            else if (value != null)
            {
                Parameters.Add(param.Key, value);
            }
        }
    }

    /// <summary>
    /// Update the properties.
    /// </summary>
    /// <param name="userCommand">An object containing the new properties.</param>
    public void Update(UserCommand userCommand)
    {
        if (!string.IsNullOrEmpty(userCommand.Name))
        {
            Name = userCommand.Name;
        }

        if (!string.IsNullOrEmpty(userCommand.Description))
        {
            Description = userCommand.Description;
        }

        if (!string.IsNullOrEmpty(userCommand.Trigger))
        {
            Trigger = userCommand.Trigger;
        }

        UpdateParameters(userCommand.Parameters);
        UpdatedAt = DateTime.Now;
    }
}
