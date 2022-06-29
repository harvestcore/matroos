using Matroos.Resources.Classes.Workers;

namespace Matroos.Backend.Services.Interfaces;

public interface IWorkersService
{
    /// <summary>
    /// The workers.
    /// </summary>
    public List<Worker> Workers { get; }

    /// <summary>
    /// Renew workers information.
    /// </summary>
    public Task RenewWorkers();

    /// <summary>
    /// Starts a bot in a worker.
    /// </summary>
    /// <param name="workerId">The identifier of the worker where to start the bot.</param>
    /// <param name="botId">The identifier of the bot to be started.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public bool StartBotInWorker(Guid workerId, Guid botId);

    /// <summary>
    /// Stops a bot in a worker.
    /// </summary>
    /// <param name="workerId">The identifier of the worker where to stop the bot.</param>
    /// <param name="botId">The identifier of the bot to be stop.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public bool StopBotInWorker(Guid workerId, Guid botId);

    /// <summary>
    /// Adds bots to a worker.
    /// </summary>
    /// <param name="workerId">The identifier of the worker where to add the bots.</param>
    /// <param name="botIds">A list containing the identifiers of the bots to be added.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public Task<bool> AddBotsToWorker(Guid workerId, List<Guid> botIds);

    /// <summary>
    /// Updates the data of the given bots in a worker.
    /// </summary>
    /// <param name="workerId">The identifier of the worker where to update the data.</param>
    /// <param name="botIds">A list containing the identifiers of the bots to be updated.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public Task<bool> UpdateBotsInWorker(Guid workerId, List<Guid> botIds);

    /// <summary>
    /// Deletes bots from a worker.
    /// </summary>
    /// <param name="workerId">The identifier of the worker where to delete the bots.</param>
    /// <param name="botIds">A list containing the identifiers of the bots to be removed.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public Task<bool> DeleteBotsFromWorker(Guid workerId, List<Guid> botIds);
}
