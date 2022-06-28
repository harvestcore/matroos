using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.API;
using Matroos.Resources.Classes.Bots;

using Microsoft.AspNetCore.Mvc;

namespace Matroos.Backend.Controllers;

[ApiController]
[Route("bots")]
public class BotsController : ControllerBase
{
    /// <summary>
    /// The bots service.
    /// </summary>
    private readonly IBotsService _botsService;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="botsService">The <see cref="IBotsService"/>.</param>
    public BotsController(IBotsService botsService)
    {
        _botsService = botsService;
    }

    /// <summary>
    /// Get all the bots.
    /// </summary>
    /// <returns>The bots.</returns>
    [HttpGet]
    public ActionResult<ItemsResponse<Bot>> GetAll()
    {
        return Ok(new ItemsResponse<Bot>(_botsService.Bots));
    }

    /// <summary>
    /// Get a single bot by its identifier.
    /// </summary>
    /// <param name="botId">The bot identifier.</param>
    /// <returns>The bot found.</returns>
    [HttpGet("{botId}")]
    public ActionResult<Bot> Get(Guid botId)
    {
        Bot? bot = _botsService.Bots.Find(bot => bot.Id == botId);
        if (bot == null)
        {
            return NotFound();
        }

        return Ok(bot);
    }

    /// <summary>
    /// Add a bot.
    /// </summary>
    /// <param name="bot">The bot to be added.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    [HttpPost]
    public ActionResult<SuccessResponse> Post([FromBody] Bot bot)
    {
        (bool result, _) = _botsService.AddBot(bot);
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
    /// Update a bot.
    /// </summary>
    /// <param name="bot">The bot to be updated.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    [HttpPut]
    public ActionResult<SuccessResponse> Put([FromBody] Bot bot)
    {
        bool result = _botsService.UpdateBot(bot);
        if (!result)
        {
            return BadRequest(new
            {
                msg = "Update failed."
            });
        }

        return Ok(new SuccessResponse(result));
    }

    /// <summary>
    /// Delete a bot.
    /// </summary>
    /// <param name="botId">The bot identifier.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    [HttpDelete("{botId}")]
    public ActionResult<SuccessResponse> Delete(Guid botId)
    {
        bool result = _botsService.DeleteBot(botId);
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
