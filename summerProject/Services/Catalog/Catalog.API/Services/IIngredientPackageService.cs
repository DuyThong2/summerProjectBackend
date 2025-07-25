using Catalog.API.Models;

namespace Catalog.API.Services
{
    public interface IPackageIngredientService
    {
        Task<IEnumerable<PackageIngredient>> GetAllAsync();
        Task AddAsync(PackageIngredient entity);
        Task<bool> UpdateAsync(string id, PackageIngredient entity);
        Task<bool> RemoveAsync(string id);
    }
}
