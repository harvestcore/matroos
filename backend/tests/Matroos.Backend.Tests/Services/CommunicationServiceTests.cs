using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using Matroos.Backend.Services;
using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Classes.Workers;
using Matroos.Resources.Services;
using Matroos.Resources.Services.Interfaces;

using Microsoft.Extensions.Configuration;
using Moq;

using Newtonsoft.Json;

using Xunit;

namespace Matroos.Backend.Tests.Services;

public class CommunicationServiceTests
{
    private readonly string WORKER_URL = "http://worker";
    private readonly ICommunicationService _communicationService;
    private readonly Worker _worker;

    public CommunicationServiceTests()
    {
        _worker = new Worker(Guid.NewGuid(), WORKER_URL, new());

        Mock<CommunicationService> csMock = new();
        csMock
            .Setup(cs => cs.Request(It.IsAny<HttpMethod>(), It.IsAny<string>(), It.IsAny<object>()))
            .Returns((HttpMethod method, string uri, object payload) =>
            {
                if (uri.Equals(WORKER_URL) && method == HttpMethod.Get)
                {
                    string json = JsonConvert.SerializeObject(_worker);
                    StringContent content = new(json, Encoding.UTF8, "application/json");

                    return Task.FromResult(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = content
                    });
                }

                return Task.FromResult(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK
                });
            }
        );

        _communicationService = csMock.Object;
    }

    [Fact]
    public async void RenewWorkersTests()
    {        
        Exception? exception = await Record.ExceptionAsync(async () =>
            await _communicationService.GetWorkerStatus(WORKER_URL)
        );
        Assert.Null(exception);
    }

    [Fact]
    public async void AddBotToWorkerTests()
    {
        Exception? exception = await Record.ExceptionAsync(async () =>
            await _communicationService.AddBotToWorker(_worker, new("name", "desc", "pref", "key", new()))
        );
        Assert.Null(exception);
    }

    [Fact]
    public async void UpdateBotInWorkerTests()
    {
        Exception? exception = await Record.ExceptionAsync(async () =>
            await _communicationService.UpdateBotInWorker(_worker, new("name", "desc", "pref", "key", new()))
        );
        Assert.Null(exception);
    }

    [Fact]
    public async void DeleteBotFromWorkerTests()
    {
        Exception? exception = await Record.ExceptionAsync(async () =>
            await _communicationService.DeleteBotFromWorker(_worker, Guid.NewGuid())
        );
        Assert.Null(exception);
    }

    [Fact]
    public async void StartBotInWorkerTests()
    {
        Exception? exception = await Record.ExceptionAsync(async () =>
            await _communicationService.StartBotInWorker(_worker, Guid.NewGuid())
        );
        Assert.Null(exception);
    }

    [Fact]
    public async void StopBotInWorkerTests()
    {
        Exception? exception = await Record.ExceptionAsync(async () =>
            await _communicationService.StopBotInWorker(_worker, Guid.NewGuid())
        );
        Assert.Null(exception);
    }
}
