using Catalog.API.Models;
using BuildingBlocks.CQRS;
using Catalog.API.Services;
using Catalog.API.Exceptions;
namespace Catalog.API.Queries.Meal
{

    public record GetMealByIdQuery(string Id) : IQuery<GetMealByIdResult>;
    public record GetMealByIdResult(Models.Meal Meal);

    internal class GetMealByIdHandler : IQueryHandler<GetMealByIdQuery, GetMealByIdResult>
    {
        private readonly IMealService _mealService;

        public GetMealByIdHandler(IMealService mealService)
        {
            _mealService = mealService;
        }

        public async Task<GetMealByIdResult> Handle(GetMealByIdQuery request, CancellationToken cancellationToken)
        {
            var meal = await _mealService.GetByIdAsync(request.Id);
            if (meal == null)
            {
                throw new ProductNotFoundException(request.Id);
            }

            return new GetMealByIdResult(meal);
        }
    }
}
