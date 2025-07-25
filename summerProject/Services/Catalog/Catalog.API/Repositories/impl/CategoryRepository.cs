using System.Text.RegularExpressions;
using Catalog.API.Data.Interfaces;
using Catalog.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.API.Repositories.impl
{
    public class CategoryRepository : MongoRepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ICatalogContext context)
            : base(context.Category)
        {
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            var filter = Builders<Category>.Filter.Regex(c => c.Name,
                    new BsonRegularExpression($"^{Regex.Escape(name)}$", "i"));

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        

    }
}
