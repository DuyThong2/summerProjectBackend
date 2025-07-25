using System.Linq.Expressions;
using MongoDB.Driver;

namespace Catalog.API.Repositories.impl
{
    public class MongoRepositoryBase<T> : IMongoRepository<T> where T : class
    {

        protected readonly IMongoCollection<T> _collection;

        public MongoRepositoryBase(IMongoCollection<T> collection)
        {
            _collection = collection;
        }


        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<IEnumerable<T>> FilterByAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        

        public async Task<T?> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<(IEnumerable<T>, long)> GetPagedAsync(Expression<Func<T, bool>> filter, int skip, int take)
        {
            filter ??= _ => true; // Nếu filter null, mặc định trả về tất cả

            var totalCount = _collection.CountDocuments(filter);
            var items = await _collection.Find(filter).Skip(skip).Limit(take).ToListAsync();
            return (items, totalCount);
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = await _collection.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<bool> UpdateAsync(string id, T entity)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = await _collection.ReplaceOneAsync(filter, entity);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }


    }
}
