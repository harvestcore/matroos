using Matroos.Resources.Attributes;
using Matroos.Resources.Classes.Commands.CustomCommands;

namespace Matroos.Resources.Classes.Commands;

public enum CommandType
{
    [Command(typeof(MessageCommand))]
    MESSAGE,    // 0
    [Command(typeof(PingCommand))]
    PING,       // 1
    [Command(typeof(StatusCommand))]
    STATUS,     // 2
    [Command(typeof(TimerCommand))]
    TIMER,      // 3
    [Command(typeof(VersionCommand))]
    VERSION     // 4
}

