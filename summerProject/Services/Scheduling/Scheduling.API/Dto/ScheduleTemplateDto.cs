namespace Scheduling.API.Dto
{
    public record ScheduleTemplateDto(
        Guid? Id,
        string Name,
        string? Description,
        List<ScheduleTemplateDetailDto> Details
    );
}
