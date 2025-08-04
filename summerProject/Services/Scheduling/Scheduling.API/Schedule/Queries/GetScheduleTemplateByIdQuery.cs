using Scheduling.API.Data;
using Scheduling.API.Dto;

namespace Scheduling.API.Schedule.Queries
{
    public record GetScheduleTemplateByIdQuery(Guid Id) : IQuery<ScheduleTemplateDto?>;

    public class GetScheduleTemplateByIdHandler(
        SchedulingDbContext db,
        IMapper mapper
    ) : IQueryHandler<GetScheduleTemplateByIdQuery, ScheduleTemplateDto?>
    {
        public async Task<ScheduleTemplateDto?> Handle(GetScheduleTemplateByIdQuery q, CancellationToken ct)
        {
            var template = await db.ScheduleTemplates
                .Include(x => x.TemplateDetails)
                .FirstOrDefaultAsync(x => x.Id == q.Id, ct);

            return template is null ? null : mapper.Map<ScheduleTemplateDto>(template);
        }
    }

}
