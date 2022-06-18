using System.Text.Json;

namespace Matroos.Resources.Extensions;

public static class ObjectExtensions
{
    /// <summary>
    /// Get the value of the object casted to the given type.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <param name="obj">The object where to get the value.</param>
    /// <returns>The value.</returns>
    public static T? GetValue<T>(this object obj)
    {
        if (obj == null)
        {
            return default;
        }

        if (obj is JsonElement element)
        {
            string json = element.GetRawText();
            return JsonSerializer.Deserialize<T>(json);
        }

        return (T)obj;
    }
}
