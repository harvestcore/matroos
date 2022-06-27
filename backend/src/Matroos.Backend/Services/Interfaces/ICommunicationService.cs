using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Classes.Workers;

namespace Matroos.Backend.Services.Interfaces;

public interface ICommunicationService
{
    /// <summary>
    /// Perform a Http request.
    /// </summary>
    /// <param name="method">The request method.</param>
    /// <param name="uri">The request URI.</param>
    /// <param name="payload">The payload.</param>
    /// <returns>A task containing the <see cref="HttpResponseMessage"/>.</returns>
    public Task<HttpResponseMessage> Request(HttpMethod method, string uri, object? payload = null);

    /// <summary>
    /// Get the status of a given worker.
    /// </summary>
    /// <param name="remoteURL">The remote URL of the worker to update.</param>
    public Task<Worker?> GetWorkerStatus(string remoteURL);

    /// <summary>
    /// Adds a bot to a worker.
    /// </summary>
    /// <param name="worker">The worker where to add the bot.</param>
    /// <param name="bot">The identifier of the bot to add.</param>
    public Task AddBotToWorker(Worker worker, Bot bot);

    /// <summary>
    /// Updates a bot from a worker.
    /// </summary>
    /// <param name="worker">The worker where to update the bot.</param>
    /// <param name="bot">The identifier of the bot to update.</param>
    public Task UpdateBotInWorker(Worker worker, Bot bot);

    /// <summary>
    /// Removes a bot from a worker.
    /// </summary>
    /// <param name="worker">The worker where to delete the bot.</param>
    /// <param name="botId">The identifier of the bot to delete.</param>
    public Task DeleteBotFromWorker(Worker worker, Guid botId);

    /// <summary>
    /// Starts a bot in a worker.
    /// </summary>
    /// <param name="worker">The worker where to start the bot.</param>
    /// <param name="botId">The identifier of the bot to start.</param>
    public Task StartBotInWorker(Worker worker, Guid botId);

    /// <summary>
    /// Stops a bot in a worker.
    /// </summary>
    /// <param name="worker">The worker where to stop the bot.</param>
    /// <param name="botId">The identifier of the bot to stop.</param>
    public Task StopBotInWorker(Worker worker, Guid botId);
}
