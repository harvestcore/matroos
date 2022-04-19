using Matroos.Backend.Core.Classes;
using Matroos.Backend.Core.Dto;

using MongoDB.Driver;

namespace Matroos.Backend.Core.Services.Interfaces;

public interface IDataContextService
{
    /// <summary>
    /// Get a collection from the database.
    /// </summary>
    /// <typeparam name="TValue">The data type of the collection.</typeparam>
    /// <returns>The collection found or null.</returns>
    public IMongoCollection<TValue>? GetCollection<TValue>() where TValue : IBaseItem;

    /// <summary>
    /// Get all the items of the collection.
    /// </summary>
    /// <typeparam name="TValue">The data type of the collection.</typeparam>
    /// <returns>All the items found.</returns>
    public Task<List<TValue>> GetAll<TValue>() where TValue : IBaseItem;

    /// <summary>
    /// Get all the items of the collection that matches the <see cref="QueryFilter"/>.
    /// </summary>
    /// <typeparam name="TValue">The data type of the collection.</typeparam>
    /// <param name="queryFilter">The filter to be matched.</param>
    /// <returns>All the items found.</returns>
    public Task<List<TValue>> GetAll<TValue>(QueryFilter queryFilter) where TValue : IBaseItem;

    /// <summary>
    /// Get the item that matches the given identifier.
    /// </summary>
    /// <typeparam name="TValue">The data type of the collection.</typeparam>
    /// <param name="guid">The identifier of the item to be fetched.</param>
    /// <returns>The item found or null.</returns>
    public Task<TValue?> Get<TValue>(Guid guid) where TValue : IBaseItem;

    /// <summary>
    /// Insert a new item in the collection.
    /// </summary>
    /// <typeparam name="TValue">The data type of the collection.</typeparam>
    /// <param name="item">The item to be inserted.</param>
    /// <returns>Whether the item was inserted or not.</returns>
    public Task<bool> Insert<TValue>(TValue item) where TValue : IBaseItem;

    /// <summary>
    /// Update an item in the collection.
    /// </summary>
    /// <typeparam name="TValue">The data type of the collection.</typeparam>
    /// <param name="guid">The identifier of the item to be updated.</param>
    /// <param name="item">The item with the new data.</param>
    /// <returns>Whether the item was updated or not.</returns>
    public Task<bool> Update<TValue>(Guid guid, TValue item) where TValue : IBaseItem;

    /// <summary>
    /// Delete an item from the collection.
    /// </summary>
    /// <typeparam name="TValue">The data type of the collection.</typeparam>
    /// <param name="guid">The identifier of the item to be deleted.</param>
    /// <returns>Whether the item was deleted or not.</returns>
    public Task<bool> Delete<TValue>(Guid guid) where TValue : IBaseItem;
}
