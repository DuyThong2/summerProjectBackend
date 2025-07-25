using Catalog.API.Models;

namespace Catalog.API.Services
{
    public interface IMealService
    {
        Task<IEnumerable<Meal>> GetAllAsync();
        Task<Meal?> GetByIdAsync(string id);
        Task<(IEnumerable<Meal> Items, long TotalCount)> GetPagedAsync(string? searchTerm, int page, int pageSize);

        Task<(IEnumerable<Meal> Items, long TotalCount)> GetPagedAsync(int pageIndex, int pageSize);

        Task AddAsync(Meal entity);
        Task<bool> UpdateAsync(string id, Meal entity);
        Task<bool> RemoveAsync(string id);
    }
}
