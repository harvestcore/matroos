using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.API;
using Matroos.Resources.Classes.Workers;

using Microsoft.AspNetCore.Mvc;

namespace Matroos.Backend.Controllers;

[ApiController]
[Route("workers")]
public class WorkersController : ControllerBase
{
    /// <summary>
    /// The workers service.
    /// </summary>
    private readonly IWorkersService _workersService;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="workersService">The <see cref="IWorkersService"/>.</param>
    public WorkersController(IWorkersService workersService)
    {
        _workersService = workersService;
    }

    /// <summary>
    /// Start a bot in a worker.
    /// </summary>
    /// <param name="workerId">The worker identifier.</param>
    /// <param name="botId">The bot identifier.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    [HttpGet("{workerId}/start/{botId}")]
    public ActionResult<SuccessResponse> Start(Guid workerId, Guid botId)
    {
        bool result = _workersService.StartBotInWorker(workerId, botId);
        if (!result)
        {
            return BadRequest(new
            {
                msg = "Start operation failed."
            });
        }

        return Ok(new SuccessResponse(result));
    }

    /// <summary>
    /// Stop a bot in a worker.
    /// </summary>
    /// <param name="workerId">The worker identifier.</param>
    /// <param name="botId">The bot identifier.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    [HttpGet("{workerId}/stop/{botId}")]
    public ActionResult<SuccessResponse> Stop(Guid workerId, Guid botId)
    {
        bool result = _workersService.StopBotInWorker(workerId, botId);
        if (!result)
        {
            return BadRequest(new
            {
                msg = "Stop operation failed."
            });
        }

        return Ok(new SuccessResponse(result));
    }

    /// <summary>
    /// Get all the workers.
    /// </summary>
    /// <returns>The workers.</returns>
    [HttpGet]
    public ActionResult<ItemsResponse<Worker>> Get()
    {
        return Ok(new ItemsResponse<Worker>(_workersService.Workers));
    }

    /// <summary>
    /// Get a single worker.
    /// </summary>
    /// <param name="workerId">The worker identifier.</param>
    /// <returns>The worker found.</returns>
    [HttpGet("{workerId}")]
    public ActionResult<Worker> Get(Guid workerId)
    {
        Worker? workerFound = _workersService.Workers.FirstOrDefault(w => w.Id == workerId);
        if (workerFound == null)
        {
            return NotFound();
        }

        return Ok(workerFound);
    }

    /// <summary>
    /// Add bots to a worker.
    /// </summary>
    /// <param name="workerId">The worker identifier.</param>
    /// <param name="botIds">A list containing the list of workers to be added.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    [HttpPost("{workerId}")]
    public async Task<ActionResult<SuccessResponse>> Add(Guid workerId, [FromBody] List<Guid> botIds)
    {
        bool result = await _workersService.AddBotsToWorker(workerId, botIds);
        if (!result)
        {
            return BadRequest(new
            {
                msg = "Creation failed."
            });
        }

        return Ok(new SuccessResponse(result));
    }

    /// <summary>
    /// Delete bots from a worker.
    /// </summary>
    /// <param name="workerId">The worker identifier.</param>
    /// <param name="botIds">A list containing the list of workers to be deleted.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    [HttpDelete("{workerId}")]
    public async Task<ActionResult<SuccessResponse>> Delete(Guid workerId, [FromBody] List<Guid> botIds)
    {
        bool result = await _workersService.DeleteBotsFromWorker(workerId, botIds);
        if (!result)
        {
            return BadRequest(new
            {
                msg = "Deletion failed."
            });
        }

        return Ok(new SuccessResponse(result));
    }
}
