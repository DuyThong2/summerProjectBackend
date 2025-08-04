namespace Scheduling.API.Schedule.Commands.NewFolder
{
    public record DeleteMonthlyScheduleItemCommand(Guid Id) : ICommand<bool>;

}
