using System;
using System.Collections.Generic;
using System.Linq;

using Matroos.Resources.Classes.BackgroundProcessing;
using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Classes.Commands;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Xunit;

namespace Matroos.Resources.Tests.Classes.Bots;

public class BotTests
{
    [Fact]
    public void GenerationTests()
    {
        Bot bot = new("test", "", "!", "key", new());

        (IHost app, CronService cron) = bot.GenerateRunnable();

        Assert.NotNull(app);
        Assert.NotNull(cron);

        // Check the application services.
        Assert.NotNull(app.Services.GetService<Bot>());
        Assert.NotNull(app.Services.GetService<CronService>());
    }

    [Fact]
    public void StartStopTests()
    {
        Bot bot = new("test", "", "!", "key", new());
        Assert.NotNull(bot);

        bot.Start();

        Assert.NotNull(bot.App);
        Assert.NotNull(bot.Cron);
        Assert.NotNull(bot.CancellationToken);
        Assert.True(bot.Running);

        bot.Stop();
        Assert.True(bot.CancellationToken?.IsCancellationRequested ?? false);
        Assert.False(bot.Running);
    }

    [Theory]
    [InlineData("name", "desc", "prefix", "key", 0, "name", "desc", "prefix", "key")]
    [InlineData("", "desc", "prefix", "key", 1, "defaultName", "desc", "prefix", "key")]
    [InlineData("name", "", "prefix", "key", 2, "name", "defaultDescription", "prefix", "key")]
    [InlineData("name", "desc", "", "key", 3, "name", "desc", "", "key")]
    [InlineData("name", "desc", "prefix", "", 4, "name", "desc", "prefix", "defaultKey")]
    public void UpdateTests(string name, string description, string prefix, string key, int commands, string expectedName, string expectedDescription, string expectedPrefix, string expectedKey)
    {
        string defaultName = "defaultName";
        string defaultDescription = "defaultDescription";
        string defaultPrefix = "defaultPrefix";
        string defaultKey = "defaultKey";

        Bot bot = new(defaultName, defaultDescription, defaultPrefix, defaultKey, new());
        Assert.NotNull(bot);

        Dictionary<string, object> data = new()
        {
            { "Message", "msg" },
            { "ChannelId", "channel" },
            { "IsResponse", true },
            { "IsTTS", true },
        };

        List<UserCommand> userCommands = Enumerable.Repeat(
            new UserCommand("a", "b", "c", data, CommandType.MESSAGE, CommandMode.SCOPED),
            commands
        ).ToList();

        Bot updated = new(name, description, prefix, key, userCommands);
        bot.Update(updated);

        Assert.Equal(expectedName, bot.Name);
        Assert.Equal(expectedDescription, bot.Description);
        Assert.Equal(expectedPrefix, bot.Prefix);
        Assert.Equal(expectedKey, bot.Key);
        Assert.True(bot.UpdatedAt < DateTime.UtcNow);
        Assert.Equal(commands, bot.UserCommands.Count);
    }
}
