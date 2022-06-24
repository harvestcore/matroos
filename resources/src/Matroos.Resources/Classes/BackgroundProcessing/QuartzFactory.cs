using Quartz;
using Quartz.Impl;

namespace Matroos.Resources.Classes.BackgroundProcessing;

public class QuartzFactory
{
    /// <summary>
    /// Lock to prevent access clashes.
    /// </summary>
    private static readonly object _lock = new();

    /// <summary>
    /// The Quartz scheduler.
    /// </summary>
    private static IScheduler? _schedulerInstance;

    /// <summary>
    /// Get the Scheduler instance.
    /// </summary>
    public static IScheduler SchedulerInstance
    {
        get
        {
            lock (_lock)
            {
                if (_schedulerInstance == null)
                {
                    Task<IScheduler> task = StdSchedulerFactory.GetDefaultScheduler();
                    task.Wait();
                    _schedulerInstance = task.Result;
                }

                return _schedulerInstance;
            }
        }
    }
}
