using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Catalog.API.Services;

namespace Catalog.API.Command.Meal
{
    public record UpdateMealCommand(string Id,string Name,string? Description,decimal Price,string? CategoryName) : ICommand<bool>;
    public class UpdateMealCommandHandler(IMealService mealService,ICategoryService categoryService, IProductService productService) : ICommandHandler<UpdateMealCommand, bool>
    {
        public async Task<bool> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
        {
            var existingMeal = await mealService.GetByIdAsync(request.Id);
            if (existingMeal == null)
               throw new ProductNotFoundException(request.Id);

            string? categoryId = await categoryService.GetOrCreateCategoryIdAsync(request.CategoryName);


            existingMeal.Name = request.Name;
            existingMeal.Description = request.Description;
            existingMeal.Price = request.Price;
            existingMeal.CategoryId = categoryId;

            var updatedProduct = Product.generateFromExistingProduct(existingMeal);
            productService.UpdateAsync(updatedProduct.Id, updatedProduct);
            return await mealService.UpdateAsync(existingMeal.Id, existingMeal);
        }
    }
}
