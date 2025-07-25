using Catalog.API.Models;
using Catalog.API.Repositories;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Catalog.API.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _repository;

        public IngredientService(IIngredientRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Ingredient>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Ingredient?> GetByIdAsync(string id) => _repository.GetByIdAsync(id);
        public Task AddAsync(Ingredient entity) => _repository.AddAsync(entity);
        public Task<bool> UpdateAsync(string id, Ingredient entity) => _repository.UpdateAsync(id, entity);
        public Task<bool> RemoveAsync(string id) => _repository.RemoveAsync(id);

        public async Task<(IEnumerable<Ingredient>, long)> GetPagedAsync(string? searchTerm, int page, int pageSize)
        {
            Expression<Func<Ingredient, bool>> filter = i =>
                string.IsNullOrEmpty(searchTerm) ||
                i.Name.ToLower().Contains(searchTerm.ToLower());

            return await _repository.GetPagedAsync(filter, (page - 1) * pageSize, pageSize);
        }

        public async Task<(IEnumerable<Ingredient> Items, long TotalCount)> GetPagedAsync(int pageIndex, int pageSize)
        {
            var skip = (pageIndex - 1) * pageSize;
            var filter = Builders<Ingredient>.Filter.Empty;
            return await _repository.GetPagedAsync(null, skip, pageSize);
        }
    }
}
