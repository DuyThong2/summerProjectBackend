using Catalog.API.Models;
using Catalog.API.Repositories;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Catalog.API.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _repository;
        private readonly IProductRepository _productRepository;

        public MealService(IMealRepository repository, IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }

        public Task<IEnumerable<Meal>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Meal?> GetByIdAsync(string id) => _repository.GetByIdAsync(id);

        public Task AddAsync(Meal entity) {

            _productRepository.AddAsync(new Product
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price
            }
            );
            return _repository.AddAsync(entity);
        }
        public Task<bool> UpdateAsync(string id, Meal entity)
        {
            _productRepository.UpdateAsync(id, new Product
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price
            });
            return _repository.UpdateAsync(id, entity);
        }
        public Task<bool> RemoveAsync(string id) => _repository.RemoveAsync(id);

        public async Task<(IEnumerable<Meal>, long)> GetPagedAsync(string? searchTerm, int page, int pageSize)
        {
            Expression<Func<Meal, bool>> filter = m =>
                string.IsNullOrEmpty(searchTerm) ||
                m.Name.ToLower().Contains(searchTerm.ToLower());

            return await _repository.GetPagedAsync(filter, (page - 1) * pageSize, pageSize);
        }

        public async Task<(IEnumerable<Meal> Items, long TotalCount)> GetPagedAsync(int pageIndex, int pageSize)
        {
            var skip = (pageIndex - 1) * pageSize;
            var filter = Builders<Meal>.Filter.Empty;
            return await _repository.GetPagedAsync(null, skip, pageSize);
        }
    }
}
