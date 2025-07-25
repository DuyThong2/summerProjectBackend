using System.Linq.Expressions;

namespace Catalog.API.Repositories
{
    public interface IMongoRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task<IEnumerable<T>> FilterByAsync(Expression<Func<T, bool>> filter);
        Task<(IEnumerable<T>, long)> GetPagedAsync(Expression<Func<T, bool>> filter, int skip, int take);

        Task AddAsync(T entity);
        Task<bool> UpdateAsync(string id, T entity);
        Task<bool> RemoveAsync(string id);
    }

}
