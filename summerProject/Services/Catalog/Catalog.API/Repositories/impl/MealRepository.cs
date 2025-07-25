using Catalog.API.Data.Interfaces;
using Catalog.API.Models;

namespace Catalog.API.Repositories.impl
{
    public class MealRepository : MongoRepositoryBase<Meal>, IMealRepository
    {

        public MealRepository(ICatalogContext context) : base (context.Meals) { }
    }
}
