
using Matroos.Resources.Classes.Bots;
using Matroos.Resources.Classes.Commands;
using Matroos.Resources.Extensions;

using Quartz;

namespace Matroos.Resources.Classes.BackgroundProcessing;

public class RunCommandJob : IJob
{
    /// <summary>
    /// Executes the RunCommand job.
    /// </summary>
    /// <param name="context">The context of this job.</param>
    public Task Execute(IJobExecutionContext context)
    {
        if (context == null)
        {
            return Task.CompletedTask;
        }

        // Extract the parameters.
        UserCommand? timerCommand = context.MergedJobDataMap?.Get("Command").GetValue<UserCommand>();
        Bot? bot = context.MergedJobDataMap?.Get("Bot").GetValue<Bot>();

        if (timerCommand == null || bot == null || bot.Client == null)
        {
            return Task.CompletedTask;
        }

        // The command to be executed.
        string? commandToExecute = timerCommand.Parameters["CommandId"].GetValue<string>();
        UserCommand? commandFound = bot.UserCommands.Find(command => command.Id == Guid.Parse(commandToExecute ?? ""));

        if (commandFound == null || commandFound.Mode == CommandMode.INLINE || commandFound.Mode == CommandMode.HEADLESS)
        {
            return Task.CompletedTask;
        }

        // Run the command.
        CommandHelper.RunCommand(bot.Client, null, bot, commandFound);

        return Task.CompletedTask;
    }
}
