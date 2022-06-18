﻿using System;

using Matroos.Backend.Services;
using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.Commands;

using Xunit;

namespace Matroos.Backend.Tests.Services;

public class UserCommandsServiceTests
{
    private readonly IUserCommandsService _userCommandsService;
    private readonly IBotsService _botsService;

    public UserCommandsServiceTests()
    {
        _botsService = new BotsService();
        _userCommandsService = new UserCommandsService(_botsService);
    }

    [Theory]
    [InlineData("a", "!", CommandType.MESSAGE, CommandMode.SCOPED)]
    [InlineData("b", "!", CommandType.MESSAGE, CommandMode.SCOPED)]
    public void AddSomeCommands(string name, string trigger, CommandType type, CommandMode mode)
    {
        UserCommand uc = new(name, "", trigger, new(), type, mode);

        Assert.True(_userCommandsService.AddUserCommand(uc).Item1);
    }

    [Fact]
    public void AddDuplicatedCommands()
    {
        UserCommand a = new("a", "", "!", new(), CommandType.MESSAGE, CommandMode.SCOPED);

        Assert.True(_userCommandsService.AddUserCommand(a).Item1);
        Assert.False(_userCommandsService.AddUserCommand(a).Item1);
    }

    [Theory]
    [InlineData("a", "!", CommandType.MESSAGE, CommandMode.SCOPED)]
    [InlineData("b", "!", CommandType.MESSAGE, CommandMode.SCOPED, true)]
    public void DeleteCommands(string name, string trigger, CommandType type, CommandMode mode, bool shouldFail = false)
    {
        UserCommand uc = new(name, "", trigger, new(), type, mode);

        if (shouldFail)
        {
            Assert.False(_userCommandsService.DeleteUserCommand(uc.Id));
            return;
        }

        (bool success, Guid commandId) = _userCommandsService.AddUserCommand(uc);
        Assert.True(success);
        Assert.True(_userCommandsService.DeleteUserCommand(commandId));
    }

    [Fact]
    public void UpdateCommand()
    {
        UserCommand? uc = new("a", "", "!", new(), CommandType.MESSAGE, CommandMode.SCOPED);

        (bool _, Guid commandId) = _userCommandsService.AddUserCommand(uc);

        // Non existant command.
        Assert.False(_userCommandsService.UpdateUserCommand(uc));

        uc = _userCommandsService.GetById(commandId);

        uc.Name = "another name";
        uc.Trigger = ">";

        Assert.True(_userCommandsService.UpdateUserCommand(uc));

        uc = _userCommandsService.GetById(commandId);
        Assert.Equal("another name", uc.Name);
        Assert.Equal(">", uc.Trigger);

    }
}
