using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;

namespace Ordering.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrderController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    //get orer by username or id or email if one of them is valid others will are "-1" 
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrdersDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrdersDto>>> GetOrdersByUsername(string? userName, string? orderId, string? emailAddress)
    {
        var query = new GetOrdersListQuery(userName, orderId, emailAddress);
        var orders = await _mediator.Send(query);
        return Ok(orders);
    }
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<OrdersDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrdersDto>>> GetOrdersByUsername([FromBody] CheckoutOrderCommand query)
    {
        var orders = await _mediator.Send(query);
        return Ok(orders);
    }
    [HttpPatch]
    [ProducesResponseType(typeof(IEnumerable<OrdersDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrdersDto>>> GetOrdersByUsername([FromBody] UpdateOrderCommand query)
    {
        var orders = await _mediator.Send(query);
        return Ok(orders);
    }
    [HttpDelete]
    [ProducesResponseType(typeof(IEnumerable<OrdersDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrdersDto>>> GetOrdersByUsername(int id)
    {
        var query = new DeleteOrderCommand(id);
        var orders = await _mediator.Send(query);
        return Ok(orders);
    }

}
