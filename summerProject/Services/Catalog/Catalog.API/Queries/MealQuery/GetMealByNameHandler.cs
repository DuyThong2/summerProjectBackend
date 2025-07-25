using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Catalog.API.Services;

namespace Catalog.API.Queries.Meal
{

    public record GetMealByNameQuery(string? SearchTerm, PaginationRequest PaginationRequest)
     : IQuery<GetMealByNameResult>;

    public record GetMealByNameResult(PaginatedResult<Models.Meal> Meals);
    public class GetMealByNameHandler : IQueryHandler<GetMealByNameQuery, GetMealByNameResult>
    {
        private readonly IMealService _mealService;

        public GetMealByNameHandler(IMealService mealService)
        {
            _mealService = mealService;
        }

        public async Task<GetMealByNameResult> Handle(GetMealByNameQuery request, CancellationToken cancellationToken)
        {
            var (items, totalCount) = await _mealService.GetPagedAsync(
                request.SearchTerm,
                request.PaginationRequest.PageIndex,
                request.PaginationRequest.PageSize
            );

            var result = new PaginatedResult<Models.Meal>(
                request.PaginationRequest.PageIndex,
                request.PaginationRequest.PageSize,
                totalCount,
                items
            );

            return new GetMealByNameResult(result);
        }
    }

}
