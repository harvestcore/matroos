using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Classes.Workers;

namespace Matroos.Backend.Services;

public class WorkersService : IWorkersService
{
    /// <summary>
    /// The workers.
    /// </summary>
    public List<Worker> Workers { get; }

    public WorkersService()
    {
        Workers = new();
    }

    /// <inheritdoc />
    public bool StartBotInWorker(Guid workerId, Guid botId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool StopBotInWorker(Guid workerId, Guid botId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<List<Bot>> GetBotsFromWorker(Guid workerId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool AddBotsToWorker(Guid workerId, List<Guid> botIds)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool UpdateBotsInWorker(Guid workerId, List<Guid> botIds)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool DeleteBotsFromWorker(Guid workerId, List<Guid> botIds)
    {
        throw new NotImplementedException();
    }
}
