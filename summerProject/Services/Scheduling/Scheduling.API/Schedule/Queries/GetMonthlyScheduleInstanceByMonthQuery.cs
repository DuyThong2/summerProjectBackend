using Scheduling.API.Data;
using Scheduling.API.Dto;

public record GetMonthlyScheduleInstanceByMonthQuery(
    Guid ScheduleCollectionId,
    int Year,
    int Month
) : IQuery<MonthlyScheduleInstanceDto?>;

public class GetMonthlyScheduleInstanceByMonthHandler(
    SchedulingDbContext db,
    IMapper mapper
) : IQueryHandler<GetMonthlyScheduleInstanceByMonthQuery, MonthlyScheduleInstanceDto?>
{
    public async Task<MonthlyScheduleInstanceDto?> Handle(GetMonthlyScheduleInstanceByMonthQuery q, CancellationToken ct)
    {
        var instance = await db.MonthlyScheduleInstances
            .Include(i => i.Items)
                .ThenInclude(it => it.Meal)
            .FirstOrDefaultAsync(i =>
                i.ScheduleCollectionId == q.ScheduleCollectionId &&
                i.Year == q.Year &&
                i.Month == q.Month, ct);

        return instance is null ? null : mapper.Map<MonthlyScheduleInstanceDto>(instance);
    }
}
