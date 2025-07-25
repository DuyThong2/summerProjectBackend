using Catalog.API.Data.Interfaces;
using Catalog.API.Models;

namespace Catalog.API.Repositories.impl
{
    public class ProductRepository : MongoRepositoryBase<Product>, IProductRepository
    {

        public ProductRepository(ICatalogContext context) : base(context.Products) { }
    }
}
