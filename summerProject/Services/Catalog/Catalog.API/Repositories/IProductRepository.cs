using Catalog.API.Models;

namespace Catalog.API.Repositories
{
    public interface IProductRepository : IMongoRepository<Product>
    {
    }
}
