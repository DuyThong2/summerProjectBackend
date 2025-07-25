using BuildingBlocks.CQRS;
using Catalog.API.Dtos.Ingredient;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Catalog.API.Repositories;
using ZstdSharp.Unsafe;

namespace Catalog.API.Command.Package
{

    public record CreatePackageCommand(string mealId,
    string Name,
    List<IngredientInPackageDto> Ingredients
) : ICommand<string>; 

    public class CreatePackageCommandHandler : ICommandHandler<CreatePackageCommand, string>
    {
        private readonly IPackageRepository _packageRepository;

        private readonly IMealRepository _mealRepository;

        private readonly IProductRepository _productRepository;

        public CreatePackageCommandHandler(IPackageRepository packageRepository, IMealRepository mealRepository, IProductRepository productRepository)
        {
            _packageRepository = packageRepository;
            _mealRepository = mealRepository;
            _productRepository = productRepository;
        }

        public async Task<string> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
        {
            Models.Meal ? existingMeal = await _mealRepository.GetByIdAsync(request.mealId);

            if (existingMeal != null)
            {
                var newPackage = new Models.Package
                {
                    Name = request.Name,
                    Ingredients = request.Ingredients.Select(i => new PackageIngredient
                    {
                        IngredientId = i.Id,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }
                    
                   
                    ).ToList(),

                    Price = request.Ingredients.Sum(i => i.UnitPrice * (decimal)i.Quantity)
                };
                await _packageRepository.AddAsync(newPackage);
                

                existingMeal.PackageId = newPackage.Id;
                await _mealRepository.UpdateAsync(existingMeal.Id, existingMeal);
                await _productRepository.AddAsync(new Product
                {
                    GlobalId = newPackage.GlobalId,
                    Id = newPackage.Id,
                    Name = newPackage.Name,
                    Price = newPackage.Price,
                    Description = newPackage.Description,
                });

                return newPackage.Id!;
            }

            throw new ProductNotFoundException(request.mealId);
        }
    }

}
