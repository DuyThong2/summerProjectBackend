using FluentValidation;
using Scheduling.API.Dto;

namespace Scheduling.API.Schedule.Commands.AddMonthlyScheduleItemsBulk
{
    public record AddMonthlyScheduleItemsBulkCommand(
    Guid MonthlyInstanceId,
    List<MonthlyScheduleItemDto> Items
) : ICommand<AddMonthlyScheduleItemsBulkResult>;

    public record AddMonthlyScheduleItemsBulkResult(int Inserted);


    

}
