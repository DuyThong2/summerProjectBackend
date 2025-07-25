using BuildingBlocks.CQRS;
using Catalog.API.Services;

namespace Catalog.API.Command.Meal
{

    public record DeleteMealCommand(string Id) : ICommand<bool>;

    public class DeleteMealCommandHandler(IMealService mealService, IPackageService packageService)
    : ICommandHandler<DeleteMealCommand, bool>
    {
        public async Task<bool> Handle(DeleteMealCommand request, CancellationToken cancellationToken)
        {
            var meal = await mealService.GetByIdAsync(request.Id);
            if (meal == null) return false;

            var deleted = await mealService.RemoveAsync(request.Id);

            if (!string.IsNullOrWhiteSpace(meal.PackageId))
            {
                await packageService.RemoveAsync(meal.PackageId);
            }

            return deleted;
        }
    }
}
