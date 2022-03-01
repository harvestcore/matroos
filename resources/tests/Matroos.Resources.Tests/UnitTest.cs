using Xunit;

namespace Matroos.Resources.Tests
{
    public class UnitTest
    {
        [Fact]
        public void Test()
        {
            string name = "test";
            Bot bot = new(name);
            Assert.Equal(name, bot.Name);
        }
    }
}