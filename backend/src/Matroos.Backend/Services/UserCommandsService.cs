using Matroos.Resources.Classes.Commands;

namespace Matroos.Backend.Services.Interfaces;

public class UserCommandsService : IUserCommandsService
{
    /// <summary>
    /// The user commands.
    /// </summary>
    public List<UserCommand> UserCommands { get; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public UserCommandsService()
    {
        UserCommands = new();
    }

    /// <inheritdoc />
    public bool AddUserCommand(UserCommand userCommand)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool DeleteUserCommand(Guid userCommandId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool UpdateUserCommand(UserCommand userCommand)
    {
        throw new NotImplementedException();
    }
}
