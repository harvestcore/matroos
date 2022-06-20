namespace Matroos.Resources.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class ATypeAttribute : Attribute
{
    /// <summary>
    /// The type.
    /// </summary>
    public Type Type { get; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="type">The type.</param>
    public ATypeAttribute(Type type)
    {
        Type = type;
    }
}
