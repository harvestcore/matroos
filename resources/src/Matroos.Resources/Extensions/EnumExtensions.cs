using System.Reflection;

using Matroos.Resources.Attributes;

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
}
