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
        if (Bots.Any(_bot =>
            _bot.Id == bot.Id ||
            _bot.Name == bot.Name ||
            _bot.Key == bot.Key
        ))
        {
            return false;
        }

        Bots.Add(new Bot(
            name: bot.Name,
            description: bot.Description,
            prefix: bot.Prefix,
            key: bot.Key,
            userCommands: bot.UserCommands
        ));

        return true;
    }

    /// <inheritdoc />
    public bool DeleteBot(Guid botId)
    {
        Bot? botFound = Bots.Find(_bot => _bot.Id == botId);

        if (botFound == null)
        {
            return false;
        }

        return Bots.Remove(botFound);
    }

    /// <inheritdoc />
    public bool UpdateBot(Bot bot)
    {
        Bot? botFound = Bots.Find(_bot => _bot.Id == bot.Id);

        if (botFound == null)
        {
            return false;
        }

        botFound.Update(bot);

        return true;
    }
}
