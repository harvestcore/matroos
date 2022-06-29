using Matroos.Resources.Services;
using Matroos.Resources.Services.Interfaces;

using Microsoft.Extensions.Configuration;

namespace Matroos.Resources.Tests.Base;

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
        string connectionString = _configurationService.Get<string>("TestMongoDBConnectionString") ?? "http://localhost:27017/matroos-test";
        string databaseName = _configurationService.Get<string>("TestMongoDBDatabaseName") ?? "matroos-test";

        // Configure the testing database.
        _configurationService.Set("MongoDBConnectionString", connectionString);
        _configurationService.Set("MongoDBDatabaseName", databaseName);

        _dataContextService = new DataContextService(_configurationService);
    }
}