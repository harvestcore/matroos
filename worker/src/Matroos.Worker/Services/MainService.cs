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
        if (!Bots.ContainsKey(bot.Id))
        {
            Bots.Add(bot.Id, bot);
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    public bool DestroyBot(Guid botId)
    {
        Bots.TryGetValue(botId, out Bot? bot);
        if (bot != null)
        {
            bot.Stop();
            Bots.Remove(botId);

            return true;
        }

        return false;
    }

    /// <inheritdoc />
    public bool UpdateBot(Bot bot)
    {
        return DestroyBot(bot.Id) && CreateBot(bot);
    }

    /// <inheritdoc />
    public bool StartBot(Guid botId)
    {
        Bots.TryGetValue(botId, out Bot? bot);
        if (bot == null)
        {
            return false;
        }

        bot.Start();

        return true;
    }

    /// <inheritdoc />
    public bool StopBot(Guid botId)
    {
        Bots.TryGetValue(botId, out Bot? bot);
        if (bot == null)
        {
            return false;
        }

        bot.Stop();

        return true;
    }
}
