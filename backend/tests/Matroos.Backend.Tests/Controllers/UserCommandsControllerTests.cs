using System;
using System.Collections.Generic;
using System.Linq;

using Matroos.Backend.Controllers;
using Matroos.Backend.Services;
using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.API;
using Matroos.Resources.Classes.Commands;
using Matroos.Resources.Classes.Commands.Utilities;
using Matroos.Resources.Tests;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace Matroos.Worker.Tests.Controllers;

public class UserCommandsControllerTests
{
    private readonly UserCommandsController _userCommandsController;
    private readonly IUserCommandsService _userCommandsService;
    private readonly IBotsService _botsService;

    public UserCommandsControllerTests()
    {
        _botsService = new BotsService();
        _userCommandsService = new UserCommandsService(_botsService);
        _userCommandsController = new UserCommandsController(_userCommandsService);
    }

    [Fact]
    public void GET_Signatures_Test()
    {
        // Actual command signatures.
        IEnumerable<CommandType>? commandEnumTypes = Enum.GetValues(typeof(CommandType)).Cast<CommandType>();
        if (commandEnumTypes == null)
        {
            throw new Exception("There are no CommandType enum values somehow.");
        }

        ActionResult<ItemsResponse<CommandSignature>>? res = _userCommandsController.GetSignatures();
        OkObjectResult? response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        ItemsResponse<CommandSignature>? commandSignatures = response?.Value as ItemsResponse<CommandSignature>;
        Assert.NotEmpty(commandSignatures?.Items ?? null);
        Assert.Equal(commandEnumTypes.Count(), commandSignatures?.Count);

        List<CommandType> commandTypes = commandEnumTypes.ToList();
        foreach (CommandSignature item in commandSignatures?.Items ?? new())
        {
            Assert.Contains(commandTypes, ct => ct == item.Type);
            Assert.NotNull(item.Signature);
            Assert.NotNull(item.AllowedModes);
        }
    }

    [Fact]
    public void GET_GetAll_Test()
    {
        // 0 commands.
        ActionResult<ItemsResponse<UserCommand>>? res = _userCommandsController.GetAll();
        OkObjectResult? response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        ItemsResponse<UserCommand>? items = response?.Value as ItemsResponse<UserCommand>;
        Assert.Empty(items?.Items ?? null);
        Assert.Equal(0, items?.Count);

        // 2 commands.
        _userCommandsService.AddUserCommand(new("a", "", "!", new(), CommandType.VERSION, CommandMode.SINGLE));
        _userCommandsService.AddUserCommand(new("a2", "2", "!2", new(), CommandType.VERSION, CommandMode.SINGLE));
        res = _userCommandsController.GetAll();
        response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        items = response?.Value as ItemsResponse<UserCommand>;
        Assert.NotEmpty(items?.Items ?? null);
        Assert.Equal(2, items?.Count);
    }

    [Fact]
    public void GET_GetSingle_Test()
    {
        UserCommand command = new("a", "", "!", new(), CommandType.VERSION, CommandMode.SINGLE);
        (_, Guid commandId) = _userCommandsService.AddUserCommand(command);

        ActionResult<UserCommand>? res = _userCommandsController.Get(commandId);
        OkObjectResult? response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        UserCommand? b = response?.Value as UserCommand;
        Assert.NotNull(b);

        // Non-existent bot.
        res = _userCommandsController.Get(Guid.NewGuid());
        NotFoundResult? nfResult = res.Result as NotFoundResult;
        Assert.Equal(404, nfResult?.StatusCode ?? 0);
    }

    [Fact]
    public void POST_AddUserCommand_Test()
    {
        UserCommand uc = new("a", "", "!", new(), CommandType.VERSION, CommandMode.SINGLE);
        _userCommandsController.Post(uc).SuccessResponseShouldBe(true);


        UserCommand? uc2 = new("a2", "2", "!2", new(), CommandType.VERSION, CommandMode.SINGLE);
        (_, Guid commandId) = _userCommandsService.AddUserCommand(uc2);
        uc2 = _userCommandsService.UserCommands.Find(c => c.Id == commandId);

        if (uc2 == null)
        {
            throw new Exception("The user command is somehow null");
        }

        _userCommandsController.Post(uc2).CheckResponse<BadRequestObjectResult>();
    }

    [Fact]
    public void PUT_UpdateUserCommand_Test()
    {
        UserCommand? uc = new("a", "", "!", new(), CommandType.VERSION, CommandMode.SINGLE);
        (_, Guid commandId) = _userCommandsService.AddUserCommand(uc);
        uc = _userCommandsService.UserCommands.Find(c => c.Id == commandId);

        if (uc == null)
        {
            throw new Exception("The bot is somehow null");
        }
        _userCommandsController.Put(uc).SuccessResponseShouldBe(true);

        UserCommand uc2 = new("a2", "2", "!2", new(), CommandType.VERSION, CommandMode.SINGLE);
        _userCommandsController.Put(uc2).CheckResponse<BadRequestObjectResult>();
    }

    [Fact]
    public void DELETE_DeleteUserCommand_Test()
    {
        UserCommand? uc = new("a", "", "!", new(), CommandType.VERSION, CommandMode.SINGLE);
        (_, Guid commandId) = _userCommandsService.AddUserCommand(uc);

        _userCommandsController.Delete(commandId).SuccessResponseShouldBe(true);

        // Non-existent bot.
        _userCommandsController.Delete(Guid.NewGuid()).CheckResponse<BadRequestObjectResult>();
    }
}
