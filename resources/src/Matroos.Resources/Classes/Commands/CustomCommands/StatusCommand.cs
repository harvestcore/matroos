using Discord.WebSocket;

using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Extensions;
using Matroos.Resources.Utilities;

namespace Matroos.Resources.Classes.Commands.CustomCommands;

public class StatusCommand : BaseCommand, IRunnableCommand
{
    public StatusCommand() : base(CommandType.STATUS, true)
    {
        AllowedModes = new() { CommandMode.SINGLE };
        Parameters = new()
        {
            new(
                name: "ChannelId",
                displayName: "Channel",
                required: false,
                type: DataType.STRING,
                @default: ""
            )
        };
    }

    public async Task Run(DiscordShardedClient client, SocketMessage message, Bot bot, UserCommand command)
    {
        if (client == null || !ValidPrefix(message, bot))
        {
            return;
        }

        // Channel where to send the message.
        ISocketMessageChannel channel;
        string channelId = command.Parameters["ChannelId"]?.GetValue<string>() ?? "";

        // There is no channel where to send messages.
        if (string.IsNullOrEmpty(channelId) && message == null)
        {
            return;
        }

        if (client.GetChannel(Convert.ToUInt64(channelId)) is ISocketMessageChannel channelFound)
        {
            channel = channelFound;
        }
        else
        {
            if (message == null)
            {
                return;
            }

            channel = message.Channel;
        }

        string msg = $"Bot: {bot.Name}\n" +
            $"Prefix: {bot.Prefix}\n" +
            $"# Commands: {bot.Commands.Count}\n" +
            $"Last update: {bot.UpdatedAt}\n";

        await channel.SendMessageAsync(msg).ConfigureAwait(false);
    }
}
