using System.Linq.Expressions;

using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.Commands;
using Matroos.Resources.Services.Interfaces;

namespace Matroos.Backend.Services;

public class UserCommandsService : IUserCommandsService
{
    /// <summary>
    /// The data context service.
    /// </summary>
    private readonly IDataContextService _dataContextService;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public UserCommandsService(IDataContextService dataContextService)
    {
        _dataContextService = dataContextService;
    }

    /// <inheritdoc />
    public async Task<UserCommand?> Get(Guid userCommandId)
    {
        return await _dataContextService.Get<UserCommand>(userCommandId);
    }

    /// <inheritdoc />
    public async Task<List<UserCommand>> GetAll()
    {
        return await _dataContextService.GetAll<UserCommand>();
    }

    /// <inheritdoc />
    public async Task<List<UserCommand>> Filter(Expression<Func<UserCommand, bool>> filter)
    {
        return await _dataContextService.Filter(filter);
    }

    /// <inheritdoc />
    public async Task<(bool, Guid)> AddUserCommand(UserCommand userCommand)
    {
        List<UserCommand> filtered = await _dataContextService.Filter<UserCommand>(command =>
            command.Id == userCommand.Id ||
            command.Name == userCommand.Name ||
            command.Trigger == userCommand.Trigger
        );

        if (filtered.Count > 0)
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

        bool result = await _dataContextService.Insert(uc);

        return (result, result ? uc.Id : Guid.Empty);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteUserCommand(Guid userCommandId)
    {
        return await _dataContextService.Delete<UserCommand>(userCommandId);
    }

    /// <inheritdoc />
    public async Task<bool> UpdateUserCommand(UserCommand userCommand)
    {
        UserCommand? found = await _dataContextService.Get<UserCommand>(userCommand.Id);
        if (found == null)
        {
            return false;
        }

        // Update the command.
        found.Update(userCommand);

        // Push the changes to DB.
        return await _dataContextService.Update(found.Id, found);
    }
}
