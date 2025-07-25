namespace Catalog.API.Settings
{
    public class CatalogDatabaseSettings : ICatalogDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ProductCollectionName { get; set; } = null!;
        public string MealCollectionName { get; set; } = null!;
        public string PackageCollectionName { get; set; } = null!;
        public string IngredientCollectionName { get; set; } = null!;
        public string CategoryCollectionName { get; set; } = null!;

        public string PackageIngredientCollectionName { get; set; } = null!;

    }
}
