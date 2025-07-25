using Scheduling.API.Repository.Impl;

namespace Scheduling.API.Repository
{
    public interface IMealRepository : IGenericRepository<Meal>
    {
        Task<IEnumerable<Meal>> GetMealsWithDetailsAsync();
    }

    

}
