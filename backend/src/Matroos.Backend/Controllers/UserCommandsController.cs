using System.Reflection;

using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Attributes;
using Matroos.Resources.Classes.API;
using Matroos.Resources.Classes.Commands;
using Matroos.Resources.Classes.Commands.Utilities;
using Matroos.Resources.Extensions;

using Microsoft.AspNetCore.Mvc;

namespace Matroos.Backend.Controllers;

[ApiController]
[Route("commands")]
public class UserCommandsController : ControllerBase
{
    /// <summary>
    /// The user commands service.
    /// </summary>
    private readonly IUserCommandsService _userCommandsService;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="userCommandsService">The <see cref="IUserCommandsService"/>.</param>
    public UserCommandsController(IUserCommandsService userCommandsService)
    {
        _userCommandsService = userCommandsService;
    }

    /// <summary>
    /// Get the available commands signatures.
    /// </summary>
    /// <returns>The command signatures.</returns>
    [HttpGet]
    [Route("signatures")]
    public ActionResult<ItemsResponse<CommandSignature>> GetSignatures()
    {
        List<CommandSignature> signatures = new();

        // Get all command types.
        IEnumerable<CommandType>? commandEnumTypes = Enum.GetValues(typeof(CommandType)).Cast<CommandType>();

        // Iterate all available command types.
        foreach (CommandType commandEnumType in commandEnumTypes)
        {
            // Get the "Command" attribute.
            CommandAttribute attribute = commandEnumType.GetAttribute<CommandAttribute>();
            Type commandType = attribute.Command;
            if (commandType == null)
            {
                throw new InvalidDataException("Missing UserCommand 'command' attribute.");
            }

            // Generate an instance of the command type.
            object? generatedInstance = Activator.CreateInstance(attribute.Command);

            // Get some properties from the object.
            MemberInfo? parametersInfo = commandType.GetMember("Parameters").FirstOrDefault();
            MemberInfo? allowedModesInfo = commandType.GetMember("AllowedModes").FirstOrDefault();
            if (parametersInfo == null || allowedModesInfo == null)
            {
                continue;
            }

            object? signature = ((PropertyInfo)parametersInfo).GetValue(generatedInstance);
            object? allowedModes = ((PropertyInfo)allowedModesInfo).GetValue(generatedInstance);

            if (signature == null || allowedModes == null)
            {
                continue;
            }

            signatures.Add(new CommandSignature(commandEnumType, (List<ParameterSignature>)signature, (List<CommandMode>)allowedModes));
        }

        // Return the command signatures.
        return Ok(new ItemsResponse<CommandSignature>(signatures));
    }

    /// <summary>
    /// Get all the user commands.
    /// </summary>
    /// <returns>The user commands.</returns>
    [HttpGet]
    public ActionResult<ItemsResponse<UserCommand>> GetAll()
    {
        return Ok(new ItemsResponse<UserCommand>(_userCommandsService.UserCommands));
    }

    /// <summary>
    /// Get a single user command by its identifier.
    /// </summary>
    /// <param name="userCommandId">The user command identifier.</param>
    /// <returns>The user command.</returns>
    [HttpGet("{userCommandId}")]
    public ActionResult<UserCommand> Get(Guid userCommandId)
    {
        UserCommand? userCommand = _userCommandsService.UserCommands.Find(userCommand => userCommand.Id == userCommandId);
        if (userCommand == null)
        {
            return NotFound();
        }

        return Ok(userCommand);
    }

    /// <summary>
    /// Add a user command.
    /// </summary>
    /// <param name="userCommand">The user command to be added.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    [HttpPost]
    public ActionResult<SuccessResponse> Post([FromBody] UserCommand userCommand)
    {
        (bool result, _) = _userCommandsService.AddUserCommand(userCommand);
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
    /// Update a user command.
    /// </summary>
    /// <param name="userCommand">The user command to be updated.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    [HttpPut]
    public ActionResult<SuccessResponse> Put([FromBody] UserCommand userCommand)
    {
        bool result = _userCommandsService.UpdateUserCommand(userCommand);
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
    /// Delete a user command.
    /// </summary>
    /// <param name="userCommandId">The user command identifier.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    [HttpDelete("{userCommandId}")]
    public ActionResult<SuccessResponse> Delete(Guid userCommandId)
    {
        bool result = _userCommandsService.DeleteUserCommand(userCommandId);
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
