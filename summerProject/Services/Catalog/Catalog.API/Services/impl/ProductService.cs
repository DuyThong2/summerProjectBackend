using Catalog.API.Models;
using Catalog.API.Repositories;
using System.Linq.Expressions;

namespace Catalog.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Product>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Product?> GetByIdAsync(string id) => _repository.GetByIdAsync(id);
        public Task AddAsync(Product entity) => _repository.AddAsync(entity);
        public Task<bool> UpdateAsync(string id, Product entity) => _repository.UpdateAsync(id, entity);
        public Task<bool> RemoveAsync(string id) => _repository.RemoveAsync(id);

        public async Task<(IEnumerable<Product>, long)> GetPagedAsync(string? searchTerm, int page, int pageSize)
        {
            Expression<Func<Product, bool>> filter = p =>
                string.IsNullOrEmpty(searchTerm) ||
                p.Name.ToLower().Contains(searchTerm.ToLower());

            return await _repository.GetPagedAsync(filter, (page - 1) * pageSize, pageSize);
        }

        
    }
}
