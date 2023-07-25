using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQueryHandler: IRequestHandler<GetOrdersListQuery, List<OrdersDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<OrdersDto>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.OrderId) && request.UserName == null && request.EmailAddress == null)
        {
            return await GetOrdersByOrderId(request.OrderId);
        }
        else if (string.IsNullOrEmpty(request.OrderId) && request.UserName != null && request.EmailAddress == null)
        {
            return await GetOrdersByUserName(request.UserName);
        }
        else if (string.IsNullOrEmpty(request.OrderId) && request.UserName == null && request.EmailAddress != null)
        {
            return await GetOrdersByEmailAddress(request.EmailAddress);
        }
        else
        {
            return await GetOrdersByUserName(request.UserName!);
        }
    }
    private async Task<List<OrdersDto>> GetOrdersByOrderId(string orderId)
    {
        int orderIdInt = int.Parse(orderId);
        var orderList = await _orderRepository.GetByIdAsync(orderIdInt);
        var ordersDto = _mapper.Map<OrdersDto>(orderList);
        return new List<OrdersDto>() { ordersDto };
    }

    private async Task<List<OrdersDto>> GetOrdersByUserName(string userName)
    {
        var orderList = await _orderRepository.GetOrdersByUserName(userName);
        return _mapper.Map<List<OrdersDto>>(orderList);
    }

    private async Task<List<OrdersDto>> GetOrdersByEmailAddress(string emailAddress)
    {
        var orderList = await _orderRepository.GetOrdersByEmailAddress(emailAddress);
        return _mapper.Map<List<OrdersDto>>(orderList);
    }

}
