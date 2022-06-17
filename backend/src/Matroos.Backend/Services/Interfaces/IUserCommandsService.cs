using Matroos.Resources.Classes.Commands;

namespace Matroos.Backend.Services;

public interface IUserCommandsService
{
    /// <summary>
    /// Add a new user command.
    /// </summary>
    /// <param name="userCommand">The user command to be added.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public bool AddUserCommand(UserCommand userCommand);

    /// <summary>
    /// Update the data of a command.
    /// </summary>
    /// <param name="userCommand">The user command with the updated data.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public bool UpdateUserCommand(UserCommand userCommand);

    /// <summary>
    /// Delete a command.
    /// </summary>
    /// <param name="userCommandId">The identifier of the user command to be removed.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public bool DeleteUserCommand(Guid userCommandId);
}
