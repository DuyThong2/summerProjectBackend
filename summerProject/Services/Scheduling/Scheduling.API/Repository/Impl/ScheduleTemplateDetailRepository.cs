using Scheduling.API.Models;

namespace Scheduling.API.Repository.Impl
{
    public class ScheduleTemplateDetailRepository : GenericRepository<ScheduleTemplateDetail>, IScheduleTemplateDetailRepository
    {
        public ScheduleTemplateDetailRepository(DbContext context) : base(context) { }
    }
}
