using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Services;
using MediatR;

namespace Catalog.API.Command.Meal
{

    public record CreateMealCommand(string Name,
        string? Description, decimal Price, string? CategoryName) : ICommand<CreateMealResult>
    { }
    public record CreateMealResult(string Id) { }
    public class CreateMealCommandHandler(IMealService mealService, ICategoryService categoryService, IProductService productService) : ICommandHandler<CreateMealCommand, CreateMealResult>
    {
        public async Task<CreateMealResult> Handle(CreateMealCommand request, CancellationToken cancellationToken)
        {
            string? categoryId = await categoryService.GetOrCreateCategoryIdAsync(request.CategoryName);



            var meal = new Models.Meal
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = categoryId
            };

            

            await mealService.AddAsync(meal);

            var product = Product.generateFromExistingProduct(meal);
            product.Id = meal.Id;
            product.GlobalId = meal.GlobalId;
            productService.AddAsync(product);
            return new CreateMealResult(meal.Id);
        }


        
    }
}