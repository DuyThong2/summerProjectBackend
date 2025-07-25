using Catalog.API.Models;
using Catalog.API.Repositories;
using System.Linq.Expressions;

namespace Catalog.API.Services
{
    public class PackageIngredientService : IPackageIngredientService
    {
        private readonly IPackageIngredientRepository _repository;

        public PackageIngredientService(IPackageIngredientRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<PackageIngredient>> GetAllAsync() => _repository.GetAllAsync();
        public Task AddAsync(PackageIngredient entity) => _repository.AddAsync(entity);
        public Task<bool> UpdateAsync(string id, PackageIngredient entity) => _repository.UpdateAsync(id, entity);
        public Task<bool> RemoveAsync(string id) => _repository.RemoveAsync(id);

        
    }
}
