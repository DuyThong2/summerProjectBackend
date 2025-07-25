using Catalog.API.Data.Interfaces;
using Catalog.API.Models;
using Catalog.API.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(ICatalogDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Products = database.GetCollection<Product>(settings.ProductCollectionName);
            Meals = database.GetCollection<Meal>(settings.MealCollectionName);
            Category = database.GetCollection<Category>(settings.CategoryCollectionName);
            Packages = database.GetCollection<Package>(settings.PackageCollectionName);
            PackageIngredients = database.GetCollection<PackageIngredient>(settings.PackageIngredientCollectionName);
            Ingredients = database.GetCollection<Ingredient>(settings.IngredientCollectionName);

            // Optional: seed only Products or any other collection
            CatalogContextSeed.SeedData(Products, Meals, Category, Packages, Ingredients);
        }

        public IMongoCollection<Product> Products { get; }
        public IMongoCollection<Meal> Meals { get; }
        public IMongoCollection<Category> Category { get; }
        public IMongoCollection<Package> Packages { get; }
        public IMongoCollection<PackageIngredient> PackageIngredients { get; }
        public IMongoCollection<Ingredient> Ingredients { get; }
    }
}
