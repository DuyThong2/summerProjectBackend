using Catalog.API.Models;

namespace Catalog.API.Repositories
{
    public interface IPackageRepository : IMongoRepository<Package>
    {
    }
}
