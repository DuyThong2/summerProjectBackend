using Catalog.API.Data.Interfaces;
using Catalog.API.Models;

namespace Catalog.API.Repositories.impl
{
    public class PackageIngredientRepository : MongoRepositoryBase<PackageIngredient>, IPackageIngredientRepository
    {

        public PackageIngredientRepository(ICatalogContext context) : base(context.PackageIngredients) { }
    }
}
