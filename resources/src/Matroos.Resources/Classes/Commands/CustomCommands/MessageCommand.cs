using Discord.WebSocket;

using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Extensions;
using Matroos.Resources.Utilities;

namespace Matroos.Resources.Classes.Commands.CustomCommands;

public class MessageCommand : BaseCommand, IRunnableCommand
{
    public MessageCommand() : base(CommandType.MESSAGE, true)
    {
        AllowedModes = new() { CommandMode.SCOPED };
        Parameters = new()
        {
            new(
                name: "Message",
                displayName: "Message",
                required: true,
                dataType: DataType.STRING,
                @default: "",
                validator: _ => true
            ),
            new(
                name: "ChannelId",
                displayName: "Channel",
                required: false,
                dataType: DataType.STRING,
                @default: "",
                validator: _ => true
            ),
            new(
                name: "IsResponse",
                displayName: "Response?",
                required: false,
                dataType: DataType.BOOLEAN,
                @default: false,
                validator: _ => true
            ),
            new(
                name: "IsTTS",
                displayName: "TTS?",
                required: false,
                dataType: DataType.BOOLEAN,
                @default: false,
                validator: _ => true
            )
        };
    }

    /// <inheritdoc />
    public async Task Run(DiscordShardedClient client, SocketMessage message, Bot bot, UserCommand command)
    {
        if (client == null || !ValidPrefix(message, bot))
        {
            return;
        }

        // Channel where to send the message.
        ISocketMessageChannel channel;
        string channelId = command.Parameters.GetValueOrDefault("ChannelId")?.GetValue<string>() ?? "";
        bool isTTS = command.Parameters.GetValueOrDefault("IsTTS")?.GetValue<bool>() ?? false;

        // There is no channel where to send messages.
        if (string.IsNullOrEmpty(channelId) && message == null)
        {
            return;
        }

        if (!string.IsNullOrEmpty(channelId) && client.GetChannel(Convert.ToUInt64(channelId)) is ISocketMessageChannel channelFound)
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

        string msg = command.Parameters.GetValueOrDefault("Message")?.GetValue<string>() ?? "";
        bool isResponse = command.Parameters.GetValueOrDefault("IsResponse")?.GetValue<bool>() ?? false;

        if (isResponse && message != null)
        {
            msg = $"<@{message.Author.Id}> {msg}";
        }

        await channel.SendMessageAsync(msg, isTTS).ConfigureAwait(false);
    }
}
