namespace Scheduling.API.Dto
{
    public record MonthlyScheduleInstanceDto(
        Guid? Id,
        Guid ScheduleCollectionId,
        int Year,
        int Month,
        Guid? AppliedTemplateId,
        DateTime GeneratedAt,
        List<MonthlyScheduleItemDto> Items
    );

    
        
    

}
