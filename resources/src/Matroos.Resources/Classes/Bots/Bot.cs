using Matroos.Resources.Classes.Commands;

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
    public string Name { get; }

    /// <summary>
    /// Bot description.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Bot Discord key.
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Bot prefix.
    /// </summary>
    public string Prefix { get; }

    /// <summary>
    /// Bot commands.
    /// </summary>
    public List<Guid> Commands { get; }

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
    /// <param name="commands">The commands.</param>
    public Bot(string name, string description, string prefix, string key, List<Guid> commands)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentException("The Bot name must not be empty.");
        Description = description;
        Key = key ?? throw new ArgumentException("The Bot key must not be empty.");
        Prefix = prefix;
        Commands = commands;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public List<UserCommand> GetUserCommands()
    {
        throw new NotImplementedException();
    }
}