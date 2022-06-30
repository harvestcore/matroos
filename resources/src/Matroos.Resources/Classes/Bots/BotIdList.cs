namespace Matroos.Resources.Classes.Bots;

/// <summary>
/// Simple class to match a list of bot identifiers.
/// </summary>
public class BotIdList
{
    /// <summary>
    /// The list of identifiers.
    /// </summary>
    public List<Guid> Bots { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public BotIdList()
    {
        Bots = new();
    }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="bots">The list of bot identifiers.</param>
    public BotIdList(List<Guid> bots)
    {
        Bots = bots;
    }
}
