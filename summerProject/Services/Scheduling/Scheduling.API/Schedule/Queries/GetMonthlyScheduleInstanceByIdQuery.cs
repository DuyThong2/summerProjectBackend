using Scheduling.API.Data;
using Scheduling.API.Dto;

namespace Scheduling.API.Schedule.Queries
{
    public record GetMonthlyScheduleInstanceByIdQuery(Guid Id)
    : IQuery<MonthlyScheduleInstanceDto?>;

    public class GetMonthlyScheduleInstanceByIdHandler(
        SchedulingDbContext db,
        IMapper mapper
    ) : IQueryHandler<GetMonthlyScheduleInstanceByIdQuery, MonthlyScheduleInstanceDto?>
    {
        public async Task<MonthlyScheduleInstanceDto?> Handle(GetMonthlyScheduleInstanceByIdQuery q, CancellationToken ct)
        {
            var instance = await db.MonthlyScheduleInstances
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == q.Id, ct);

            return instance is null ? null : mapper.Map<MonthlyScheduleInstanceDto>(instance);
        }
    }

}
