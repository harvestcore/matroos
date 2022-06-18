using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.Commands;

namespace Matroos.Backend.Services;

public class UserCommandsService : IUserCommandsService
{
    /// <summary>
    /// The user commands.
    /// </summary>
    public List<UserCommand> UserCommands { get; }

    /// <summary>
    /// The bots service.
    /// </summary>
    private readonly IBotsService _botsService;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public UserCommandsService(IBotsService botsService)
    {
        UserCommands = new();
        _botsService = botsService;
    }

    /// <inheritdoc />
    public UserCommand? GetById(Guid userCommandId)
    {
        return UserCommands.Find(command => command.Id == userCommandId);
    }

    /// <inheritdoc />
    public (bool, Guid) AddUserCommand(UserCommand userCommand)
    {
        if (UserCommands.Any(command =>
            command.Id == userCommand.Id ||
            command.Name == userCommand.Name ||
            command.Trigger == userCommand.Trigger
        ))
        {
            return (false, Guid.Empty);
        }

        UserCommand uc = new(
            name: userCommand.Name,
            description: userCommand.Description,
            trigger: userCommand.Trigger,
            parameters: userCommand.Parameters,
            commandType: userCommand.Type,
            commandMode: userCommand.Mode
        );

        UserCommands.Add(uc);

        return (true, uc.Id);
    }

    /// <inheritdoc />
    public bool DeleteUserCommand(Guid userCommandId)
    {
        UserCommand? commandFound = GetById(userCommandId);

        if (commandFound == null)
        {
            return false;
        }

        foreach (Resources.Classes.Bots.Bot? bot in _botsService.Bots)
        {
            bot.Commands.Remove(userCommandId);
        }

        return UserCommands.Remove(commandFound);
    }

    /// <inheritdoc />
    public bool UpdateUserCommand(UserCommand userCommand)
    {
        UserCommand? commandFound = GetById(userCommand.Id);

        if (commandFound == null)
        {
            return false;
        }

        commandFound.Update(userCommand);

        return true;
    }
}
