using Basket.Application.Models;

namespace Basket.Application.Contracts.ExtApiService;

public interface IOrderService
{
    Task<int> Checkout(OrderCheckoutModel orderCheckoutModel);

}
