using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.Bots;

namespace Matroos.Backend.Services;

public class BotsService : IBotsService
{
    /// <inheritdoc />
    public List<Bot> Bots { get; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public BotsService()
    {
        Bots = new List<Bot>();
    }

    /// <inheritdoc />
    public bool AddBot(Bot bot)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool DeleteBot(Guid botId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool UpdateBot(Bot bot)
    {
        throw new NotImplementedException();
    }
}
