using Matroos.Resources.Classes.API;
using Matroos.Resources.Classes.Bots;
using Matroos.Worker.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using WWorker = Matroos.Resources.Classes.Workers.Worker;

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
    public ActionResult<WWorker> Get()
    {
        List<Bot> bots = _mainService.Bots.Values.ToList();
        return Ok(new WWorker(_mainService.Id, "", bots));
    }

    [HttpPost]
    public ActionResult<SuccessResponse> Add(Bot bot)
    {
        bool result = _mainService.CreateBot(bot);
        return Ok(new SuccessResponse(result));
    }

    [HttpPut]
    public ActionResult<SuccessResponse> Update(Bot bot)
    {
        bool result = _mainService.UpdateBot(bot);
        return Ok(new SuccessResponse(result));
    }

    [HttpDelete("{botId}")]
    public ActionResult<SuccessResponse> Delete(Guid botId)
    {
        bool result = _mainService.DestroyBot(botId);
        return Ok(new SuccessResponse(result));
    }

    [HttpGet("start/{botId}")]
    public ActionResult<SuccessResponse> Start(Guid botId)
    {
        bool result = _mainService.StartBot(botId);
        return Ok(new SuccessResponse(result));
    }

    [HttpGet("stop/{botId}")]
    public ActionResult<SuccessResponse> Stop(Guid botId)
    {
        bool result = _mainService.StopBot(botId);
        return Ok(new SuccessResponse(result));
    }
}