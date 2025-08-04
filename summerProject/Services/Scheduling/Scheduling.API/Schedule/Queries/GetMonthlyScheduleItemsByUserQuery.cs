using Scheduling.API.Data;
using Scheduling.API.Dto;

namespace Scheduling.API.Schedule.Queries
{
    public record GetMonthlyScheduleItemsByUserQuery(
    string UserId,
    DateTime? From,
    DateTime? To
) : IQuery<IEnumerable<MonthlyScheduleItemDto>>;

    public class GetMonthlyScheduleItemsByUserHandler(
        SchedulingDbContext db,
        IMapper mapper
    ) : IQueryHandler<GetMonthlyScheduleItemsByUserQuery, IEnumerable<MonthlyScheduleItemDto>>
    {
        public async Task<IEnumerable<MonthlyScheduleItemDto>> Handle(GetMonthlyScheduleItemsByUserQuery q, CancellationToken ct)
        {
            var collectionIds = await db.ScheduleCollections
                .Where(c => c.UserId == q.UserId)
                .Select(c => c.Id)
                .ToListAsync(ct);

            var query = db.MonthlyScheduleItems
                .Include(i => i.MonthlyScheduleInstance)
                .Where(i => collectionIds.Contains(i.MonthlyScheduleInstance.ScheduleCollectionId));

            if (q.From.HasValue) query = query.Where(i => i.Date >= q.From.Value.Date);
            if (q.To.HasValue) query = query.Where(i => i.Date <= q.To.Value.Date);

            var list = await query.ToListAsync(ct);
            return mapper.Map<IEnumerable<MonthlyScheduleItemDto>>(list);
        }
    }

}
