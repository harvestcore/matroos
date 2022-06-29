using System.Drawing;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

using Discord;
using Discord.Addons.Hosting;
using Discord.WebSocket;

using Matroos.Resources.Classes.BackgroundProcessing;
using Matroos.Resources.Classes.Commands;
using Matroos.Resources.Classes.Mappers;
using Matroos.Resources.Interfaces;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Matroos.Resources.Classes.Bots;

public class Bot : IBaseItem
{
    /// <inheritdoc />
    public static string CollectionName { get; } = "bots";

    /// <summary>
    /// Bot identifier.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    /// <summary>
    /// Bot name.
    /// </summary>
    [BsonElement("name")]
    public string Name { get; set; }

    /// <summary>
    /// Bot description.
    /// </summary>
    [BsonElement("description")]
    public string Description { get; set; }

    /// <summary>
    /// Bot Discord key.
    /// </summary>
    [BsonElement("key")]
    public string Key { get; set; }

    /// <summary>
    /// Bot prefix.
    /// </summary>
    [BsonElement("prefix")]
    public string Prefix { get; set; }

    /// <summary>
    /// Bot's user commands. This list is populated when the Bot application is generated.
    /// </summary>
    [BsonElement("userCommands")]
    public List<UserCommand> UserCommands { get; set; }

    /// <summary>
    /// The <see cref="DiscordShardedClient"/> associated to this bot.
    /// </summary>
    [JsonIgnore]
    [BsonIgnore]
    [IgnoreDataMember]
    public DiscordShardedClient? Client { get; internal set; }

    /// <summary>
    /// Application cancellation token.
    /// </summary>
    [JsonIgnore]
    [BsonIgnore]
    [IgnoreDataMember]
    public CancellationTokenSource? CancellationToken { get; internal set; }

    /// <summary>
    /// Bot application.
    /// </summary>
    [JsonIgnore]
    [BsonIgnore]
    [IgnoreDataMember]
    public IHost? App { get; internal set; }

    /// <summary>
    /// Application cron service.
    /// </summary>
    [JsonIgnore]
    [BsonIgnore]
    [IgnoreDataMember]
    public CronService? Cron { get; internal set; }

    /// <summary>
    /// Whether the bot is running or not.
    /// </summary>
    [JsonIgnore]
    [BsonIgnore]
    [IgnoreDataMember]
    public bool Running { get; internal set; }

    /// <summary>
    /// The creation time of the bot.
    /// </summary>
    [BsonElement("createdAt")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// The update time of the bot.
    /// </summary>
    [BsonElement("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Bot()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Description = string.Empty;
        Key = string.Empty;
        Prefix = string.Empty;
        UserCommands = new();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

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