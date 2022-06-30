using System;
using System.Collections.Generic;
using System.Reflection;

using Matroos.Resources.Attributes;
using Matroos.Resources.Classes.Commands;

using Xunit;

namespace Matroos.Resources.Extensions;

public class TestCommand : BaseCommand
{
    public TestCommand() : base(CommandType.MESSAGE, true) { }
}

public enum TestEnum
{
    [Command(typeof(TestCommand))]
    A,
    [AType(typeof(string))]
    B,
    C
}

public class EnumExtensionsTests
{
    [Theory]
    [InlineData(TestEnum.A)]
    [InlineData(TestEnum.C, true)]
    public void GetAttributeTest_CommandAttribute(Enum value, bool shouldThrow = false)
    {
        if (shouldThrow)
        {
            Assert.Throws<CustomAttributeFormatException>(() => value.GetAttribute<CommandAttribute>());
            return;
        }

        CommandAttribute attribute = value.GetAttribute<CommandAttribute>();
        Assert.Equal(typeof(TestCommand), attribute.Command);
    }

    [Theory]
    [InlineData(TestEnum.B)]
    [InlineData(TestEnum.C, true)]
    public void GetAttributeTest_ATypeAttribute(Enum value, bool shouldThrow = false)
    {
        if (shouldThrow)
        {
            Assert.Throws<CustomAttributeFormatException>(() => value.GetAttribute<ATypeAttribute>());
            return;
        }

        ATypeAttribute attribute = value.GetAttribute<ATypeAttribute>();
        Assert.Equal(typeof(string), attribute.Type);
    }

    [Theory]
    [InlineData(CommandType.MESSAGE, new CommandMode[] { CommandMode.SCOPED })]
    [InlineData(CommandType.VERSION, new CommandMode[] { CommandMode.SINGLE })]
    [InlineData(CommandType.STATUS, new CommandMode[] { CommandMode.SINGLE })]
    [InlineData(CommandType.PING, new CommandMode[] { CommandMode.INLINE, CommandMode.SCOPED })]
    public void GetAllowedCommandModesTest(CommandType type, CommandMode[] expectedValues)
    {
        List<CommandMode> values = new(expectedValues);
        Assert.Equal(expectedValues.Length, values.Count);

        foreach (CommandMode mode in type.GetAllowedCommandModes())
        {
            Assert.Contains(mode, values);
        }
    }

    [Fact]
    public void ValidateParametersTest_NoParameters()
    {
        Dictionary<string, object> parameters = new();
        Exception exception = Record.Exception(() => CommandType.VERSION.ValidateParameters(parameters));
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateParametersTest_SomeParameters()
    {
        // The parameters are fine.
        Dictionary<string, object> parameters = new()
        {
            { "ChannelId", "123" }
        };
        Exception exception = Record.Exception(() => CommandType.STATUS.ValidateParameters(parameters));
        Assert.Null(exception);

        // One extra parameter.
        parameters = new()
        {
            { "ChannelId", "123" },
            { "ThisParameter", "ShouldNotBeHere" }
        };
        exception = Record.Exception(() => CommandType.STATUS.ValidateParameters(parameters));
        Assert.Null(exception);

        // No parameters at all.
        parameters = new();
        exception = Record.Exception(() => CommandType.STATUS.ValidateParameters(parameters));
        Assert.Null(exception);
    }
}
