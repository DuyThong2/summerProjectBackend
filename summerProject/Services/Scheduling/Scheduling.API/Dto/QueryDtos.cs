using Scheduling.API.Enums.Materialized;

namespace Scheduling.API.Dto
{
   

    public record CalendarMealDto(Guid MealId, string MealName, ScheduleItemSource Source, Guid? SourceId);
    public record CalendarSlotDto(TimeSlot TimeSlot, List<CalendarMealDto> Meals);
    public record CalendarDayDto(DateTime Date, List<CalendarSlotDto> Slots);
    



}
