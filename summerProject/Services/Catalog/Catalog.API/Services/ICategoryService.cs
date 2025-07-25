using Catalog.API.Models;

namespace Catalog.API.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(string id);
        Task<(IEnumerable<Category> Items, long TotalCount)> GetPagedAsync(string? searchTerm, int page, int pageSize);
        Task AddAsync(Category entity);
        Task<bool> UpdateAsync(string id, Category entity);
        Task<bool> RemoveAsync(string id);

        Task<Category?> GetByNameAsync(string name);

        Task<string?> GetOrCreateCategoryIdAsync(string categoryName);


    }
}
