using Discord.WebSocket;

namespace Matroos.Resources.Classes.Commands.CustomCommands;

public interface IRunnableCommand
{
    /// <summary>
    /// Runs the command.
    /// </summary>
    /// <param name="client">The Discord client (the bot context).</param>
    /// <param name="message">The message sent by the user to trigger the command.</param>
    /// <param name="command">The user command to be executed.</param>
    public Task Run(DiscordShardedClient client, SocketMessage message, UserCommand command);
}
