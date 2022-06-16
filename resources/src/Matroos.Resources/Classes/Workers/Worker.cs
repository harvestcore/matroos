namespace Matroos.Resources.Classes.Workers;

public class Worker
{
    /// <summary>
    /// The worker identifier.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// The remote URL where the worker is located.
    /// </summary>
    public string RemoteUrl { get; private set; }

    /// <summary>
    /// The bots that are deployed in the worker.
    /// </summary>
    public List<Bot> Bots { get; private set; }

    /// <summary>
    /// Whether the worker is up (reachable) or not.
    /// </summary>
    public bool IsUp { get; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="remoteURL">The remote URL where the worker is located.</param>
    /// <param name="bots">The bots that are deployed in the worker.</param>
    public Worker(string remoteURL, List<Bot> bots)
    {
        Id = Guid.NewGuid();
        RemoteUrl = remoteURL ?? throw new ArgumentException("The Worker remote URL must not be empty.");
        Bots = new(bots);
    }
}
