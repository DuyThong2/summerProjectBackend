using Scheduling.API.Models;
using Scheduling.API.Repository.Impl;

namespace Scheduling.API.Repository
{
    public interface IScheduleInstanceRepository : IGenericRepository<ScheduleInstance>
    {
        Task<ScheduleInstance?> GetWithDetailsAsync(Guid id);
    }

   

}
