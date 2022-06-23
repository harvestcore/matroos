using Discord.WebSocket;

using Matroos.Resources.Classes.BackgroundProcessing;
using Matroos.Resources.Classes.Commands;

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
    public string Prefix { get; }

    /// <summary>
    /// Bot's user commands. This list is populated when the Bot application is generated.
    /// </summary>
    public List<UserCommand> UserCommands { get; internal set; }

    /// <summary>
    /// The <see cref="DiscordShardedClient"/> associated to this bot.
    /// </summary>
    public DiscordShardedClient? Client { get; internal set; }

    /// <summary>
    /// Application cancellation token.
    /// </summary>
    public CancellationTokenSource? CancellationToken { get; internal set; }

    /// <summary>
    /// Bot application.
    /// </summary>
    public IHost? App { get; internal set; }

    /// <summary>
    /// Application cron service.
    /// </summary>
    public CronService? Cron { get; internal set; }

    /// <summary>
    /// Whether the bot is running or not.
    /// </summary>
    public bool Running { get; set; }

    /// <summary>
    /// The creation time of the bot.
    /// </summary>
    public DateTime? CreatedAt { get; }

    /// <summary>
    /// The update time of the bot.
    /// </summary>
    public DateTime? UpdatedAt { get; }

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
        UserCommands = new();
        Client = null;
    }

    /// <summary>
    /// Start the bot.
    /// </summary>
    public void Start()
    {
        if (!Running)
        {
            (App, Cron) = BotGenerator.Generate(this);
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

        UserCommands = new List<UserCommand>(bot.UserCommands);
        UpdatedAt = DateTime.UtcNow;
    }
}