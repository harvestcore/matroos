using System.Net.NetworkInformation;

using Discord.WebSocket;

using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Extensions;
using Matroos.Resources.Utilities;

namespace Matroos.Resources.Classes.Commands.CustomCommands;

public class PingCommand : BaseCommand, IRunnableCommand
{
    public PingCommand() : base(CommandType.PING, true)
    {
        AllowedModes = new() { CommandMode.INLINE, CommandMode.SCOPED };
        Parameters = new()
        {
            new(
                name: "Host",
                displayName: "Host",
                required: true,
                type: DataType.STRING,
                @default: ""
            ),
            new(
                name: "ChannelId",
                displayName: "Channel",
                required: false,
                type: DataType.STRING,
                @default: ""
            )
        };
    }

    /// <inheritdoc />
    public async Task Run(DiscordShardedClient client, SocketMessage message, Bot bot, UserCommand command)
    {
        if (!ValidPrefix(message, bot))
        {
            return;
        }

        // Channel where to send the message.
        ISocketMessageChannel channel;
        string channelId = command.Parameters["ChannelId"]?.GetValue<string>() ?? "";

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
            channel = message.Channel;
        }

        string host = "";
        if (command.Mode == CommandMode.INLINE)
        {
            string[] args = message.Content.Split(" ");
            if (args.Length != 2)
            {
                await channel.SendMessageAsync("Missing host.").ConfigureAwait(false); ;
            }

            host = args[1];
        }
        else if (command.Mode == CommandMode.SCOPED)
        {
            host = command.Parameters["Host"]?.GetValue<string>() ?? "";
        }

        if (string.IsNullOrEmpty(host))
        {
            await channel.SendMessageAsync("Missing host.").ConfigureAwait(false); ;
        }

        string msg = "";
        Ping pingSender = new();
        pingSender.PingCompleted += new PingCompletedEventHandler(async (sender, e) =>
        {
            if (e.Cancelled)
            {
                msg = $"Ping to {host} was canceled.";
            }
            else if (e.Error != null)
            {
                msg = $"Ping to {host} failed. Probably the host does not exist.";
            }
            else
            {
                PingReply? reply = e.Reply;

                if (reply != null)
                {
                    msg += $"Host: {host}\n";
                    msg += $"Status: {reply.Status}\n";
                    msg += $"Address: {reply.Address}\n";
                    msg += $"Round Trip Time: {reply.RoundtripTime} ms\n";
                    msg += $"Time to live: {reply.Options?.Ttl}\n";
                }
            }

            await channel.SendMessageAsync(msg ?? $"Ping to {host} failed.").ConfigureAwait(false);
        });

        pingSender.SendAsync(host, null);
    }
}
