using Catalog.API.Models;

namespace Catalog.API.Services
{
    public interface IIngredientService
    {
        Task<IEnumerable<Ingredient>> GetAllAsync();
        Task<Ingredient?> GetByIdAsync(string id);
        Task<(IEnumerable<Ingredient> Items, long TotalCount)> GetPagedAsync(string? searchTerm, int page, int pageSize);
        Task AddAsync(Ingredient entity);
        Task<bool> UpdateAsync(string id, Ingredient entity);
        Task<bool> RemoveAsync(string id);

        Task<(IEnumerable<Ingredient> Items, long TotalCount)> GetPagedAsync(int pageIndex, int pageSize);

    }
}
