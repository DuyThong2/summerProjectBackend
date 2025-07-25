using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public interface IMealRepository : IMongoRepository<Meal>
    {
    }
}
