using System.Reflection;

using Matroos.Resources.Attributes;
using Matroos.Resources.Classes.Commands;

namespace Matroos.Resources.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Get the desired attribute from an <see cref="Enum"/> value.
    /// </summary>
    /// <typeparam name="TType">The type of the attribute.</typeparam>
    /// <param name="value">The <see cref="Enum"/> value.</param>
    /// <returns>The attribute found.</returns>
    public static TType GetAttribute<TType>(this Enum value)
    {
        Type type = value.GetType();
        MemberInfo[] memInfo = type.GetMember(value.ToString());
        object[] attributes = memInfo[0].GetCustomAttributes(typeof(TType), false);

        if (attributes.Length == 0)
        {
            throw new CustomAttributeFormatException("Missing attribute.");
        }

        return (TType)attributes[0];
    }

    /// <summary>
    /// Get the allowed execution modes of a given <see cref="CommandType"/>.
    /// </summary>
    /// <param name="value">The <see cref="CommandType"/>.</param>
    /// <returns>A list containing all the allowed execution modes.</returns>
    public static List<CommandMode> GetAllowedCommandModes(this CommandType value)
    {
        CommandAttribute commandAttribute = value.GetAttribute<CommandAttribute>();

        if (commandAttribute == null)
        {
            return new();
        }

        // Generate an instance.
        object? generatedInstance = Activator.CreateInstance(commandAttribute.Command);

        // Get the property.
        PropertyInfo? property = commandAttribute.Command.GetProperty("AllowedModes");

        // Get the allowed modes.
        List<CommandMode>? allowedModes = property?.GetValue(generatedInstance) as List<CommandMode>;

        return allowedModes ?? new();
    }

    /// <summary>
    /// Get the allowed properties of a given <see cref="CommandType"/>.
    /// </summary>
    /// <param name="value">The <see cref="CommandType"/>.</param>
    /// <returns>A list containing all the allowed properties.</returns>
    public static bool ValidateParameters(this CommandType value, Dictionary<string, object> parameters)
    {
        CommandAttribute commandAttribute = value.GetAttribute<CommandAttribute>();
        if (commandAttribute == null || commandAttribute.Command == null)
        {
            return false;
        }

        // Generate an instance from the command type.
        object? generatedInstance = Activator.CreateInstance(commandAttribute.Command);

        // Invoke the "Run" method to run the command.
        MethodInfo? method = commandAttribute.Command.GetMethod("ValidateParameters");

        if (method == null)
        {
            return false;
        }

        method.Invoke(generatedInstance, new object[] { parameters });

        return true;
    }
}
