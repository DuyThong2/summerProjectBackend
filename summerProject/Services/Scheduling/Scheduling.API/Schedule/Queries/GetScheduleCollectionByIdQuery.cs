using Scheduling.API.Data;


namespace Scheduling.API.Schedule.Queries
{
    public record GetScheduleCollectionByIdQuery(Guid Id)
    : IQuery<ScheduleCollectionBriefDto?>;

    public class GetScheduleCollectionByIdHandler(
        SchedulingDbContext db,
        IMapper mapper
    ) : IQueryHandler<GetScheduleCollectionByIdQuery, ScheduleCollectionBriefDto?>
    {
        public async Task<ScheduleCollectionBriefDto?> Handle(GetScheduleCollectionByIdQuery q, CancellationToken ct)
        {
            var col = await db.ScheduleCollections
                .FirstOrDefaultAsync(c => c.Id == q.Id, ct);

            return col is null ? null : mapper.Map<ScheduleCollectionBriefDto>(col);
        }
    }

}
