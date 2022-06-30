using System.Linq.Expressions;

using Matroos.Resources.Classes.Bots;

namespace Matroos.Backend.Services.Interfaces;

public interface IBotsService
{
    /// <summary>
    /// Get a bot by its identifier.
    /// </summary>
    /// <param name="botId">The bot identifier.</param>
    /// <returns>The bot found.</returns>
    public Task<Bot?> Get(Guid botId);

    /// <summary>
    /// Get all the bots.
    /// </summary>
    /// <returns>A list containing all the bots.</returns>
    public Task<List<Bot>> GetAll();

    /// <summary>
    /// Get all the bots that match the filter.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <returns>A list containing all the bots found.</returns>
    public Task<List<Bot>> Filter(Expression<Func<Bot, bool>> filter);

    /// <summary>
    /// Add a new bot.
    /// </summary>
    /// <param name="bot">The bot.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public Task<(bool, Guid)> AddBot(Bot bot);

    /// <summary>
    /// Update the data of a bot.
    /// </summary>
    /// <param name="bot">The bot with the updated data.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public Task<bool> UpdateBot(Bot bot);

    /// <summary>
    /// Delete a bot.
    /// </summary>
    /// <param name="botId">The identifier of the bot to be removed.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public Task<bool> DeleteBot(Guid botId);
}
