namespace Catalog.API.Dtos.Ingredient
{
    public class IngredientInPackageDto
    {

        public string Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal UnitPrice { get; set; }    
        public double Quantity { get; set; }
        public string Unit { get; set; } = null!;
    }
}
