using System.Linq.Expressions;

using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Services.Interfaces;

namespace Matroos.Backend.Services;

public class BotsService : IBotsService
{
    /// <summary>
    /// The data context service.
    /// </summary>
    private readonly IDataContextService _dataContextService;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public BotsService(IDataContextService dataContextService)
    {
        _dataContextService = dataContextService;
    }

    /// <inheritdoc />
    public async Task<Bot?> Get(Guid botId)
    {
        return await _dataContextService.Get<Bot>(botId);
    }

    /// <inheritdoc />
    public async Task<List<Bot>> GetAll()
    {
        return await _dataContextService.GetAll<Bot>();
    }

    /// <inheritdoc />
    public async Task<List<Bot>> Filter(Expression<Func<Bot, bool>> filter)
    {
        return await _dataContextService.Filter<Bot>(filter);
    }

    /// <inheritdoc />
    public async Task<(bool, Guid)> AddBot(Bot bot)
    {
        List<Bot> filtered = await _dataContextService.Filter<Bot>(_bot =>
            _bot.Id == bot.Id ||
            _bot.Name == bot.Name ||
            _bot.Key == bot.Key
        );

        if (filtered.Count > 0)
        {
            return (false, Guid.Empty);
        }

        Bot newBot = new(
            name: bot.Name,
            description: bot.Description,
            prefix: bot.Prefix,
            key: bot.Key,
            userCommands: bot.UserCommands
        );

        bool result = await _dataContextService.Insert(newBot);

        return (result, result ? newBot.Id : Guid.Empty);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteBot(Guid botId)
    {
        return await _dataContextService.Delete<Bot>(botId);
    }

    /// <inheritdoc />
    public async Task<bool> UpdateBot(Bot bot)
    {
        Bot? found = await _dataContextService.Get<Bot>(bot.Id);
        if (found == null)
        {
            return false;
        }

        // Update the bot.
        found.Update(bot);

        // Push the changes to DB.
        return await _dataContextService.Update(found.Id, found);
    }
}
