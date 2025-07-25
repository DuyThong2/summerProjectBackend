namespace Catalog.API.Settings
{
    public interface ICatalogDatabaseSettings
    {
        string ConnectionString { get; set; } 
        string DatabaseName { get; set; }
        string ProductCollectionName { get; set; } 
        string MealCollectionName { get; set; }
        string PackageCollectionName { get; set; } 
        string IngredientCollectionName { get; set; } 
        string CategoryCollectionName { get; set; }

        string PackageIngredientCollectionName { get; set; }
    }
}
