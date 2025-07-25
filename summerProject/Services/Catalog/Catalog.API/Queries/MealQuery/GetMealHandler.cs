using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Catalog.API.Models;
using Catalog.API.Services;
using System.Reflection;

public record GetMealQuery(PaginationRequest PaginationRequest) : IQuery<GetMealResult>;

public record GetMealResult(PaginatedResult<Meal> Meals);

public class GetMealHandler : IQueryHandler<GetMealQuery, GetMealResult>
{
    private readonly IMealService _mealService;

    public GetMealHandler(IMealService mealService)
    {
        _mealService = mealService;
    }

    public async Task<GetMealResult> Handle(GetMealQuery request, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _mealService.GetPagedAsync(request.PaginationRequest.PageIndex, request.PaginationRequest.PageSize);

        var result = new PaginatedResult<Meal>(
            request.PaginationRequest.PageIndex,
            request.PaginationRequest.PageSize,
            totalCount,
            items
        );

        return new GetMealResult(result);
    }
}
