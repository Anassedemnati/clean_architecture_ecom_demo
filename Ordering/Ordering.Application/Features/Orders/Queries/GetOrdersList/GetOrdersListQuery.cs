using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQuery:IRequest<List<OrdersDto>>
{
    public string? UserName { get; set; }
    public string? OrderId { get; set;}
    public string? EmailAddress { get; set; }

    public GetOrdersListQuery(string? userName, string? orderId, string? emailAddress)
    {
        UserName = userName;
        OrderId = orderId;
        EmailAddress = emailAddress;
    }
}
