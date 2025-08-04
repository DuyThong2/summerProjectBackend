namespace Scheduling.API.Repository
{
    public interface IScheduleTemplateRepository : IGenericRepository<ScheduleTemplate>
    {
        Task<ScheduleTemplate?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
    }

}
