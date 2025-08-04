namespace Scheduling.API.Dto
{
    public record ScheduleCollectionBriefDto(
    Guid Id,
    int StartYear,
    int StartMonth,
    Guid? BaseTemplateId,
    bool AutoRepeatMonthly
);
}
