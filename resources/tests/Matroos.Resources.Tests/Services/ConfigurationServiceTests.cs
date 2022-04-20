using System;
using System.Collections.Generic;

using Matroos.Resources.Services;
using Matroos.Resources.Services.Interfaces;

using Microsoft.Extensions.Configuration;

using Xunit;

namespace Matroos.Resources.Tests.Services;

public class ConfigurationServiceTests
{
    private readonly IConfigurationService _configurationService;
    public ConfigurationServiceTests()
    {
        // Base configuration data.
        Dictionary<string, string> configurationDictionary = new()
        {
            { "key_1", "value_1" },
            { "key_2", "value_2" }
        };

        // Create the base configuration object.
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configurationDictionary)
            .Build();

        // Create the configuration service.
        _configurationService = new ConfigurationService(configuration);
    }

    [Theory]
    [InlineData("key_1", "value_1")]
    [InlineData("key_2", "value_2")]
    [InlineData("key_0", null)]
    public void GetValuesFromBaseConfiguration(string key, string value)
    {
        // Assert the value.
        Assert.Equal(value, _configurationService.Get<string>(key));
    }

    [Theory]
    [InlineData("key_3", "value_3")]
    [InlineData("key_4", "value_4")]
    [InlineData("key_0", null)]
    public void GetValuesFromBaseEnvironment(string key, string value)
    {
        // Set the environment variable.
        Environment.SetEnvironmentVariable(key, value, EnvironmentVariableTarget.User);

        // Assert the value.
        Assert.Equal(value, _configurationService.Get<string>(key));
    }

    [Theory]
    [InlineData("key_5", "value_5")]
    [InlineData("key_6", "value_6")]
    [InlineData("key_0", null)]
    public void GetValuesFromPool(string key, string value)
    {
        // Set the variable in the pool.
        if (!string.IsNullOrEmpty(key))
        {
            _configurationService.Set(key, value);
        }

        // Assert the value.
        Assert.Equal(value, _configurationService.Get<string>(key));
    }

    [Theory]
    [InlineData("key_7", 7)]
    [InlineData("key_8", 0.8f)]
    [InlineData("key_9", true)]
    [InlineData("key_0", null)]
    public void SetAndGetDifferentTypesOfVariables(string key, object value)
    {
        // Set the variable in the pool.
        _configurationService.Set(key, value);

        // Assert the value.
        Assert.Equal(value, _configurationService.Get(key));
    }
}
