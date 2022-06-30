using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Classes.Workers;
using Matroos.Resources.Extensions;
using Matroos.Resources.Services.Interfaces;

namespace Matroos.Backend.Services;

public class WorkersService : IWorkersService
{
    /// <inheritdoc />
    public List<Worker> Workers { get; }

    /// <summary>
    /// The <see cref="IConfigurationService"/>.
    /// </summary>
    private readonly IConfigurationService _configurationService;

    /// <summary>
    /// The <see cref="ICommunicationService"/>.
    /// </summary>
    private readonly ICommunicationService _communicationService;

    /// <summary>
    /// The <see cref="IBotsService"/>;
    /// </summary>
    private readonly IBotsService _botsService;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="communicationService">The <see cref="CommunicationService"/>.</param>
    public WorkersService(IConfigurationService configurationService, ICommunicationService communicationService, IBotsService botsService)
    {
        _configurationService = configurationService;
        _communicationService = communicationService;
        _botsService = botsService;

        Workers = new();
    }

    /// <inheritdoc />
    public async Task RenewWorkers()
    {
        string workers = _configurationService.Get<string>("Workers") ?? "";
        List<string> workerURLs = workers.GetWorkerURLs();

        foreach (string workerURL in workerURLs)
        {
            Worker? newWorker = await _communicationService.GetWorkerStatus(workerURL);
            if (newWorker == null)
            {
                continue;
            }

            Worker? current = Workers.Find(item => item.RemoteUrl.Equals(workerURL));
            if (current != null)
            {
                current.Renew(newWorker.Id, newWorker.Bots, workerURL);
            }
            else
            {
                newWorker.Renew(newWorker.Id, newWorker.Bots, workerURL);
                Workers.Add(newWorker);
            }
        }
    }

    /// <inheritdoc />
    public bool StartBotInWorker(Guid workerId, Guid botId)
    {
        Worker? workerFound = Workers.Find(w => w.Id == workerId);
        if (workerFound == null)
        {
            return false;
        }

        _ = _communicationService.StartBotInWorker(workerFound, botId);

        return true;
    }

    /// <inheritdoc />
    public bool StopBotInWorker(Guid workerId, Guid botId)
    {
        Worker? workerFound = Workers.Find(w => w.Id == workerId);
        if (workerFound == null)
        {
            return false;
        }

        _ = _communicationService.StopBotInWorker(workerFound, botId);

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> AddBotsToWorker(Guid workerId, List<Guid> botIds)
    {
        Worker? workerFound = Workers.Find(w => w.Id == workerId);

        if (workerFound == null)
        {
            return false;
        }

        List<Bot> bots = await _botsService.Filter(bot => botIds.Contains(bot.Id));

        foreach (Bot bot in bots)
        {
            // Do not wait for this call to finish.
            _ = _communicationService.AddBotToWorker(workerFound, bot);
        }

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateBotsInWorker(Guid workerId, List<Guid> botIds)
    {
        Worker? workerFound = Workers.Find(w => w.Id == workerId);

        if (workerFound == null)
        {
            return false;
        }

        List<Bot> bots = await _botsService.Filter(bot => botIds.Contains(bot.Id));

        foreach (Bot bot in bots)
        {
            // Do not wait for this call to finish.
            _ = _communicationService.UpdateBotInWorker(workerFound, bot);
        }

        _ = RenewWorkers();

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteBotsFromWorker(Guid workerId, List<Guid> botIds)
    {
        Worker? workerFound = Workers.Find(w => w.Id == workerId);

        if (workerFound == null)
        {
            return false;
        }

        List<Bot> bots = await _botsService.Filter(bot => botIds.Contains(bot.Id));

        foreach (Bot bot in bots)
        {
            // Do not wait for this call to finish.
            _ = _communicationService.DeleteBotFromWorker(workerFound, bot.Id);
        }

        _ = RenewWorkers();

        return true;
    }
}
