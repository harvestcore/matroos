using Matroos.Backend.Services.Interfaces;

namespace Matroos.Backend.Services;

public class BackgroundService : Microsoft.Extensions.Hosting.BackgroundService
{
    /// <summary>
    /// The <see cref="PeriodicTimer"/>.
    /// </summary>
    private PeriodicTimer? _timer;

    /// <summary>
    /// The <see cref="IWorkersService"/>.
    /// </summary>
    private readonly IWorkersService _workersService;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="workersService"></param>
    public BackgroundService(IWorkersService workersService)
    {
        _workersService = workersService;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(10));
        while (await _timer.WaitForNextTickAsync(stoppingToken))
        {
            await _workersService.RenewWorkers();
        }
    }

    /// <inheritdoc />
    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _timer?.Dispose();
        await base.StopAsync(stoppingToken);
    }
}
