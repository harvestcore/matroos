using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AppContext.Services;
using AppContext.Services.Interfaces;

using Matroos.Backend.Controllers;
using Matroos.Backend.Services;
using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.API;
using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Tests;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Moq;

using Xunit;

using WWorker = Matroos.Resources.Classes.Workers.Worker;

namespace Matroos.Worker.Tests.Controllers;

[Collection("Sequential")]
public class WorkersControllerTests : BaseTest
{
    private readonly WorkersController _workersController;
    private readonly IWorkersService _workersService;
    private readonly Dictionary<string, WWorker> _workers = new()
    {
        { "http://w1", new(Guid.NewGuid(), "http://w1", new()) },
        { "http://w2", new(Guid.NewGuid(), "http://w2", new()) }
    };

    public WorkersControllerTests() : base()
    {
        IBotsService botsService = new BotsService(_dataContextService);
        IConfigurationService configurationService = new ConfigurationService(new ConfigurationBuilder().Build());

        Mock<CommunicationService> csMock = new();
        csMock.Setup(cs => cs.GetWorkerStatus(It.IsAny<string>())).Returns((string url) => Task.FromResult(_workers[url]));
        csMock.Setup(cs => cs.StartBotInWorker(It.IsAny<WWorker>(), It.IsAny<Guid>())).Callback(() => { });
        csMock.Setup(cs => cs.AddBotToWorker(It.IsAny<WWorker>(), It.IsAny<Bot>())).Callback(() => { });
        csMock.Setup(cs => cs.UpdateBotInWorker(It.IsAny<WWorker>(), It.IsAny<Bot>())).Callback(() => { });
        csMock.Setup(cs => cs.DeleteBotFromWorker(It.IsAny<WWorker>(), It.IsAny<Guid>())).Callback(() => { });
        ICommunicationService communicationService = csMock.Object;

        _workersService = new WorkersService(configurationService, communicationService, botsService);
        _workersController = new WorkersController(_workersService);
    }

    [Fact]
    public void GET_Start_Test()
    {
        WWorker w = new(Guid.NewGuid(), "http://w1", new());
        _workersService.Workers.Add(w);

        _workersController.Start(w.Id, Guid.NewGuid()).SuccessResponseShouldBe(true);
        _workersController.Start(Guid.NewGuid(), Guid.NewGuid()).CheckResponse<BadRequestObjectResult>();
    }

    [Fact]
    public void GET_Stop_Test()
    {
        WWorker w = new(Guid.NewGuid(), "http://w1", new());
        _workersService.Workers.Add(w);

        _workersController.Stop(w.Id, Guid.NewGuid()).SuccessResponseShouldBe(true);
        _workersController.Stop(Guid.NewGuid(), Guid.NewGuid()).CheckResponse<BadRequestObjectResult>();
    }

    [Fact]
    public void GET_GetAllWorkers_Test()
    {
        _workersService.Workers.Add(_workers["http://w1"]);
        _workersService.Workers.Add(_workers["http://w2"]);
        ActionResult<ItemsResponse<WWorker>>? res = _workersController.Get();
        OkObjectResult? response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        ItemsResponse<WWorker>? items = response?.Value as ItemsResponse<WWorker>;
        Assert.NotNull(items);
        Assert.Equal(2, items?.Count ?? 0);
        Assert.Equal(2, items?.Items?.Count ?? 0);
    }

    [Fact]
    public void GET_GetWorker_Test()
    {
        WWorker w = _workers["http://w1"];
        _workersService.Workers.Add(w);

        ActionResult<WWorker>? res = _workersController.Get(w.Id);
        OkObjectResult? response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        // Non-existent bot.
        res = _workersController.Get(Guid.NewGuid());
        NotFoundResult? nfResult = res.Result as NotFoundResult;
        Assert.Equal(404, nfResult?.StatusCode ?? 0);
    }

    [Fact]
    public async void POST_AddBotToWorker_Test()
    {
        WWorker w = _workers["http://w1"];
        _workersService.Workers.Add(w);

        (await _workersController.Add(w.Id, new())).SuccessResponseShouldBe(true);
        (await _workersController.Add(Guid.NewGuid(), new())).CheckResponse<BadRequestObjectResult>();
    }

    [Fact]
    public async void DELETE_DeleteBotFromWorker_Test()
    {
        WWorker w = _workers["http://w1"];
        _workersService.Workers.Add(w);

        (await _workersController.Delete(w.Id, new())).SuccessResponseShouldBe(true);
        (await _workersController.Delete(Guid.NewGuid(), new())).CheckResponse<BadRequestObjectResult>();
    }
}
