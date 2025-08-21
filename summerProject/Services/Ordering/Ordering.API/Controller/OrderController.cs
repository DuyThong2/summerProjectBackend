using BuildingBlocks.Pagination;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Application.Orders.Commands.DeleteOrder;
using Ordering.Application.Orders.Commands.UpdateOrder;
using Ordering.Application.Orders.Queries.GetOrderById;
using Ordering.Application.Orders.Queries.GetOrders;
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;
using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/order/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var result = await _mediator.Send(new GetOrderByIdQuery(id));
        if (result.Order == null)
            return NotFound();
        return Ok(result);
    }

    // GET: api/order
    [HttpGet]
    public async Task<IActionResult> GetOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {

        var paginationRequest = new PaginationRequest(pageNumber, pageSize);
        var result = await _mediator.Send(new GetOrdersQuery(paginationRequest));
        return Ok(result);
    }

    // GET: api/order/by-customer/{customerId}
    [HttpGet("by-customer/{customerId:guid}")]
    public async Task<IActionResult> GetOrdersByCustomer(Guid customerId)
    {
        var result = await _mediator.Send(new GetOrdersByCustomerQuery(customerId));
        return Ok(result);
    }

    // GET: api/order/by-name?name=abc
    [HttpGet("by-name")]
    public async Task<IActionResult> GetOrdersByName([FromQuery] string name)
    {
        var result = await _mediator.Send(new GetOrdersByNameQuery(name));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto order)
    {
        var result = await _mediator.Send(new CreateOrderCommand(order));
        return CreatedAtAction(nameof(GetOrderById), new { id = result.Id }, result);
    }

    // PUT: api/order/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderDto order)
    {
        if (id != order.Id)
            return BadRequest("ID mismatch");

        var result = await _mediator.Send(new UpdateOrderCommand(order));
        if (!result.IsSuccess)
            return NotFound("Update failed");
        return Ok(result);
    }

    // DELETE: api/order/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var result = await _mediator.Send(new DeleteOrderCommand(id));
        if (!result.IsSuccess)
            return NotFound("Delete failed");
        return Ok(result);
    }
}