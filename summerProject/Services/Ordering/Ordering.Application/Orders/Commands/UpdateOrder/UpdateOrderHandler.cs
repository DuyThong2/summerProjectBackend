using AutoMapper;

namespace Ordering.Application.Orders.Commands.UpdateOrder;
public class UpdateOrderHandler(IApplicationDbContext dbContext, IMapper mapper)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        //Update Order entity from command object
        //save to database
        //return result

        var orderId = OrderId.Of(command.Order.Id);
        var order = await dbContext.Orders
                                     .Include(o => o.OrderItems) 
                                     .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Order.Id);
        }

        UpdateOrderWithNewValues(order, command.Order);



        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);        
    }

    public void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
    {
        var updatedShippingAddress = mapper.Map<Address>(orderDto.ShippingAddress);
        var updatedBillingAddress = mapper.Map<Address>(orderDto.BillingAddress);
        var updatedPayment = mapper.Map<Payment>(orderDto.Payment);

        order.Update(
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: updatedShippingAddress,
            billingAddress: updatedBillingAddress,
            payment: updatedPayment,
            status: orderDto.Status);

        order.ClearAllItems(); 

        foreach (var itemDto in orderDto.OrderItems)
        {
            order.Add(ProductId.Of(itemDto.ProductId), itemDto.Quantity, itemDto.Price);
        }
    }
}
