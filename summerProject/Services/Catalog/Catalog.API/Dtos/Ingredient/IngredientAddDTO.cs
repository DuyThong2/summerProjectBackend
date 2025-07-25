namespace Catalog.API.Dtos.Ingredient
{
    public class IngredientAddDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public string Unit { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
