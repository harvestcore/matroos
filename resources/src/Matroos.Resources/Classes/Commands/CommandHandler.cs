using Discord.Addons.Hosting;
using Discord.WebSocket;

using Matroos.Resources.Classes.Bots;

using Microsoft.Extensions.Logging;

namespace Matroos.Resources.Classes.Commands;

public class CommandHandler : DiscordShardedClientService
{
    /// <summary>
    /// The Discord client.
    /// </summary>
    private readonly DiscordShardedClient _client;

    /// <summary>
    /// The action used to execute the commands asynchronously.
    /// </summary>
    private readonly Action ExecuteAsyncAction;

    /// <summary>
    /// The current bot.
    /// </summary>
    private readonly Bot _bot;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="client">The Discord client.</param>
    /// <param name="logger">The logger.</param>
    public CommandHandler(DiscordShardedClient client, ILogger<CommandHandler> logger, Bot bot) : base(client, logger)
    {
        _client = client;
        _bot = bot;
        bot.Client = client;


        ExecuteAsyncAction = () =>
        {
            Client.MessageReceived += OnMessageReceived;
        };
    }

    /// <summary>
    /// Executes all the callbacks attached to the event.
    /// </summary>
    /// <param name="stoppingToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A completed <see cref="Task"/>.</returns>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        ExecuteAsyncAction?.Invoke();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Executes the commands asynchronously.
    /// </summary>
    /// <param name="socketMessage">The message that has been sent.</param>
    /// <returns>A completed <see cref="Task"/>.</returns>
    private Task OnMessageReceived(SocketMessage socketMessage)
    {
        // Handle user messages only.
        if (socketMessage.Source != Discord.MessageSource.User)
        {
            return Task.CompletedTask;
        }

        string commandWithPrefix = socketMessage.Content.Split(" ").FirstOrDefault() ?? "";
        string trigger = commandWithPrefix.Replace(_bot.Prefix, "");

        UserCommand? foundCommand = _bot.UserCommands.Find(command => command.Trigger.Equals(trigger));
        if (foundCommand == null)
        {
            return Task.CompletedTask;
        }

        // Run the command (asynchronously).
        CommandHelper.RunCommand(_client, socketMessage, _bot, foundCommand);

        return Task.CompletedTask;
    }
}
