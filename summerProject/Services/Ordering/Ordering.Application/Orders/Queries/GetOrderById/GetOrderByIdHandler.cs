using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Ordering.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdHandler(IApplicationDbContext dbContext, IMapper mapper)
    : IQueryHandler<GetOrderByIdQuery, GetOrderByIdResult>
    {
        public async Task<GetOrderByIdResult> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var order = await dbContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == OrderId.Of(query.OrderId), cancellationToken);

            if (order is null)
            {
                throw new Exception("Order not found"); 
            }

            return new GetOrderByIdResult(mapper.Map<OrderDto>(order));
        }
    }
}
