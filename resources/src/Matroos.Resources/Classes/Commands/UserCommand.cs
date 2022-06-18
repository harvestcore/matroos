using Matroos.Resources.Extensions;

namespace Matroos.Resources.Classes.Commands;

public class UserCommand
{
    /// <summary>
    /// Comand identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Command friendly name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Command description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The command type.
    /// </summary>
    public CommandType Type { get; set; }

    /// <summary>
    /// The trigger used to run the command.
    /// </summary>
    public string Trigger { get; set; }

    /// <summary>
    /// The command execution mode.
    /// </summary>
    public CommandMode Mode { get; set; }

    /// <summary>
    /// Command particular parameters.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; }

    /// <summary>
    /// The creation time of the bot.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The update time of the bot.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

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

        foreach (KeyValuePair<string, object> param in parameters)
        {
            if (Parameters.ContainsKey(param.Key) && param.Value != null)
            {
                Parameters[param.Key] = param.Value;
            }
            else if (param.Value != null)
            {
                Parameters.Add(param.Key, param.Value);
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
