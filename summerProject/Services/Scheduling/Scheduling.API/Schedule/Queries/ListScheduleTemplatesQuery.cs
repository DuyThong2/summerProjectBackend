using Scheduling.API.Data;
using Scheduling.API.Dto;

namespace Scheduling.API.Schedule.Queries
{
    public record ListScheduleTemplatesQuery : IQuery<IEnumerable<ScheduleTemplateDto>>;

    public class ListScheduleTemplatesHandler(
        SchedulingDbContext db,
        IMapper mapper
    ) : IQueryHandler<ListScheduleTemplatesQuery, IEnumerable<ScheduleTemplateDto>>
    {
        public async Task<IEnumerable<ScheduleTemplateDto>> Handle(ListScheduleTemplatesQuery q, CancellationToken ct)
        {
            var templates = await db.ScheduleTemplates
                .Include(t => t.TemplateDetails)
                .ToListAsync(ct);

            return mapper.Map<IEnumerable<ScheduleTemplateDto>>(templates);
        }
    }

}
