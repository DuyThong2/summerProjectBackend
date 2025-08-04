using Scheduling.API.Data;
using Scheduling.API.Dto;

namespace Scheduling.API.Schedule.Queries
{
    public record GetMonthlyScheduleInstancesByUserQuery(
    string UserId,
    int? Year,
    int? Month
) : IQuery<IEnumerable<MonthlyScheduleInstanceDto>>;

    public class GetMonthlyScheduleInstancesByUserHandler(
        SchedulingDbContext db,
        IMapper mapper
    ) : IQueryHandler<GetMonthlyScheduleInstancesByUserQuery, IEnumerable<MonthlyScheduleInstanceDto>>
    {
        public async Task<IEnumerable<MonthlyScheduleInstanceDto>> Handle(GetMonthlyScheduleInstancesByUserQuery q, CancellationToken ct)
        {
            var collectionIds = await db.ScheduleCollections
                .Where(c => c.UserId == q.UserId)
                .Select(c => c.Id)
                .ToListAsync(ct);

            var query = db.MonthlyScheduleInstances
                .Include(i => i.Items)
                .Where(i => collectionIds.Contains(i.ScheduleCollectionId));

            if (q.Year.HasValue) query = query.Where(i => i.Year == q.Year.Value);
            if (q.Month.HasValue) query = query.Where(i => i.Month == q.Month.Value);

            var list = await query.ToListAsync(ct);
            return mapper.Map<IEnumerable<MonthlyScheduleInstanceDto>>(list);
        }
    }

}
