using Scheduling.API.Models;

namespace Scheduling.API.Repository.Impl
{
    public class ScheduleInstanceDetailRepository : GenericRepository<ScheduleInstanceDetail>, IScheduleInstanceDetailRepository
    {
        public ScheduleInstanceDetailRepository(DbContext context) : base(context) { }
    }
}
