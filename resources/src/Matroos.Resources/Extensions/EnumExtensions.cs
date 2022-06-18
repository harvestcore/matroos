using System.Reflection;

using Matroos.Resources.Attributes;
using Matroos.Resources.Classes.Commands;

namespace Matroos.Resources.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Get the <see cref="CommandAttribute"/> attribute from an <see cref="Enum"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Enum"/> value.</param>
    /// <returns>The <see cref="CommandAttribute"/>.</returns>
    public static CommandAttribute GetCommandAttribute(this Enum value)
    {
        Type type = value.GetType();
        MemberInfo[] memInfo = type.GetMember(value.ToString());
        object[] attributes = memInfo[0].GetCustomAttributes(typeof(CommandAttribute), false);

        if (attributes.Length == 0)
        {
            throw new CustomAttributeFormatException("Missing 'Command' attribute value.");
        }

        return (CommandAttribute)attributes[0];
    }

    /// <summary>
    /// Get the allowed execution modes of a given <see cref="CommandType"/>.
    /// </summary>
    /// <param name="value">The <see cref="CommandType"/>.</param>
    /// <returns>A list containing all the allowed execution modes.</returns>
    public static List<CommandMode> GetAllowedCommandModes(this CommandType value)
    {
        CommandAttribute commandAttribute = value.GetCommandAttribute();

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
}
