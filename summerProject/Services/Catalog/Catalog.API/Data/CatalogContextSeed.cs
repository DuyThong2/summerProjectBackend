using Catalog.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;

public static class CatalogContextSeed
{
    private static readonly string CategoryFoodId = "6878c516bc0ad4769b229a40";
    private static readonly string CategoryDrinkId = "6878c516bc0ad4769b229a41";
    private static readonly string RiceId = "6878c516bc0ad4769b229a42";
    private static readonly string ChickenId = "6878c516bc0ad4769b229a43";
    private static readonly string PackageStandardId = "6878c516bc0ad4769b229a44";
    private static readonly string SampleProductId = ObjectId.GenerateNewId().ToString();
    private static readonly string SampleMealId = "6878c516bc0ad4769b229a46";
    private static readonly Guid mealGuid = Guid.Parse("93170c85-7795-489c-8e8f-7dcf3b4f4188");
    private static readonly Guid packageGuid = Guid.Parse("5334c996-8457-4cf0-815c-ed2b77c4ff61");

    public static void SeedData(
        IMongoCollection<Product> productCollection,
        IMongoCollection<Meal> mealCollection,
        IMongoCollection<Category> categoryCollection,
        IMongoCollection<Package> packageCollection,
        IMongoCollection<Ingredient> ingredientCollection)
    {
        if (!productCollection.Find(p => true).Any())
        {
            productCollection.InsertMany(GetPreconfiguredProducts());
        }

        if (!categoryCollection.Find(p => true).Any())
        {
            categoryCollection.InsertMany(GetPreconfiguredCategories());
        }

        if (!ingredientCollection.Find(p => true).Any())
        {
            ingredientCollection.InsertMany(GetPreconfiguredIngredients());
        }

        if (!packageCollection.Find(p => true).Any())
        {
            packageCollection.InsertMany(GetPreconfiguredPackages());
        }

        if (!mealCollection.Find(p => true).Any())
        {
            mealCollection.InsertMany(GetPreconfiguredMeals());
        }
    }

    private static IEnumerable<Product> GetPreconfiguredProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Id = PackageStandardId,
                Name = "Standard Package",
                Price = 30,
                GlobalId = packageGuid,
                Description = "Includes rice and chicken"
            },
            new Product
            {
                Id = SampleMealId,
                Name = "Chicken Rice Meal",
                Description = "Delicious rice with chicken",
                GlobalId = mealGuid,
                Price = 45,
            }
        };
    }

    private static IEnumerable<Category> GetPreconfiguredCategories()
    {
        return new List<Category>
        {
            new Category { Id = CategoryFoodId, Name = "Food" },
            new Category { Id = CategoryDrinkId, Name = "Drink" }
        };
    }

    private static IEnumerable<Ingredient> GetPreconfiguredIngredients()
    {
        return new List<Ingredient>
        {
            new Ingredient
            {
                Id = RiceId,
                Name = "Rice",
                Unit = "gram",
                Description = "White rice",
                Price = 0.1m
            },
            new Ingredient
            {
                Id = ChickenId,
                Name = "Chicken",
                Unit = "gram",
                Description = "Fresh chicken",
                Price = 0.2m
            }
        };
    }

    private static IEnumerable<Package> GetPreconfiguredPackages()
    {
        return new List<Package>
        {
            new Package
            {
                Id = PackageStandardId,
                Name = "Standard Package",
                Price = 30,
                Description = "Includes rice and chicken",
                GlobalId = packageGuid,

                Ingredients = new List<PackageIngredient>
                {
                    new PackageIngredient { IngredientId = RiceId, Quantity = 100, UnitPrice = 0.1m },
                    new PackageIngredient { IngredientId = ChickenId, Quantity = 200, UnitPrice = 0.2m }
                }
            }
        };
    }

    private static IEnumerable<Meal> GetPreconfiguredMeals()
    {
        return new List<Meal>
        {
            new Meal
            {
                Id = SampleMealId,
                Name = "Chicken Rice Meal",
                Description = "Delicious rice with chicken",
                Price = 45,
                GlobalId = mealGuid,
                CategoryId = CategoryFoodId,
                PackageId = PackageStandardId
            }
        };
    }
}
