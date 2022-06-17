using System;
using System.Reflection;

using Matroos.Resources.Attributes;
using Matroos.Resources.Classes.Commands;

using Xunit;

namespace Matroos.Resources.Extensions;

public class TestCommand : BaseCommand
{
    public TestCommand() : base(CommandType.MESSAGE, false) { }
}

public enum TestEnum
{
    [Command(typeof(TestCommand))]
    A,
    B
}

public class EnumExtensionsTests
{
    [Theory]
    [InlineData(TestEnum.A)]
    [InlineData(TestEnum.B, true)]
    public void CheckEnumCommandAttribute(Enum value, bool shouldThrow = false)
    {
        if (shouldThrow)
        {
            Assert.Throws<CustomAttributeFormatException>(() => value.GetCommandAttribute());
            return;
        }

        CommandAttribute attribute = value.GetCommandAttribute();
        Assert.Equal(typeof(TestCommand), attribute.Command);
    }
}
