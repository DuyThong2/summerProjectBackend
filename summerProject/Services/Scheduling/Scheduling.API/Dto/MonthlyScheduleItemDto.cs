using Scheduling.API.Enums.Materialized;

namespace Scheduling.API.Dto
{
    public record MonthlyScheduleItemDto(
    Guid? Id,
    DateTime Date,
    TimeSlot TimeSlot,
    Guid MealId,
    string MealName,         

    ScheduleItemSource Source,
    Guid? SourceId
);

    

}
