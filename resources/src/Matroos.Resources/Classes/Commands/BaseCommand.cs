using System.ComponentModel.DataAnnotations;

using Discord.WebSocket;

using Matroos.Resources.Attributes;
using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Extensions;

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

    /// <summary>
    /// Check if the prefix is valid.
    /// </summary>
    /// <param name="message">The message where to check the prefix.</param>
    /// <param name="bot">The bot that is running the command.</param>
    /// <returns>Whether the prefix is valid or not.</returns>
    protected bool ValidPrefix(SocketMessage message, Bot bot)
    {
        if (!NeedsPrefix || message == null)
        {
            return true;
        }

        return NeedsPrefix && message.Content.StartsWith(bot.Prefix);
    }

    /// <summary>
    /// Validate the given parameters.
    /// </summary>
    /// <param name="parameters">The parameters to be validated.</param>
    /// <returns>Whether the validation is successful or not.</returns>
    public void ValidateParameters(Dictionary<string, object> parameters)
    {
        foreach (ParameterSignature parameter in Parameters)
        {
            parameters.TryGetValue(parameter.Name, out object? providedParam);

            if (
                (providedParam == null && parameter.Required) ||
                parameter.Type.GetAttribute<ATypeAttribute>().Type != providedParam?.GetType() ||
                !parameter.Validator(providedParam)
            )
            {
                throw new ValidationException($"Validation failed. The parameter '{parameter.Name}' is not valid.");
            }
        }

        foreach (KeyValuePair<string, object> parameter in parameters)
        {
            ParameterSignature? signature = Parameters.Find(item => item.Name.Equals(parameter.Key));

            if (signature == null || !signature.Validator(parameter.Value) || parameter.Value.GetType() != signature.Type.GetAttribute<ATypeAttribute>().Type)
            {
                throw new ValidationException($"Validation failed. The parameter '{parameter.Key}' is not valid.");
            }
        }
    }
}
