namespace Matroos.Resources.Classes.Commands;

public class BaseCommand
{
    /// <summary>
    /// The allowed execution modes for this command.
    /// </summary>
    public List<CommandMode> AllowedModes { get; protected set; }

    /// <summary>
    /// The command type.
    /// </summary>
    public CommandType CommandType { get; }

    /// <summary>
    /// Whether this command needs a prefix to be executed or not.
    /// </summary>
    public bool NeedsPrefix { get; }

    /// <summary>
    /// The parameters signature.
    /// </summary>
    public List<ParameterSignature> Parameters { get; protected set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="commandType">The <see cref="CommandType"/>.</param>
    /// <param name="needsPrefix">Whether the command needs a prefix to be executed or not.</param>
    public BaseCommand(CommandType commandType, bool needsPrefix)
    {
        CommandType = commandType;
        NeedsPrefix = needsPrefix;
        AllowedModes = new();
        Parameters = new();
    }
}
