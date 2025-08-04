namespace Scheduling.API.Schedule.Commands.DeleteMonthlyScheduleInstance
{
    public record DeleteMonthlyScheduleInstanceCommand(Guid Id) : ICommand<bool>;

}
