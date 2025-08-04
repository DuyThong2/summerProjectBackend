namespace Scheduling.API.Repository.Impl
{
    using Microsoft.EntityFrameworkCore;
    using Scheduling.API.Data;

    public class ScheduleTemplateRepository
        : GenericRepository<ScheduleTemplate>, IScheduleTemplateRepository
    {
        private readonly SchedulingDbContext _db;

        public ScheduleTemplateRepository(SchedulingDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ScheduleTemplate?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.ScheduleTemplates
                .Include(t => t.TemplateDetails)
                .ThenInclude(d => d.Meal)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }
    }

}
