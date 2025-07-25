using System.Xml.Linq;
using BuildingBlocks.CQRS;
using Catalog.API.Dtos.Ingredient;
using Catalog.API.Models;
using Catalog.API.Repositories;

namespace Catalog.API.Command.Package
{
    public record UpdatePackageCommand(
    string Id,
    string Name,
    List<IngredientInPackageDto> Ingredients,
    String Description
) : ICommand<bool>;

    public class UpdatePackageCommandHandler : ICommandHandler<UpdatePackageCommand, bool>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IProductRepository _productRepository;

        public UpdatePackageCommandHandler(IPackageRepository packageRepository, IProductRepository productRepository)
        {
            _packageRepository = packageRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdatePackageCommand request, CancellationToken cancellationToken)
        {
            var existing = await _packageRepository.GetByIdAsync(request.Id);
            if (existing == null)
                return false;

            // Map lại thông tin từ DTO vào entity
            existing.Name = request.Name;

            existing.Ingredients = request.Ingredients.Select(i => new PackageIngredient
            {
                IngredientId = i.Id,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList();

            existing.Price = request.Ingredients.Sum(i => i.UnitPrice * (decimal)i.Quantity);
            existing.Description = existing.Description;

            await _productRepository.UpdateAsync(existing.Id, new Product
            {
                GlobalId = existing.GlobalId,
                Id = existing.Id,
                Name = existing.Name,
                Price = existing.Price,
                Description = existing.Description
            });

            // Update lại trong DB
            return await _packageRepository.UpdateAsync(existing.Id, existing);
        }
    }

}
