using Catalog.API.Models;

namespace Catalog.API.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(string id);
        Task<(IEnumerable<Product> Items, long TotalCount)> GetPagedAsync(string? searchTerm, int page, int pageSize);
        Task AddAsync(Product entity);
        Task<bool> UpdateAsync(string id, Product entity);
        Task<bool> RemoveAsync(string id);
    }
}
