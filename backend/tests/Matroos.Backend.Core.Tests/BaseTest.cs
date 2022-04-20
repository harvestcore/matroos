using Matroos.Backend.Core.Services;
using Matroos.Backend.Core.Services.Interfaces;
using Matroos.Resources.Services;
using Matroos.Resources.Services.Interfaces;

using Microsoft.Extensions.Configuration;

namespace Matroos.Backend.Core.Tests;

public class BaseTest
{
    public readonly IDataContextService _dataContextService;
    public readonly IConfigurationService _configurationService;

    public BaseTest()
    {
        // Create the base configuration object.
        IConfiguration configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddInMemoryCollection()
            .Build();

        // Create the configuration service.
        _configurationService = new ConfigurationService(configuration);

        // The default values can be overwriten via Environment Variables.
        string connectionString = _configurationService.Get<string>("TestMongoDBConnectionString") ?? "";
        string databaseName = _configurationService.Get<string>("TestMongoDBDatabaseName") ?? "";

        // Configure the testing database.
        _configurationService.Set("MongoDBConnectionString", connectionString);
        _configurationService.Set("MongoDBDatabaseName", databaseName);

        _dataContextService = new DataContextService(_configurationService);
    }
}
