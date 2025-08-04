using Scheduling.API.Data;
using Scheduling.API.Dto;

namespace Scheduling.API.Schedule.Queries
{
    public record GetMonthlyScheduleItemByIdQuery(Guid Id)
    : IQuery<MonthlyScheduleItemDto?>;

    public class GetMonthlyScheduleItemByIdHandler(
        SchedulingDbContext db,
        IMapper mapper
    ) : IQueryHandler<GetMonthlyScheduleItemByIdQuery, MonthlyScheduleItemDto?>
    {
        public async Task<MonthlyScheduleItemDto?> Handle(GetMonthlyScheduleItemByIdQuery q, CancellationToken ct)
        {
            var item = await db.MonthlyScheduleItems
                .FirstOrDefaultAsync(i => i.Id == q.Id, ct);

            return item is null ? null : mapper.Map<MonthlyScheduleItemDto>(item);
        }
    }

}
