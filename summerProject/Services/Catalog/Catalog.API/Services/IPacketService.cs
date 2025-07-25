using Catalog.API.Dtos.Package;
using Catalog.API.Models;
using Catalog.API.Queries.PackageQuery;

namespace Catalog.API.Services
{
    public interface IPackageService
    {
        Task<IEnumerable<Package>> GetAllAsync();
        Task<Package?> GetByIdAsync(string id);
        Task<(IEnumerable<Package> Items, long TotalCount)> GetPagedAsync(string? searchTerm, int page, int pageSize);
        Task AddAsync(Package entity);
        Task<bool> UpdateAsync(string id, Package entity);
        Task<bool> RemoveAsync(string id);
        Task<PackageDetailDto> GetPackageDetailAsync(string packageId);
    }
}
