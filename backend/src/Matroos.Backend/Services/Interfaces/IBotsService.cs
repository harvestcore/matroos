using Matroos.Resources.Classes.Bots;

namespace Matroos.Backend.Services.Interfaces;

public interface IBotsService
{
    /// <summary>
    /// The bots.
    /// </summary>
    public List<Bot> Bots { get; }

    /// <summary>
    /// Add a new bot.
    /// </summary>
    /// <param name="bot">The bot.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public bool AddBot(Bot bot);

    /// <summary>
    /// Update the data of a bot.
    /// </summary>
    /// <param name="bot">The bot with the updated data.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public bool UpdateBot(Bot bot);

    /// <summary>
    /// Delete a bot.
    /// </summary>
    /// <param name="botId">The identifier of the bot to be removed.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public bool DeleteBot(Guid botId);
}
