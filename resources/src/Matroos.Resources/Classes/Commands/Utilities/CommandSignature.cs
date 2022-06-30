namespace Matroos.Resources.Classes.Commands.Utilities;

public class CommandSignature
{
    /// <summary>
    /// The command type.
    /// </summary>
    public CommandType CommandType { get; }

    /// <summary>
    /// The parameter signature.
    /// </summary>
    public List<ParameterSignature>? Signature { get; }

    /// <summary>
    /// The allowed command modes.
    /// </summary>
    public List<CommandMode>? AllowedModes { get; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="type">The command type.</param>
    /// <param name="signature">The parameter signature.</param>
    /// <param name="allowedModes">The allowed command modes.</param>
    public CommandSignature(CommandType commandType, List<ParameterSignature> signature, List<CommandMode> allowedModes)
    {
        CommandType = commandType;
        Signature = signature;
        AllowedModes = allowedModes;
    }
}
