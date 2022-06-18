using System;
using System.Collections.Generic;

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
            new UserCommand("name", "description", "trigger", new(), CommandType.MESSAGE, CommandMode.INLINE);
        });
    }

    [Fact]
    public void UpdateSimpleAttributes()
    {
        UserCommand uc = new("name", "description", "trigger", new(), CommandType.MESSAGE, CommandMode.SCOPED);
        UserCommand modified = new("a", "b", "c", new(), CommandType.MESSAGE, CommandMode.SCOPED);
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
            { "a", 1 },
            { "b", "b" },
            { "c", true },
            { "d", null },
        };

        UserCommand uc = new("a", "b", "c", data, CommandType.MESSAGE, CommandMode.SCOPED);

        // There should only 3 parameters, "d" is null and should not be added.
        Assert.Equal(3, uc.Parameters.Count);

        Dictionary<string, object> updated = new()
        {
            { "a", 2 },
            { "b", "test" },
            { "e", 0.5 },
            { "f", false }
        };


        UserCommand modified = new("a", "b", "c", updated, CommandType.MESSAGE, CommandMode.SCOPED);

        uc.Update(modified);

        // There should only 5 parameters.
        Assert.Equal(5, uc.Parameters.Count);

        Assert.Equal(2, uc.Parameters["a"]);
        Assert.Equal("test", uc.Parameters["b"]);
        Assert.Equal(true, uc.Parameters["c"]);
        Assert.Equal(0.5, uc.Parameters["e"]);
        Assert.Equal(false, uc.Parameters["f"]);
    }
}
