using Catalog.API.Data.Interfaces;
using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Repositories.impl
{
    public class IngredientRepository : MongoRepositoryBase<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(ICatalogContext context) 
            : base(context.Ingredients)
        {
        }
    }
}
