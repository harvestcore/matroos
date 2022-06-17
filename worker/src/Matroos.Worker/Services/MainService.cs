using Matroos.Resources.Classes.Bots;
using Matroos.Worker.Services.Interfaces;

namespace Matroos.Worker.Services;

public class MainService : IMainService
{
    /// <summary>
    /// The worker identifier.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// The bots hosted in this worker.
    /// </summary>
    public Dictionary<Guid, Bot> Bots { get; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public MainService()
    {
        Id = Guid.NewGuid();
        Bots = new();
    }

    /// <inheritdoc />
    public bool CreateBot(Bot bot)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool DestroyBot(Guid botId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool UpdateBot(Bot bot)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool StartBot(Guid botId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool StopBot(Guid botId)
    {
        throw new NotImplementedException();
    }
}
