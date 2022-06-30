using Discord.WebSocket;

using Matroos.Resources.Attributes;
using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Extensions;

namespace Matroos.Resources.Classes.Commands;

public static class CommandHelper
{
    /// <summary>
    /// Run a <see cref="UserCommand"/>.
    /// </summary>
    /// <param name="client">The Discord client.</param>
    /// <param name="message">The Discord message.</param>
    /// <param name="message">The bot.</param>
    /// <param name="command">The user command to be run.</param>
    public static void RunCommand(DiscordShardedClient client, SocketMessage message, Bot bot, UserCommand command)
    {
        // Get the command attribute.
        CommandAttribute attribute = command.Type.GetAttribute<CommandAttribute>();
        Type commandType = attribute.Command;

        if (commandType == null)
        {
            throw new InvalidDataException("Missing command type");
        }

        // Generate an instance from the command type.
        object? generatedInstance = Activator.CreateInstance(attribute.Command);


        // Invoke the "Run" method to run the command.
        commandType
            .GetMethod("Run")
            ?.Invoke(generatedInstance, new object[] { client, message, bot, command });
    }
}
