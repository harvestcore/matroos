using System;
using System.Collections.Generic;
using System.Reflection;

using Matroos.Resources.Classes.Commands;

using Xunit;

namespace Matroos.Resources.Tests.Classes.Commands;

public class UserCommandTests
{
    [Fact]
    public void CreateUserCommandWithWrongCommandMode()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            // INLINE is not a valid mode.
            new UserCommand("name", "description", "trigger", new(), CommandType.VERSION, CommandMode.INLINE);        });
    }

    [Fact]
    public void UpdateSimpleAttributes()
    {
        UserCommand uc = new("name", "description", "trigger", new(), CommandType.VERSION, CommandMode.SINGLE);
        UserCommand modified = new("a", "b", "c", new(), CommandType.VERSION, CommandMode.SINGLE);
        uc.Update(modified);

        Assert.Equal("a", uc.Name);
        Assert.Equal("b", uc.Description);
        Assert.Equal("c", uc.Trigger);
    }

    [Fact]
    public void UpdateParametersAttribute()
    {
        Dictionary<string, object> data = new()
        {
            { "Message", "msg" },
            { "ChannelId", "channel" },
            { "IsResponse", true },
            { "IsTTS", true },
        };

        UserCommand uc = new("a", "b", "c", data, CommandType.MESSAGE, CommandMode.SCOPED);
        Assert.Equal(4, uc.Parameters.Count);

        Dictionary<string, object> updated = new()
        {
            { "Message", "test" },
            { "ChannelId", "test" },
            { "IsResponse", false },
            { "IsTTS", false }
        };

        UserCommand modified = new("a", "b", "c", updated, CommandType.MESSAGE, CommandMode.SCOPED);
        uc.Update(modified);

        Assert.Equal(4, uc.Parameters.Count);
        Assert.Equal("test", uc.Parameters["Message"]);
        Assert.Equal("test", uc.Parameters["ChannelId"]);
        Assert.Equal(false, uc.Parameters["IsResponse"]);
        Assert.Equal(false, uc.Parameters["IsTTS"]);
    }

    [Fact]
    public void CheckWrongParameters()
    {
        // Wrong data types.
        Dictionary<string, object> data = new()
        {
            { "Message", 1 },
            { "ChannelId", true },
            { "IsResponse", 2 },
            { "IsTTS", 0.5 },
        };

        Assert.Throws<TargetInvocationException>(
            () => new UserCommand("a", "b", "c", data, CommandType.MESSAGE, CommandMode.SCOPED)
        );
    }
}
