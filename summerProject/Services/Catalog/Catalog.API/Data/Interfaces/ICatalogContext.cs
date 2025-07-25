using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Data.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
        IMongoCollection<Meal> Meals { get; }
        IMongoCollection<Category> Category { get; }
        IMongoCollection<Package> Packages { get; }
        IMongoCollection<PackageIngredient> PackageIngredients { get; }
        IMongoCollection<Ingredient> Ingredients { get; }
    }
}
