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

[Collection("Sequential")]
public class UserCommandsControllerTests : BaseTest
{
    private readonly UserCommandsController _userCommandsController;
    private readonly IUserCommandsService _userCommandsService;

    public UserCommandsControllerTests() : base()
    {
        _userCommandsService = new UserCommandsService(_dataContextService);
        _userCommandsController = new UserCommandsController(_userCommandsService);
    }

    [Fact]
    public async void GET_Signatures_Test()
    {
        await EmptyCollection<UserCommand>();
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
    public async void GET_GetAll_Test()
    {
        await EmptyCollection<UserCommand>();
        // 0 commands.
        ActionResult<ItemsResponse<UserCommand>>? res = await _userCommandsController.GetAll();
        OkObjectResult? response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        ItemsResponse<UserCommand>? items = response?.Value as ItemsResponse<UserCommand>;
        Assert.Empty(items?.Items ?? null);
        Assert.Equal(0, items?.Count);

        // 2 commands.
        await _userCommandsService.AddUserCommand(new("super-command-1", "", "super-trigger-1", new(), CommandType.VERSION, CommandMode.SINGLE));
        await _userCommandsService.AddUserCommand(new("super-command-2", "", "super-trigger-2", new(), CommandType.VERSION, CommandMode.SINGLE));
        res = await _userCommandsController.GetAll();
        response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        items = response?.Value as ItemsResponse<UserCommand>;
        Assert.NotEmpty(items?.Items ?? null);
        Assert.Equal(2, items?.Count);
    }

    [Fact]
    public async void GET_GetSingle_Test()
    {
        await EmptyCollection<UserCommand>();
        UserCommand command = new("a", "", "!", new(), CommandType.VERSION, CommandMode.SINGLE);
        (_, Guid commandId) = await _userCommandsService.AddUserCommand(command);

        ActionResult<UserCommand>? res = await _userCommandsController.Get(commandId);
        OkObjectResult? response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        UserCommand? b = response?.Value as UserCommand;
        Assert.NotNull(b);

        // Non-existent bot.
        res = await _userCommandsController.Get(Guid.NewGuid());
        NotFoundResult? nfResult = res.Result as NotFoundResult;
        Assert.Equal(404, nfResult?.StatusCode ?? 0);
    }

    [Fact]
    public async void POST_AddUserCommand_Test()
    {
        await EmptyCollection<UserCommand>();
        UserCommand uc = new("a", "", "!", new(), CommandType.VERSION, CommandMode.SINGLE);
        (await _userCommandsController.Post(uc)).SuccessResponseShouldBe(true);


        UserCommand? uc2 = new("a2", "2", "!2", new(), CommandType.VERSION, CommandMode.SINGLE);
        (_, Guid commandId) = await _userCommandsService.AddUserCommand(uc2);
        uc2 = await _userCommandsService.Get(commandId);

        if (uc2 == null)
        {
            throw new Exception("The user command is somehow null");
        }

        (await _userCommandsController.Post(uc2)).CheckResponse<BadRequestObjectResult>();
    }

    [Fact]
    public async void PUT_UpdateUserCommand_Test()
    {
        await EmptyCollection<UserCommand>();
        UserCommand? uc = new("a", "", "!", new(), CommandType.VERSION, CommandMode.SINGLE);
        (_, Guid commandId) = await _userCommandsService.AddUserCommand(uc);
        uc = await _userCommandsService.Get(commandId);

        if (uc == null)
        {
            throw new Exception("The bot is somehow null");
        }
        (await _userCommandsController.Put(uc)).SuccessResponseShouldBe(true);

        UserCommand uc2 = new("a2", "2", "!2", new(), CommandType.VERSION, CommandMode.SINGLE);
        (await _userCommandsController.Put(uc2)).CheckResponse<BadRequestObjectResult>();
    }

    [Fact]
    public async void DELETE_DeleteUserCommand_Test()
    {
        await EmptyCollection<UserCommand>();
        UserCommand? uc = new("a", "", "!", new(), CommandType.VERSION, CommandMode.SINGLE);
        (_, Guid commandId) = await _userCommandsService.AddUserCommand(uc);

        (await _userCommandsController.Delete(commandId)).SuccessResponseShouldBe(true);

        // Non-existent bot.
        (await _userCommandsController.Delete(Guid.NewGuid())).CheckResponse<BadRequestObjectResult>();
    }
}
