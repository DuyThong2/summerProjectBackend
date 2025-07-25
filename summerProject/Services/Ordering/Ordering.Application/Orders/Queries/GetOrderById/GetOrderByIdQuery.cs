namespace Ordering.Application.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid OrderId) : IQuery<GetOrderByIdResult>;

public record GetOrderByIdResult(OrderDto Order);
