
using Matroos.Resources.Classes.BackgroundProcessing;
using Matroos.Resources.Classes.Bots;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Xunit;

namespace Matroos.Resources.Tests.Classes.Bots;

public class BotGeneratorTests
{
    private readonly Bot _bot;

    public BotGeneratorTests()
    {
        _bot = new("test", "", "!", "key", new());
    }

    [Fact]
    public void GenerationTests()
    {
        (IHost app, CronService cron) = BotGenerator.Generate(_bot);

        Assert.NotNull(app);
        Assert.NotNull(cron);

        // Check the application services.
        Assert.NotNull(app.Services.GetService<Bot>());
        Assert.NotNull(app.Services.GetService<CronService>());
    }
}