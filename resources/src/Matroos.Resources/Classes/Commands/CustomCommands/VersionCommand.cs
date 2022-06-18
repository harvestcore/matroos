using Discord.WebSocket;

using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Extensions;

namespace Matroos.Resources.Classes.Commands.CustomCommands;

public class VersionCommand : BaseCommand, IRunnableCommand
{
    public VersionCommand() : base(CommandType.STATUS, true)
    {
        AllowedModes = new() { CommandMode.SINGLE };
    }

    public async Task Run(DiscordShardedClient client, SocketMessage message, Bot botSettings, UserCommand command)
    {
        AssemblyInfo assemblyInfo = AssemblyExtensions.GetAssemblyInfo();
        await message.Channel.SendMessageAsync($"{assemblyInfo.Product} - v{assemblyInfo.Version}").ConfigureAwait(false);
    }
}
