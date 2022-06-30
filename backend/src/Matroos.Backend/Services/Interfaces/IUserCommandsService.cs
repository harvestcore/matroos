using System.Linq.Expressions;

using Matroos.Resources.Classes.Commands;

namespace Matroos.Backend.Services.Interfaces;

public interface IUserCommandsService
{
    /// <summary>
    /// Get a User Command by its identifier.
    /// </summary>
    /// <param name="userCommandId">The User Command identifier.</param>
    /// <returns>The user command found or null.</returns>
    public Task<UserCommand?> Get(Guid userCommandId);

    /// <summary>
    /// Get all the user commands.
    /// </summary>
    /// <returns>A list containing all the user commands.</returns>
    public Task<List<UserCommand>> GetAll();

    /// <summary>
    /// Get all the user commands that match the filter.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <returns>A list containing all the user commands found.</returns>
    public Task<List<UserCommand>> Filter(Expression<Func<UserCommand, bool>> filter);

    /// <summary>
    /// Add a new user command.
    /// </summary>
    /// <param name="userCommand">The user command to be added.</param>
    /// <returns>Whether the operation was successful or not, and the identifier of the created User Command.</returns>
    public Task<(bool, Guid)> AddUserCommand(UserCommand userCommand);

    /// <summary>
    /// Update the data of a command.
    /// </summary>
    /// <param name="userCommand">The user command with the updated data.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public Task<bool> UpdateUserCommand(UserCommand userCommand);

    /// <summary>
    /// Delete a command.
    /// </summary>
    /// <param name="userCommandId">The identifier of the user command to be removed.</param>
    /// <returns>Whether the operation was successful or not.</returns>
    public Task<bool> DeleteUserCommand(Guid userCommandId);
}
