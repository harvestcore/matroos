using System.ComponentModel.DataAnnotations;
using System.Reflection;

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
    /// Throws a validation exception.
    /// </summary>
    /// <param name="name">The name of the parameter that is not valid.</param>
    private static void Throw(string name)
    {
        throw new ValidationException($"Validation failed. The parameter '{name}' is not valid.");
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

            Type parameterType = parameter.DataType.GetAttribute<ATypeAttribute>().Type;
            MethodInfo? method = typeof(ObjectExtensions).GetMethod("GetValue");
            MethodInfo? genericMethod = method?.MakeGenericMethod(parameterType);
            object? value = genericMethod?.Invoke(this, new object[] { providedParam });

            if (parameter.Required)
            {
                if (providedParam == null || parameterType != value?.GetType() || !parameter.Validator(value))
                {
                    Throw(parameter.Name);
                }
            }
            else
            {
                if (providedParam != null && (parameterType != value?.GetType() || !parameter.Validator(value)))
                {
                    Throw(parameter.Name);
                }
            }
        }
    }
}
