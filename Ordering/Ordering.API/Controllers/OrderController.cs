using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;

namespace Ordering.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrderController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrdersDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrdersDto>>> GetOrders(string? userName, string? orderId, string? emailAddress)
    {
        var query = new GetOrdersListQuery(userName, orderId, emailAddress);
        var orders = await _mediator.Send(query);
        return Ok(orders);
    }
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<OrdersDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrdersDto>>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
    {
        var orders = await _mediator.Send(command);
        return Ok(orders);
    }
    [HttpPatch]
    [ProducesResponseType(typeof(IEnumerable<OrdersDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrdersDto>>> UpdateOrder([FromBody] UpdateOrderCommand command)
    {
        var orders = await _mediator.Send(command);
        return Ok(orders);
    }
    [HttpDelete]
    [ProducesResponseType(typeof(IEnumerable<OrdersDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrdersDto>>> DeleteOrder(int id)
    {
        var command = new DeleteOrderCommand(id);
        var orders = await _mediator.Send(command);
        return Ok(orders);
    }

}
