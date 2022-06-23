using Microsoft.AspNetCore.Mvc;

using Matroos.Resources.Classes.API;
using Matroos.Worker.Services.Interfaces;
using Matroos.Resources.Classes.Bots;

namespace Matroos.Worker.Controllers;

[ApiController]
[Route("")]
public class MainController : ControllerBase
{
    private readonly IMainService _mainService;

    public MainController(IMainService mainService)
    {
        _mainService = mainService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        List<Bot> bots = _mainService.Bots.Values.ToList();
        return Ok(new Resources.Classes.Workers.Worker(_mainService.Id, "", bots));
    }

    [HttpPost]
    public IActionResult Add(Bot bot)
    {
        bool result = _mainService.CreateBot(bot);
        return Ok(new SuccessResponse(result));
    }

    [HttpPut]
    public IActionResult Update(Bot bot)
    {
        bool result = _mainService.UpdateBot(bot);
        return Ok(new SuccessResponse(result));
    }

    [HttpDelete("{botId}")]
    public IActionResult Delete(Guid botId)
    {
        bool result = _mainService.DestroyBot(botId);
        return Ok(new SuccessResponse(result));
    }

    [HttpGet("start/{botId}")]
    public IActionResult Start(Guid botId)
    {
        bool result = _mainService.StartBot(botId);
        return Ok(new SuccessResponse(result));
    }

    [HttpGet("stop/{botId}")]
    public IActionResult Stop(Guid botId)
    {
        bool result = _mainService.StopBot(botId);
        return Ok(new SuccessResponse(result));
    }
}