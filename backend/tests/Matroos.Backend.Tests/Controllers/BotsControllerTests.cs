using System;

using Matroos.Backend.Controllers;
using Matroos.Backend.Services;
using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.API;
using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Classes.Commands;
using Matroos.Resources.Tests;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace Matroos.Worker.Tests.Controllers;

[Collection("Sequential")]
public class BotsControllerTests : BaseTest
{
    private readonly BotsController _botsController;
    private readonly IBotsService _botsService;

    public BotsControllerTests() : base()
    {
        _botsService = new BotsService(_dataContextService);
        _botsController = new BotsController(_botsService);
    }

    [Fact]
    public async void GET_GetAll_Test()
    {
        await EmptyCollection<Bot>();
        // 0 bots.
        ActionResult<ItemsResponse<Bot>>? res = await _botsController.GetAll();
        OkObjectResult? response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        ItemsResponse<Bot>? items = response?.Value as ItemsResponse<Bot>;
        Assert.Empty(items?.Items ?? null);
        Assert.Equal(0, items?.Count);

        // 2 bots.
        await _botsService.AddBot(new("test", "", "!", "key", new()));
        await _botsService.AddBot(new("test2", "2", "!2", "key2", new()));
        res = await _botsController.GetAll();
        response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        items = response?.Value as ItemsResponse<Bot>;
        Assert.NotEmpty(items?.Items ?? null);
        Assert.Equal(2, items?.Count);
    }

    [Fact]
    public async void GET_GetSingleBot_Test()
    {
        await EmptyCollection<Bot>();
        // Existent bot.
        Bot bot = new("test", "", "!", "key", new());
        (_, Guid botId) = await _botsService.AddBot(bot);

        ActionResult<Bot>? res = await _botsController.Get(botId);
        OkObjectResult? response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        Bot? b = response?.Value as Bot;
        Assert.NotNull(b);

        // Non-existent bot.
        res = await _botsController.Get(Guid.NewGuid());
        NotFoundResult? nfResult = res.Result as NotFoundResult;
        Assert.Equal(404, nfResult?.StatusCode ?? 0);
    }

    [Fact]
    public async void POST_AddBot_Test()
    {
        await EmptyCollection<Bot>();
        Bot bot = new("test", "", "!", "key", new());
        (await _botsController.Post(bot)).SuccessResponseShouldBe(true);


        Bot? bot2 = new("test2", "2", "!2", "key2", new());
        (_, Guid botId) = await _botsService.AddBot(bot2);
        bot2 = await _botsService.Get(botId);

        if (bot2 == null)
        {
            throw new Exception("The bot is somehow null");
        }

        (await _botsController.Post(bot2)).CheckResponse<BadRequestObjectResult>();
    }

    [Fact]
    public async void PUT_UpdateBot_Test()
    {
        await EmptyCollection<Bot>();
        Bot? bot = new("test", "", "!", "key", new());
        (_, Guid botId) = await _botsService.AddBot(bot);
        bot = await _botsService.Get(botId);

        if (bot == null)
        {
            throw new Exception("The bot is somehow null");
        }
        (await _botsController.Put(bot)).SuccessResponseShouldBe(true);

        Bot bot2 = new("test2", "2", "!2", "key2", new());
        (await _botsController.Put(bot2)).CheckResponse<BadRequestObjectResult>();
    }

    [Fact]
    public async void DELETE_DeleteBot_Test()
    {
        await EmptyCollection<Bot>();
        Bot? bot = new("test", "", "!", "key", new());
        (_, Guid botId) = await _botsService.AddBot(bot);

        (await _botsController.Delete(botId)).SuccessResponseShouldBe(true);

        // Non-existent bot.
        (await _botsController.Delete(Guid.NewGuid())).CheckResponse<BadRequestObjectResult>();
    }
}
