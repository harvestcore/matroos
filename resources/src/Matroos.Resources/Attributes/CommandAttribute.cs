using Matroos.Resources.Classes.Commands;

namespace Matroos.Resources.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class CommandAttribute : Attribute
{
    /// <summary>
    /// The command type.
    /// </summary>
    public Type Command { get; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="command">The command type.</param>
    public CommandAttribute(Type command)
    {
        if (!command.IsSubclassOf(typeof(BaseCommand)))
        {
            throw new ArgumentException("The given type does not inherit from the 'BaseCommand' class.");
        }

        Command = command;
    }
}
