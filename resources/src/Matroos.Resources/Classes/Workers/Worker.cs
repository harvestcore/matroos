using Matroos.Resources.Classes.Bots;

namespace Matroos.Resources.Classes.Workers;

public class Worker
{
    /// <summary>
    /// The worker identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The remote URL where the worker is located.
    /// </summary>
    public string RemoteUrl { get; set; }

    /// <summary>
    /// The bots that are deployed in the worker.
    /// </summary>
    public List<Bot> Bots { get; set; }

    /// <summary>
    /// Worker's last update.
    /// </summary>
    public DateTime LastUpdate { get; internal set; }

    /// <summary>
    /// Whether the worker is up (reachable) or not.
    /// </summary>
    public bool IsUp => LastUpdate > DateTime.UtcNow;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Worker()
    {
        Id = Guid.NewGuid();
        Bots = new();
        LastUpdate = DateTime.UtcNow;
        RemoteUrl = string.Empty;
    }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="id">The worker identifier.</param>
    /// <param name="remoteURL">The remote URL where the worker is located.</param>
    /// <param name="bots">The bots that are deployed in the worker.</param>
    public Worker(Guid id, string remoteURL, List<Bot> bots)
    {
        Id = id;
        RemoteUrl = remoteURL ?? throw new ArgumentException("The Worker remote URL must not be empty.");
        Bots = new(bots);
    }

    /// <summary>
    /// Renew the worker information.
    /// </summary>
    /// <param name="bots">The new list of bots.</param>
    /// <param name="remoteURL">The remote URL.</param>
    public void Renew(List<Bot> bots, string remoteURL)
    {
        LastUpdate = DateTime.UtcNow + TimeSpan.FromSeconds(15);
        Bots = new(bots);
        RemoteUrl = remoteURL;
    }
}
