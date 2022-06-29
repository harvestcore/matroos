using System.Text.Json.Serialization;

using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.API;
using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Classes.Mappers;

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
    public async Task<ActionResult<ItemsResponse<Bot>>> GetAll()
    {
        List<Bot> bots = await _botsService.GetAll();
        return Ok(new ItemsResponse<Bot>(bots));
    }

    /// <summary>
    /// Get a single bot by its identifier.
    /// </summary>
    /// <param name="botId">The bot identifier.</param>
    /// <returns>The bot found.</returns>
    [HttpGet("{botId}")]
    public async Task<ActionResult<Bot>> Get(Guid botId)
    {
        Bot? bot = await _botsService.Get(botId);
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
    public async Task<ActionResult<SuccessResponse>> Post([FromBody] Bot bot)
    {
        (bool result, _) = await _botsService.AddBot(bot);
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
    public async Task<ActionResult<SuccessResponse>> Put([FromBody] Bot bot)
    {
        bool result = await _botsService.UpdateBot(bot);
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
    public async Task<ActionResult<SuccessResponse>> Delete(Guid botId)
    {
        bool result = await _botsService.DeleteBot(botId);
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
