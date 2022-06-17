using Matroos.Resources.Classes.Bots;

namespace Matroos.Worker.Services.Interfaces;

public interface IMainService
{
    /// <summary>
    /// Create a bot in the worker.
    /// </summary>
    /// <param name="bot">The bot.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public bool CreateBot(Bot bot);

    /// <summary>
    /// Destroy a bot in the worker.
    /// </summary>
    /// <param name="botId">The bot identifier.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public bool DestroyBot(Guid botId);

    /// <summary>
    /// Update the data of a bot.
    /// </summary>
    /// <param name="bot">The bot with the updated data.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public bool UpdateBot(Bot bot);

    /// <summary>
    /// Start a bot.
    /// </summary>
    /// <param name="botId">The bot identifier.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public bool StartBot(Guid botId);

    /// <summary>
    /// Stop a bot.
    /// </summary>
    /// <param name="botId">The bot identifier.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public bool StopBot(Guid botId);
}
