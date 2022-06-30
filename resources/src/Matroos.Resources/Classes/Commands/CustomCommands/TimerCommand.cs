using Matroos.Resources.Utilities;

using Quartz;

namespace Matroos.Resources.Classes.Commands.CustomCommands;

public class TimerCommand : BaseCommand
{
    public TimerCommand() : base(CommandType.TIMER, false)
    {
        AllowedModes = new() { CommandMode.HEADLESS };
        Parameters = new()
        {
            new(
                name: "Interval",
                displayName: "TimeSpan",
                required: true,
                dataType: DataType.DATE,
                @default: "",
                validator: ValidateInterval
            ),
            new(
                name: "CommandId",
                displayName: "Command?",
                required: false,
                dataType: DataType.STRING,
                @default: "",
                validator: _ => true
            ),
            new(
                name: "Active",
                displayName: "Active",
                required: true,
                dataType: DataType.BOOLEAN,
                @default: false,
                validator: _ => true
            )
        };
    }

    /// <summary>
    /// Validates the given cron expression.
    /// </summary>
    /// <param name="expression">The expression to be validated.</param>
    /// <returns></returns>
    private bool ValidateInterval(object expression)
    {
        return CronExpression.IsValidExpression((string)expression);
    }
}
