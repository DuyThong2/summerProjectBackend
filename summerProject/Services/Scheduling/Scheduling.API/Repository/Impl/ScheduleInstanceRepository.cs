using Scheduling.API.Models;

namespace Scheduling.API.Repository.Impl
{
   

    public class ScheduleInstanceRepository : GenericRepository<ScheduleInstance>, IScheduleInstanceRepository
    {
        public ScheduleInstanceRepository(DbContext context) : base(context) { }

        public async Task<ScheduleInstance?> GetWithDetailsAsync(Guid id)
        {
            return await _context.Set<ScheduleInstance>()
                .Include(i => i.InstanceDetails)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }

}
