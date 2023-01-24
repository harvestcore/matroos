using AppContext.Interfaces;
using AppContext.Services;
using AppContext.Services.Interfaces;

using Microsoft.Extensions.Configuration;

using MongoDB.Driver;

namespace Matroos.Resources.Tests;

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
        string connectionString = _configurationService.Get<string>("TestMongoDBConnectionString") ?? "mongodb://localhost:27017/matroos-test";
        string databaseName = _configurationService.Get<string>("TestMongoDBDatabaseName") ?? "matroos-test";

        // Configure the testing database.
        _configurationService.Set("MongoDBConnectionString", connectionString);
        _configurationService.Set("MongoDBDatabaseName", databaseName);

        _dataContextService = new DataContextService(_configurationService);
    }

    public async Task<DeleteResult> EmptyCollection<TValue>() where TValue : IBaseItem
    {
        return await _dataContextService.GetCollection<TValue>().DeleteManyAsync(Builders<TValue>.Filter.Empty);
    }
}