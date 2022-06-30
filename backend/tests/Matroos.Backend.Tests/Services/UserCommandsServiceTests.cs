using System;

using Matroos.Backend.Services;
using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.Commands;
using Matroos.Resources.Tests;

using Xunit;

namespace Matroos.Backend.Tests.Services;

[Collection("Sequential")]
public class UserCommandsServiceTests : BaseTest
{
    private readonly IUserCommandsService _userCommandsService;

    public UserCommandsServiceTests() : base()
    {
        _userCommandsService = new UserCommandsService(_dataContextService);
    }

    [Theory]
    [InlineData("name-a", "!", CommandType.VERSION, CommandMode.SINGLE)]
    [InlineData("name-b", "!", CommandType.VERSION, CommandMode.SINGLE)]
    public async void AddSomeCommands(string name, string trigger, CommandType type, CommandMode mode)
    {
        await EmptyCollection<UserCommand>();
        UserCommand uc = new(name, "", trigger, new(), type, mode);

        Assert.True((await _userCommandsService.AddUserCommand(uc)).Item1);
    }

    [Fact]
    public async void AddDuplicatedCommands()
    {
        await EmptyCollection<UserCommand>();
        UserCommand a = new("duplicated", "duplicated", "duplicated", new(), CommandType.VERSION, CommandMode.SINGLE);

        Assert.True((await _userCommandsService.AddUserCommand(a)).Item1);
        Assert.False((await _userCommandsService.AddUserCommand(a)).Item1);
    }

    [Theory]
    [InlineData("a", "!", CommandType.VERSION, CommandMode.SINGLE)]
    [InlineData("b", "!", CommandType.VERSION, CommandMode.SINGLE, true)]
    public async void DeleteCommands(string name, string trigger, CommandType type, CommandMode mode, bool shouldFail = false)
    {
        await EmptyCollection<UserCommand>();
        UserCommand uc = new(name, "", trigger, new(), type, mode);

        if (shouldFail)
        {
            Assert.False(await _userCommandsService.DeleteUserCommand(uc.Id));
            return;
        }

        (bool success, Guid commandId) = await _userCommandsService.AddUserCommand(uc);
        Assert.True(success);
        Assert.True(await _userCommandsService.DeleteUserCommand(commandId));
    }

    [Fact]
    public async void UpdateCommand()
    {
        await EmptyCollection<UserCommand>();
        UserCommand? uc = new("a", "", "!", new(), CommandType.VERSION, CommandMode.SINGLE);

        (bool _, Guid commandId) = await _userCommandsService.AddUserCommand(uc);

        // Non existant command.
        Assert.False(await _userCommandsService.UpdateUserCommand(uc));

        uc = await _userCommandsService.Get(commandId);
        Assert.NotNull(uc);

        if (uc != null)
        {
            uc.Name = "another name";
            uc.Trigger = ">";
            Assert.True(await _userCommandsService.UpdateUserCommand(uc));
        }

        uc = await _userCommandsService.Get(commandId);
        Assert.NotNull(uc);

        if (uc != null)
        {
            Assert.Equal("another name", uc.Name);
            Assert.Equal(">", uc.Trigger);
        }
    }
}
