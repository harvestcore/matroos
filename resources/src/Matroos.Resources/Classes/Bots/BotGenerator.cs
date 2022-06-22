using Discord;
using Discord.Addons.Hosting;
using Discord.WebSocket;

using Matroos.Resources.Classes.BackgroundProcessing;
using Matroos.Resources.Classes.Commands;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Matroos.Resources.Classes.Bots;

public static class BotGenerator
{
    /// <summary>
    /// Generates the bot application and its <see cref="CronService"/>.
    /// </summary>
    /// <param name="bot">The <see cref="Bot"/> where to extract the necessary info.</param>
    /// <returns>The <see cref="IHost"/> bot application and its <see cref="CronService"/>.</returns>
    public static (IHost, CronService) Generate(Bot bot)
    {
        CronService cron = new(bot);

        IHost app = Host
            .CreateDefaultBuilder()
            .ConfigureLogging(logging =>
            {
                // Logging to be configured.
            })
            .ConfigureDiscordShardedHost((HostBuilderContext context, DiscordHostConfiguration config) =>
            {
                config.SocketConfig = new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Verbose,
                    AlwaysDownloadUsers = false,
                    MessageCacheSize = 200,
                    TotalShards = 4
                };

                config.Token = bot.Key;
            })
            .ConfigureServices((_, services) =>
            {
                services
                    // Add the Bot instance as a singleton service.
                    .AddSingleton(bot)

                    // Add the command handler as a hosted service.
                    .AddHostedService<CommandHandler>()

                    // Add the CronService as a singleton service.
                    .AddSingleton(cron);
            })
            .Build();

        return (app, cron);
    }
}