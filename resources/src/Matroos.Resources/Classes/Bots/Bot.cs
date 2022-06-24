using System.Text.Json.Serialization;

using Discord;
using Discord.Addons.Hosting;
using Discord.WebSocket;

using Matroos.Resources.Classes.BackgroundProcessing;
using Matroos.Resources.Classes.Commands;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Matroos.Resources.Classes.Bots;

public class Bot
{
    /// <summary>
    /// Bot identifier.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Bot name.
    /// </summary>
    public string Name { get; internal set; }

    /// <summary>
    /// Bot description.
    /// </summary>
    public string Description { get; internal set; }

    /// <summary>
    /// Bot Discord key.
    /// </summary>
    public string Key { get; internal set; }

    /// <summary>
    /// Bot prefix.
    /// </summary>
    public string Prefix { get; internal set; }

    /// <summary>
    /// Bot's user commands. This list is populated when the Bot application is generated.
    /// </summary>
    public List<UserCommand> UserCommands { get; internal set; }

    /// <summary>
    /// The <see cref="DiscordShardedClient"/> associated to this bot.
    /// </summary>
    [JsonIgnore]
    public DiscordShardedClient? Client { get; internal set; }

    /// <summary>
    /// Application cancellation token.
    /// </summary>
    [JsonIgnore]
    public CancellationTokenSource? CancellationToken { get; internal set; }

    /// <summary>
    /// Bot application.
    /// </summary>
    [JsonIgnore]
    public IHost? App { get; internal set; }

    /// <summary>
    /// Application cron service.
    /// </summary>
    [JsonIgnore]
    public CronService? Cron { get; internal set; }

    /// <summary>
    /// Whether the bot is running or not.
    /// </summary>
    [JsonIgnore]
    public bool Running { get; internal set; }

    /// <summary>
    /// The creation time of the bot.
    /// </summary>
    public DateTime? CreatedAt { get; }

    /// <summary>
    /// The update time of the bot.
    /// </summary>
    public DateTime? UpdatedAt { get; internal set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="description">The description.</param>
    /// <param name="prefix">The prefix.</param>
    /// <param name="key">The Discord key.</param>
    /// <param name="userCommands">The user commands.</param>
    public Bot(string name, string description, string prefix, string key, List<UserCommand> userCommands)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentException("The Bot name must not be empty.");
        Description = description;
        Key = key ?? throw new ArgumentException("The Bot key must not be empty.");
        Prefix = prefix;
        UserCommands = userCommands;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;

        // Runnable props
        CancellationToken = null;
        Client = null;
        Cron = null;
        App = null;
    }

    /// <summary>
    /// Start the bot.
    /// </summary>
    public void Start()
    {
        if (!Running)
        {
            (App, Cron) = GenerateRunnable();
            CancellationToken = new CancellationTokenSource();
            App?.RunAsync(CancellationToken.Token).ConfigureAwait(false);
            Cron?.TriggerAction(BackgroundProcessing.Action.START);
            Running = true;
        }
    }

    /// <summary>
    /// Stop the bot.
    /// </summary>
    public void Stop()
    {
        if (
            Running &&
            CancellationToken != null &&
            App != null &&
            Cron != null)
        {
            Cron.TriggerAction(BackgroundProcessing.Action.STOP);
            CancellationToken.Cancel();
            App.WaitForShutdownAsync(CancellationToken.Token).ConfigureAwait(false);
            Running = false;
        }
    }

    /// <summary>
    /// Update the properties.
    /// </summary>
    /// <param name="bot">An object containing the new properties.</param>
    public void Update(Bot bot)
    {
        if (!string.IsNullOrEmpty(bot.Name))
        {
            Name = bot.Name;
        }

        if (!string.IsNullOrEmpty(bot.Description))
        {
            Description = bot.Description;
        }

        if (!string.IsNullOrEmpty(bot.Key))
        {
            Key = bot.Key;
        }

        Prefix = bot.Prefix;

        UserCommands = new List<UserCommand>(bot.UserCommands);
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Generates the bot application and its <see cref="CronService"/>.
    /// </summary>
    /// <returns>The <see cref="IHost"/> bot application and its <see cref="CronService"/>.</returns>
    public (IHost, CronService) GenerateRunnable()
    {
        CronService cron = new(this);

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

                config.Token = Key;
            })
            .ConfigureServices((_, services) =>
            {
                services
                    // Add the Bot instance as a singleton service.
                    .AddSingleton(this)

                    // Add the command handler as a hosted service.
                    .AddHostedService<CommandHandler>()

                    // Add the CronService as a singleton service.
                    .AddSingleton(cron);
            })
            .Build();

        return (app, cron);
    }
}