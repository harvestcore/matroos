using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Matroos.Backend.Services;
using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Services;
using Matroos.Resources.Services.Interfaces;
using Matroos.Resources.Tests;

using Microsoft.Extensions.Configuration;

using Moq;

using Xunit;

using WWorker = Matroos.Resources.Classes.Workers.Worker;

namespace Matroos.Backend.Tests.Services;

[Collection("Sequential")]
public class WorkersServiceTests : BaseTest
{
    private readonly IWorkersService _workersService;
    private readonly IBotsService _botsService;
    private readonly ICommunicationService _communicationService;
    private new readonly IConfigurationService _configurationService;

    public WorkersServiceTests() : base()
    {
        _botsService = new BotsService(_dataContextService);
        _configurationService = new ConfigurationService(new ConfigurationBuilder().Build());

        Dictionary<string, WWorker> workers = new()
        {
            { "http://w1", new(Guid.NewGuid(), "http://w1", new()) },
            { "http://w2", new(Guid.NewGuid(), "http://w2", new()) }
        };

        Mock<CommunicationService> csMock = new();
        csMock.Setup(cs => cs.GetWorkerStatus(It.IsAny<string>())).Returns((string url) => Task.FromResult(workers[url]));
        csMock.Setup(cs => cs.StartBotInWorker(It.IsAny<WWorker>(), It.IsAny<Guid>())).Callback(() => { });
        csMock.Setup(cs => cs.AddBotToWorker(It.IsAny<WWorker>(), It.IsAny<Bot>())).Callback(() => { });
        csMock.Setup(cs => cs.UpdateBotInWorker(It.IsAny<WWorker>(), It.IsAny<Bot>())).Callback(() => { });
        csMock.Setup(cs => cs.DeleteBotFromWorker(It.IsAny<WWorker>(), It.IsAny<Guid>())).Callback(() => { });
        _communicationService = csMock.Object;

        _workersService = new WorkersService(_configurationService, _communicationService, _botsService);
    }

    [Fact]
    public void RenewWorkersTests()
    {
        _configurationService.Set("Workers", "http://w1;http://w2");
        Assert.Empty(_workersService.Workers);

        _workersService.RenewWorkers();
        Assert.Equal(2, _workersService.Workers.Count);

        foreach (WWorker worker in _workersService.Workers)
        {
            Assert.True(worker.LastUpdate > DateTime.UtcNow);
        }
    }

    [Fact]
    public void StartBotInWorkerTests()
    {
        WWorker w = new(Guid.NewGuid(), "http://w1", new());
        _workersService.Workers.Add(w);

        Assert.True(_workersService.StartBotInWorker(w.Id, Guid.NewGuid()));
        Assert.False(_workersService.StartBotInWorker(Guid.NewGuid(), Guid.NewGuid()));
    }

    [Fact]
    public void StopBotInWorkerTests()
    {
        WWorker w = new(Guid.NewGuid(), "http://w1", new());
        _workersService.Workers.Add(w);

        Assert.True(_workersService.StopBotInWorker(w.Id, Guid.NewGuid()));
        Assert.False(_workersService.StopBotInWorker(Guid.NewGuid(), Guid.NewGuid()));
    }

    [Fact]
    public async void UpdateBotsInWorkerTests()
    {
        WWorker w = new(Guid.NewGuid(), "http://w1", new());
        _workersService.Workers.Add(w);

        Assert.True(await _workersService.UpdateBotsInWorker(w.Id, new()));
        Assert.False(await _workersService.UpdateBotsInWorker(Guid.NewGuid(), new()));
    }

    [Fact]
    public async void DeleteBotsFromWorkerTests()
    {
        WWorker w = new(Guid.NewGuid(), "http://w1", new());
        _workersService.Workers.Add(w);

        Assert.True(await _workersService.DeleteBotsFromWorker(w.Id, new()));
        Assert.False(await _workersService.DeleteBotsFromWorker(Guid.NewGuid(), new()));
    }
}
