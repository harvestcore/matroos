using Matroos.Resources.Services.Interfaces;

using Microsoft.Extensions.Configuration;

namespace Matroos.Resources.Services;

public class ConfigurationService : IConfigurationService
{
    /// <summary>
    /// The base configuration.
    /// </summary>
    private readonly IConfiguration _baseConfiguration;

    /// <summary>
    /// The configuration pool.
    /// </summary>
    private readonly Dictionary<string, object> _configurationPool;

    public ConfigurationService(IConfiguration configuration)
    {
        _baseConfiguration = configuration;
        _configurationPool = new();
    }

    /// <inheritdoc />
    public object? Get(string key)
    {
        // Try to get the variable from the configuration pool.
        _configurationPool.TryGetValue(key, out object? fromPool);
        if (fromPool != null)
        {
            return fromPool;
        }

        // Try to get the variable from the environment.
        object? fromEnvironment = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.User);
        if (fromEnvironment != null)
        {
            _configurationPool.Add(key, fromEnvironment);
            return fromEnvironment;
        }

        // Try to get the variable from the base configuration service.
        object? fromBaseConfiguration = _baseConfiguration[key];
        if (fromBaseConfiguration != null)
        {
            _configurationPool.Add(key, fromBaseConfiguration);
            return fromBaseConfiguration;
        }

        return null;
    }

    /// <inheritdoc />
    public TValue? Get<TValue>(string key)
    {
        // Get the variable.
        object? variable = Get(key);

        // Cast it or return the default value.
        return variable != null ? (TValue)variable : default;
    }

    /// <inheritdoc />
    public void Set(string key, object value)
    {
        if (!_configurationPool.ContainsKey(key))
        {
            _configurationPool.Add(key, value);
        }
    }
}
