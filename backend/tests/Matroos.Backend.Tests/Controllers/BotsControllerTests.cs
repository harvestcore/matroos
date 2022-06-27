using System;

using Matroos.Backend.Controllers;
using Matroos.Backend.Services;
using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.API;
using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Tests;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace Matroos.Worker.Tests.Controllers;

public class BotsControllerTests
{
    private readonly BotsController _botsController;
    private readonly IBotsService _botsService;

    public BotsControllerTests()
    {
        _botsService = new BotsService();
        _botsController = new BotsController(_botsService);
    }

    [Fact]
    public void GET_GetAll_Test()
    {
        // 0 bots.
        ActionResult<ItemsResponse<Bot>>? res = _botsController.GetAll();
        OkObjectResult? response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        ItemsResponse<Bot>? items = response?.Value as ItemsResponse<Bot>;
        Assert.Empty(items?.Items ?? null);
        Assert.Equal(0, items?.Count);

        // 2 bots.
        _botsService.AddBot(new("test", "", "!", "key", new()));
        _botsService.AddBot(new("test2", "2", "!2", "key2", new()));
        res = _botsController.GetAll();
        response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        items = response?.Value as ItemsResponse<Bot>;
        Assert.NotEmpty(items?.Items ?? null);
        Assert.Equal(2, items?.Count);
    }

    [Fact]
    public void GET_GetSingleBot_Test()
    {
        // Existent bot.
        Bot bot = new("test", "", "!", "key", new());
        (_, Guid botId) = _botsService.AddBot(bot);

        ActionResult<Bot>? res = _botsController.Get(botId);
        OkObjectResult? response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        Bot? b = response?.Value as Bot;
        Assert.NotNull(b);

        // Non-existent bot.
        res = _botsController.Get(Guid.NewGuid());
        NotFoundResult? nfResult = res.Result as NotFoundResult;
        Assert.Equal(404, nfResult?.StatusCode ?? 0);
    }

    [Fact]
    public void POST_AddBot_Test()
    {
        Bot bot = new("test", "", "!", "key", new());
        _botsController.Post(bot).SuccessResponseShouldBe(true);


        Bot? bot2 = new("test2", "2", "!2", "key2", new());
        (_, Guid botId) = _botsService.AddBot(bot2);
        bot2 = _botsService.Bots.Find(b => b.Id == botId);

        if (bot2 == null)
        {
            throw new Exception("The bot is somehow null");
        }

        _botsController.Post(bot2).CheckResponse<BadRequestObjectResult>();
    }

    [Fact]
    public void PUT_UpdateBot_Test()
    {
        Bot? bot = new("test", "", "!", "key", new());
        (_, Guid botId) = _botsService.AddBot(bot);
        bot = _botsService.Bots.Find(b => b.Id == botId);

        if (bot == null)
        {
            throw new Exception("The bot is somehow null");
        }
        _botsController.Put(bot).SuccessResponseShouldBe(true);

        Bot bot2 = new("test2", "2", "!2", "key2", new());
        _botsController.Put(bot2).CheckResponse<BadRequestObjectResult>();
    }

    [Fact]
    public void DELETE_DeleteBot_Test()
    {
        Bot? bot = new("test", "", "!", "key", new());
        (_, Guid botId) = _botsService.AddBot(bot);

        _botsController.Delete(botId).SuccessResponseShouldBe(true);

        // Non-existent bot.
        _botsController.Delete(Guid.NewGuid()).CheckResponse<BadRequestObjectResult>();
    }
}
