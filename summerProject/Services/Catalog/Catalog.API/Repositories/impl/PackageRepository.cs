using Catalog.API.Data.Interfaces;
using Catalog.API.Models;

namespace Catalog.API.Repositories.impl
{
    public class PackageRepository : MongoRepositoryBase<Package>, IPackageRepository
    {
        public PackageRepository(ICatalogContext context) : base(context.Packages) { }

    }
}
