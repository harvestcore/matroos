using System;

using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Tests;
using Matroos.Worker.Controllers;
using Matroos.Worker.Services;

using Microsoft.AspNetCore.Mvc;

using Xunit;

using WWorker = Matroos.Resources.Classes.Workers.Worker;

namespace Matroos.Worker.Tests.Controllers;

public class MainControllerTests
{
    private readonly MainController _mainController;

    public MainControllerTests()
    {
        _mainController = new MainController(new MainService());
    }

    [Fact]
    public void GET_GetBot_Test()
    {
        ActionResult<WWorker>? res = _mainController.Get();
        OkObjectResult? response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        WWorker? worker = response?.Value as WWorker;
        Assert.Empty(worker?.Bots ?? null);
        Assert.False(worker?.IsUp ?? true);
        Assert.Equal(string.Empty, worker?.RemoteUrl ?? "not-empty");
        Assert.NotEqual(Guid.Empty, worker?.Id ?? Guid.Empty);
        Assert.True((worker?.LastUpdate ?? DateTime.UtcNow + TimeSpan.FromSeconds(5)) < DateTime.UtcNow);
    }

    [Fact]
    public void POST_AddBot_Test()
    {
        Bot bot = new("test", "", "!", "key", new());

        // Add the bot.
        _mainController.Add(bot).SuccessResponseShouldBe(true);

        // Adding an existant bot.
        _mainController.Add(bot).SuccessResponseShouldBe(false);
    }

    [Fact]
    public void PUT_UpdateBot_Test()
    {
        Bot bot = new("test", "", "!", "key", new());
        Bot updated = new("test2", "2", "!2", "key2", new());
        _mainController.Add(bot).SuccessResponseShouldBe(true);
        bot.Update(updated);

        // Update the bot.
        _mainController.Update(bot).SuccessResponseShouldBe(true);

        // Updating a non-existent bot.
        _mainController.Update(updated).SuccessResponseShouldBe(false);
    }

    [Fact]
    public void DELETE_DeleteBot_Test()
    {
        Bot bot = new("test", "", "!", "key", new());
        _mainController.Add(bot).SuccessResponseShouldBe(true);

        // Delete the bot.
        _mainController.Delete(bot.Id).SuccessResponseShouldBe(true);

        // Deleting a non-existent bot.
        _mainController.Delete(Guid.NewGuid()).SuccessResponseShouldBe(false);
    }

    [Fact]
    public void GET_StartBot_StopBot_Test()
    {
        Bot bot = new("test", "", "!", "key", new());
        _mainController.Add(bot).SuccessResponseShouldBe(true);

        // Start the bot.
        _mainController.Start(bot.Id).SuccessResponseShouldBe(true);

        // Stop the bot.
        _mainController.Stop(bot.Id).SuccessResponseShouldBe(true);

        // Starting a non-existent bot.
        _mainController.Start(Guid.NewGuid()).SuccessResponseShouldBe(false);

        // Stopping a non-existent bot.
        _mainController.Start(Guid.NewGuid()).SuccessResponseShouldBe(false);
    }
}
