using System;
using Catalog.API.Models;

namespace Catalog.API.Repositories
{
    public interface ICategoryRepository : IMongoRepository<Category>
    {
        Task<Category?> GetByNameAsync(string name);

       


    }
}
