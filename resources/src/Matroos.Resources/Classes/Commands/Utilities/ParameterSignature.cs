using System.Runtime.Serialization;
using System.Text.Json.Serialization;

using Matroos.Resources.Utilities;

namespace Matroos.Resources.Classes.Commands;

public class ParameterSignature
{
    /// <summary>
    /// The name of the parameter.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The display name of the parameter.
    /// </summary>
    public string DisplayName { get; }

    /// <summary>
    /// Whether the parameter is required or not.
    /// </summary>
    public bool Required { get; }

    /// <summary>
    /// The data type of the parameter.
    /// </summary>
    public DataType DataType { get; }

    /// <summary>
    /// The default value of the parameter.
    /// </summary>
    public object Default { get; }

    /// <summary>
    /// Validation function.
    /// </summary>
    [JsonIgnore]
    [IgnoreDataMember]
    public Func<object, bool> Validator { get; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="displayName">The display name of the parameter.</param>
    /// <param name="required">Whether the parameter is required or not.</param>
    /// <param name="type">The data type of the parameter.</param>
    /// <param name="default">The default value of the parameter.</param>
    /// <param name="validator">The validation function.</param>
    public ParameterSignature(string name, string displayName, bool required, DataType dataType, object @default, Func<object, bool> validator)
    {
        Name = name ?? throw new ArgumentException("The ParameterSignature name must not be empty.");
        DisplayName = displayName ?? "";
        Required = required;
        DataType = dataType;
        Default = @default;
        Validator = validator;

        if (Validator == null)
        {
            Validator = _ => true;
        }
    }
}
