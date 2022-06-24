using System;

using Matroos.Resources.Classes.Bots;
using Matroos.Worker.Services;
using Matroos.Worker.Services.Interfaces;

using Xunit;

namespace Matroos.Worker.Tests.Services;

public class MainServiceTests
{
    private readonly IMainService _mainService;

    public MainServiceTests()
    {
        _mainService = new MainService();
    }

    [Fact]
    public void InitializationTests()
    {
        Assert.False(_mainService.Id == Guid.Empty);
        Assert.Empty(_mainService.Bots);
    }

    [Fact]
    public void CreateBotTests()
    {
        Bot bot = new("test", "", "!", "key", new());
        Assert.True(_mainService.CreateBot(bot));
        Assert.False(_mainService.CreateBot(bot));
        Assert.Single(_mainService.Bots);
    }

    [Fact]
    public void DestroyBotTests()
    {
        Bot bot = new("test", "", "!", "key", new());
        _mainService.CreateBot(bot);

        Assert.True(_mainService.DestroyBot(bot.Id));
        Assert.False(_mainService.DestroyBot(bot.Id));
        Assert.Empty(_mainService.Bots);
    }

    [Fact]
    public void UpdateBotTests()
    {
        Bot bot = new("test", "", "!", "key", new());
        _mainService.CreateBot(bot);

        Bot updated = new("test2", "2", "!2", "key2", new());
        bot.Update(updated);
        Assert.True(_mainService.UpdateBot(bot));
        Assert.Single(_mainService.Bots);

        Bot anotherBot = new("test3", "3", "!3", "key3", new());
        Assert.False(_mainService.UpdateBot(anotherBot));
    }

    [Fact]
    public void StartStopBotTests()
    {
        Bot bot = new("test", "", "!", "key", new());
        _mainService.CreateBot(bot);

        // Start and stop an existent bot.
        Assert.True(_mainService.StartBot(bot.Id));
        Assert.True(_mainService.StopBot(bot.Id));

        // Start and stop non-existent bots.
        Assert.False(_mainService.StartBot(Guid.NewGuid()));
        Assert.False(_mainService.StopBot(Guid.NewGuid()));
    }
}
