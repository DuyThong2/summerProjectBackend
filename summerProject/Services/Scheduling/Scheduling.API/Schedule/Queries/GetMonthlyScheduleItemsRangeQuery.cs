using Scheduling.API.Data;
using Scheduling.API.Dto;

namespace Scheduling.API.Schedule.Queries
{
    public record GetMonthlyScheduleItemsRangeQuery(
    string UserId,
    DateTime From,
    DateTime To
) : IQuery<IEnumerable<MonthlyScheduleItemDto>>;

    public class GetMonthlyScheduleItemsRangeHandler(
        SchedulingDbContext db,
        IMapper mapper
    ) : IQueryHandler<GetMonthlyScheduleItemsRangeQuery, IEnumerable<MonthlyScheduleItemDto>>
    {
        public async Task<IEnumerable<MonthlyScheduleItemDto>> Handle(GetMonthlyScheduleItemsRangeQuery q, CancellationToken ct)
        {
            var collectionIds = await db.ScheduleCollections
                .Where(c => c.UserId == q.UserId)
                .Select(c => c.Id)
                .ToListAsync(ct);

            var items = await db.MonthlyScheduleItems
                .Include(i => i.Meal)
                .Include(i => i.MonthlyScheduleInstance)
                .Where(i =>
                    collectionIds.Contains(i.MonthlyScheduleInstance.ScheduleCollectionId) &&
                    i.Date >= q.From && i.Date <= q.To)
                .ToListAsync(ct);

            return mapper.Map<IEnumerable<MonthlyScheduleItemDto>>(items);
        }
    }


}
