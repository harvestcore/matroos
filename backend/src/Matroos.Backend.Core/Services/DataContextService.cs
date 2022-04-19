using System.Reflection;

using Matroos.Backend.Core.Classes;
using Matroos.Backend.Core.Dto;
using Matroos.Backend.Core.Services.Interfaces;
using Matroos.Resources.Services.Interfaces;

using MongoDB.Driver;

namespace Matroos.Backend.Core.Services;

public class DataContextService : IDataContextService
{
    /// <summary>
    /// The MongoDB client.
    /// </summary>
    private readonly MongoClient _mongoClient;

    /// <summary>
    /// The database.
    /// </summary>
    private readonly IMongoDatabase _database;

    /// <summary>
    /// The static property name that contains the collection name.
    /// </summary>
    private readonly string CollectionNamePropertyName = "CollectionName";

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="configurationService">The configuration service where to extract the required connection parameters.</param>
    /// <exception cref="Exception">Exceptions.</exception>
    public DataContextService(IConfigurationService configurationService)
    {
        string? connectionString = configurationService.Get<string>("MongoDBConnectionString");
        string? databaseName = configurationService.Get<string>("MongoDBDatabaseName");

        _mongoClient = new MongoClient(connectionString) ?? throw new Exception("Could not get the MongoDB client.");
        _database = _mongoClient?.GetDatabase(databaseName) ?? throw new Exception("Could not get the database.");
    }

    /// <inheritdoc/>
    public IMongoCollection<TValue>? GetCollection<TValue>() where TValue : IBaseItem
    {
        // Get the static property that contains the collection name.
        PropertyInfo? property = typeof(TValue).GetProperty(CollectionNamePropertyName);

        if (property != null)
        {
            // Cast the object to string.
            string? collectionName = property.GetValue(null) as string;

            if (!string.IsNullOrEmpty(collectionName))
            {
                // Return the collection.
                return _database.GetCollection<TValue>(collectionName);
            }
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<List<TValue>> GetAll<TValue>() where TValue : IBaseItem
    {
        // List of elements to be returned.
        List<TValue> output = new();

        // Get the collection.
        IMongoCollection<TValue>? collection = GetCollection<TValue>();

        if (collection != null)
        {
            // Get all the items.
            IAsyncCursor<TValue> items = await collection.FindAsync(_ => true);
            if (items != null)
            {
                // Convert the items to a list.
                output = items.ToList();
            }
        }

        // Return the items.
        return output;
    }

    /// <inheritdoc/>
    public async Task<List<TValue>> GetAll<TValue>(QueryFilter queryFilter) where TValue : IBaseItem
    {
        // List of elements to be returned.
        List<TValue> output = new();

        // Get the collection.
        IMongoCollection<TValue>? collection = GetCollection<TValue>();

        if (collection != null)
        {
            // Avoid null or empty search fields in the query filter.
            queryFilter.SearchFields = queryFilter.SearchFields.FindAll(item => !string.IsNullOrEmpty(item));

            if (!string.IsNullOrEmpty(queryFilter.SearchTerm) && queryFilter.SearchFields.Count > 0)
            {
                // Create ethe different queries using a case insensitive regex based on the search term.
                FilterDefinition<TValue>[] queries = queryFilter.SearchFields
                    .Select(field => Builders<TValue>.Filter.Regex(field, $"/{queryFilter.SearchTerm}/isg"))
                    .ToArray();

                // Get all the items that match the query and other filters.
                output = await collection
                    .Find(Builders<TValue>.Filter.Or(queries))
                    .Skip(queryFilter.Skip)
                    .Limit(queryFilter.Limit)
                    .ToListAsync();
            }
            else
            {
                output = await collection
                    .Find(_ => true)
                    .Skip(queryFilter.Skip)
                    .Limit(queryFilter.Limit)
                    .ToListAsync();
            }
        }

        // Return the items.
        return output;
    }

    /// <inheritdoc/>
    public async Task<TValue?> Get<TValue>(Guid guid) where TValue : IBaseItem
    {
        // Get the collection.
        IMongoCollection<TValue>? collection = GetCollection<TValue>();

        if (collection != null)
        {
            // Get all the items.
            FilterDefinition<TValue> filter = Builders<TValue>.Filter.Eq("Id", guid.ToString());
            IAsyncCursor<TValue> items = await collection.FindAsync(filter);
            if (items != null)
            {
                // Convert the items to a list.
                return items.FirstOrDefault();
            }
        }

        // Return the items.
        return default;
    }

    /// <inheritdoc/>
    public async Task<bool> Insert<TValue>(TValue item) where TValue : IBaseItem
    {
        if (item == null)
        {
            return false;
        }

        // Get the collection.
        IMongoCollection<TValue>? collection = GetCollection<TValue>();

        if (collection != null)
        {
            await collection.InsertOneAsync(item);
            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<bool> Update<TValue>(Guid guid, TValue item) where TValue : IBaseItem
    {
        if (item == null)
        {
            return false;
        }

        // Get the collection.
        IMongoCollection<TValue>? collection = GetCollection<TValue>();

        if (collection != null)
        {
            FilterDefinition<TValue> filter = Builders<TValue>.Filter.Eq("Id", guid.ToString());
            ReplaceOneResult result = await collection.ReplaceOneAsync(filter, item);
            return result.ModifiedCount == 1;
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete<TValue>(Guid guid) where TValue : IBaseItem
    {
        // Get the collection.
        IMongoCollection<TValue>? collection = GetCollection<TValue>();

        if (collection != null)
        {
            FilterDefinition<TValue> filter = Builders<TValue>.Filter.Eq("Id", guid.ToString());
            DeleteResult result = await collection.DeleteOneAsync(filter);
            return result.DeletedCount == 1;
        }

        return false;
    }
}
