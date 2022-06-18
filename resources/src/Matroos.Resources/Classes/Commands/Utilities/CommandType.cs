using Matroos.Reources.Classes.Commands.CustomCommands;
using Matroos.Resources.Attributes;

namespace Matroos.Resources.Classes.Commands;

public enum CommandType
{
    [Command(typeof(MessageCommand))]
    MESSAGE,    // 0
    PING,       // 1
    STATUS,     // 2
    TIMER,      // 3
    VERSION     // 4
}

