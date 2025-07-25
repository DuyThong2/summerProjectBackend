namespace Catalog.API.Dtos.Package
{
    public class PackageDetailDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public List<Ingredient.IngredientInPackageDto> Ingredients { get; set; } = new();
        public decimal TotalPrice { get; set; }
    }
}
