using Catalog.API.Models;
using Catalog.API.Repositories;
using Catalog.API.Repositories.impl;
using System.Linq.Expressions;

namespace Catalog.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Category>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Category?> GetByIdAsync(string id) => _repository.GetByIdAsync(id);
        public Task AddAsync(Category entity) => _repository.AddAsync(entity);
        public Task<bool> UpdateAsync(string id, Category entity) => _repository.UpdateAsync(id, entity);
        public Task<bool> RemoveAsync(string id) => _repository.RemoveAsync(id);

        public async Task<(IEnumerable<Category>, long)> GetPagedAsync(string? searchTerm, int page, int pageSize)
        {
            Expression<Func<Category, bool>> filter = c =>
                string.IsNullOrEmpty(searchTerm) ||
                c.Name.ToLower().Contains(searchTerm.ToLower());

            return await _repository.GetPagedAsync(filter, (page - 1) * pageSize, pageSize);
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _repository.GetByNameAsync(name);
        }

        public async Task<string?> GetOrCreateCategoryIdAsync(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return null;

            var existing = await GetByNameAsync(categoryName);
            if (existing != null)
                return existing.Id;

            var newCategory = new Category { Name = categoryName };
            await AddAsync(newCategory);
            return newCategory.Id;
        }


    }
}
