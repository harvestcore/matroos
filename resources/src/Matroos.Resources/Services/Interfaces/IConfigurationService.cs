namespace Matroos.Resources.Services.Interfaces;

public interface IConfigurationService
{
    /// <summary>
    /// Get a variable from the configuration pool.
    /// </summary>
    /// <param name="key">The name of that variable.</param>
    /// <returns>The variable found or null.</returns>
    public object? Get(string key);

    /// <summary>
    /// Get a variable from the configuration pool.
    /// </summary>
    /// <typeparam name="TValue">The type of the variable to get.</typeparam>
    /// <param name="key">The name of that variable.</param>
    /// <returns>The variable found or the default value of the given type.</returns>
    public TValue? Get<TValue>(string key);

    /// <summary>
    /// Set a variable in the configuarion pool.
    /// </summary>
    /// <param name="key">The name of the variable.</param>
    /// <param name="value">The value of the variable.</param>
    public void Set(string key, object value);
}
