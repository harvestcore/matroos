using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Classes.Commands;
using Matroos.Resources.Classes.Commands.CustomCommands;
using Matroos.Resources.Extensions;

using Quartz;
using Quartz.Impl;

namespace Matroos.Resources.Classes.BackgroundProcessing;

public class CronService
{
    /// <summary>
    /// Task scheduler.
    /// </summary>
    private IScheduler? _scheduler;

    /// <summary>
    /// The job that runs the commands.
    /// </summary>
    private IJobDetail? _runCommandJob;

    /// <summary>
    /// Service group name.
    /// </summary>
    private static readonly string CronServiceGroupName = "CronJobGroup";

    /// <summary>
    /// Job identity name.
    /// </summary>
    private static readonly string JobIdentityName = "RunCommand";

    /// <summary>
    /// The <see cref="Bot"/> where to extract the <see cref="TimerCommand"/>s.
    /// </summary>
    private readonly Bot _bot;

    /// <summary>
    /// The triggers. This dictionary contains all the timers commands that can be executed in this service.
    /// </summary>
    private Dictionary<Guid, ITrigger> Triggers { get; set; } = new();

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="bot">The <see cref="Bot"/> where to extract the <see cref="TimerCommand"/>s.</param>
    public CronService(Bot bot)
    {
        _bot = bot;
    }

    /// <summary>
    /// Triggers an action in this service.
    /// </summary>
    /// <param name="action">The action to perform.</param>
    public void TriggerAction(Action action)
    {
        switch (action)
        {
            case Action.START:
                Startup();
                break;
            case Action.STOP:
                Shutdown();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Initialize and start the service.
    /// </summary>
    private void Startup()
    {
        // Create the job.
        _runCommandJob = JobBuilder.Create<RunCommandJob>()
            .WithIdentity(JobIdentityName, CronServiceGroupName)
            .StoreDurably()
            .Build();

        // Create the scheduler.
        Task<IScheduler> getSchedulerTask = new StdSchedulerFactory().GetScheduler();

        // Wait for the scheduler to initialize.
        getSchedulerTask.Wait();
        _scheduler = getSchedulerTask.Result;

        // Add the job to the scheduler.
        Task addJobTask = _scheduler.AddJob(_runCommandJob, true);

        // Wait for the scheduler to add the task.
        addJobTask.Wait();

        // Run the scheduler.
        Task startSchedulerTask = _scheduler.Start();

        // Wait for the scheduler to start.
        startSchedulerTask.Wait();

        // Activate tasks (commands).
        foreach (UserCommand userCommand in _bot.UserCommands.FindAll(command => command.Type == CommandType.TIMER))
        {
            if (userCommand.Parameters["Active"].GetValue<bool>())
            {
                ActivateTask(userCommand);
            }
        }
    }

    /// <summary>
    /// Shut the service down.
    /// </summary>
    private void Shutdown()
    {
        foreach ((Guid _, ITrigger trigger) in Triggers)
        {
            _scheduler?.UnscheduleJob(trigger.Key);
        }
    }

    /// <summary>
    /// Activate a task (a command).
    /// </summary>
    /// <param name="userCommand">The <see cref="UserCommand"/> to activate.</param>
    public void ActivateTask(UserCommand userCommand)
    {
        if (
            _runCommandJob == null ||
            _scheduler == null ||
            userCommand.Type != CommandType.TIMER
        )
        {
            return;
        }

        Guid userCommandId = userCommand.Id;
        string? cron = userCommand.Parameters["Interval"].GetValue<string>();

        if (cron == null)
        {
            return;
        }

        // Send the UserCommand and the Bot as parameters.
        JobDataMap map = new();
        map.Put("Command", userCommand);
        map.Put("Bot", _bot);

        // Create trigger.
        ITrigger trigger = TriggerBuilder.Create()

            // This trigger is for the _runCommandJob (that was created previously).
            .ForJob(_runCommandJob)

            // Give an identity to this (the UserCommand identifier).
            .WithIdentity(userCommandId.ToString(), CronServiceGroupName)

            // Schedule the trigger with the cron expression from the UserCommand.
            .WithCronSchedule(cron)

            // Parameters.
            .UsingJobData(map)
            .Build();

        if (!Triggers.ContainsKey(userCommandId))
        {
            // Enqueue the trigger. When the event triggers the "RunCommand" task will run.
            _scheduler.ScheduleJob(trigger);

            // Save the trigger.
            Triggers.Add(userCommandId, trigger);

            // Set the command as active.
            userCommand.Parameters["Active"] = true;
        }
    }

    /// <summary>
    /// Deactivate a task (a command).
    /// </summary>
    /// <param name="userCommand">The <see cref="UserCommand"/> to be deactivated.</param>
    public void DeactivateTask(UserCommand userCommand)
    {
        if (_scheduler == null)
        {
            return;
        }

        Guid userCommandId = userCommand.Id;
        if (Triggers.ContainsKey(userCommandId))
        {
            // Unschedule the trigger.
            _scheduler.UnscheduleJob(Triggers[userCommandId].Key);

            // Remove the trigger.
            Triggers.Remove(userCommandId);

            // Set the command as inactive.
            userCommand.Parameters["Active"] = false;
        }
    }
}
