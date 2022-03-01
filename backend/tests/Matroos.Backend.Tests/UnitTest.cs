using Matroos.Resources;

using Xunit;

namespace Matroos.Backend.Tests
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