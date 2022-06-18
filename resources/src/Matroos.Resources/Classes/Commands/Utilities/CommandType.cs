using Matroos.Resources.Attributes;
using Matroos.Resources.Classes.Commands.CustomCommands;

namespace Matroos.Resources.Classes.Commands;

public enum CommandType
{
    [Command(typeof(MessageCommand))]
    MESSAGE,    // 0
    [Command(typeof(PingCommand))]
    PING,       // 1
    STATUS,     // 2
    TIMER,      // 3
    VERSION     // 4
}

