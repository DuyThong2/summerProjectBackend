namespace Scheduling.API.Dto
{
    public record ScheduleTemplateDetailDto(
        Guid? Id,
        DayOfWeek DayOfWeek,
        TimeSlot TimeSlot,
        Guid MealId
    );

    

}
