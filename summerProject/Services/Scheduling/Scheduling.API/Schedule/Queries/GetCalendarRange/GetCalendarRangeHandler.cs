using Scheduling.API.Data;
using Scheduling.API.Dto;

namespace Scheduling.API.Schedule.Queries.GetCalendarRange
{
    public class GetCalendarRangeHandler(
        SchedulingDbContext db,
        IMapper mapper
) : IQueryHandler<GetCalendarRangeQuery, GetCalendarRangeResult>
    {
        public async Task<GetCalendarRangeResult> Handle(GetCalendarRangeQuery q, CancellationToken ct)
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
                    i.Date >= q.From.Date && i.Date <= q.To.Date)
                .ToListAsync(ct);

            var days = items
                .GroupBy(i => i.Date.Date)
                .Select(g => new CalendarDayDto(
                    g.Key,
                    g.GroupBy(i => i.TimeSlot)
                     .Select(slot => new CalendarSlotDto(
                         slot.Key,
                         mapper.Map<List<CalendarMealDto>>(slot.ToList())
                     )).ToList()
                ))
                .OrderBy(d => d.Date)
                .ToList();

            return new GetCalendarRangeResult(q.UserId, q.From, q.To, days);
        }
    }
}
