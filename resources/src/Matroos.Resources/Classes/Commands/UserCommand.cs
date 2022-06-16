namespace Matroos.Resources.Classes.Commands;

public class UserCommand
{
    /// <summary>
    /// Comand identifier.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Command friendly name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Command description.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// The command type.
    /// </summary>
    public CommandType Type { get; }

    /// <summary>
    /// The trigger used to run the command.
    /// </summary>
    public string Trigger { get; }

    /// <summary>
    /// The command execution mode.
    /// </summary>
    public CommandMode Mode { get; }

    /// <summary>
    /// Command particular parameters.
    /// </summary>
    public Dictionary<string, object> Parameters { get; }

    /// <summary>
    /// The creation time of the bot.
    /// </summary>
    public DateTime CreatedAt { get; }

    /// <summary>
    /// The update time of the bot.
    /// </summary>
    public DateTime UpdatedAt { get; }

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
        Type = commandType;
        Mode = commandMode;
        Parameters = new(parameters);
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}
