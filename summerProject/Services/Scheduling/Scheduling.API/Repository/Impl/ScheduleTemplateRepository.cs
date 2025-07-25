using Scheduling.API.Models;

namespace Scheduling.API.Repository.Impl
{
    

    public class ScheduleTemplateRepository : GenericRepository<ScheduleTemplate>, IScheduleTemplateRepository
    {
        public ScheduleTemplateRepository(DbContext context) : base(context) { }

        public async Task<ScheduleTemplate?> GetWithDetailsAsync(Guid id)
        {
            return await _context.Set<ScheduleTemplate>()
                .Include(t => t.TemplateDetails)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }

}
