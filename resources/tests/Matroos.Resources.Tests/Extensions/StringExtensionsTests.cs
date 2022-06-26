using System.Collections.Generic;

using Xunit;

namespace Matroos.Resources.Extensions;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("", 0)]
    [InlineData("http://test", 1)]
    [InlineData(";;http://test;;;;", 1)]
    [InlineData("http://test;http://test2;http://test3", 3)]
    public void GetWorkerURLsTests(string value, int count)
    {
        List<string> workerURLs = value.GetWorkerURLs();
        Assert.Equal(count, workerURLs.Count);
    }
}
