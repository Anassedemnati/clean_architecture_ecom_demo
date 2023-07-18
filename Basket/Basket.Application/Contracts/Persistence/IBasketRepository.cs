using Basket.Domain.Entities;

namespace Basket.Application.Contracts.Persistence;

public interface IBasketRepository: IAsyncRepository<ShoppingCart>
{
    Task<ShoppingCart?> GetBasket(string? userName);
    Task<ShoppingCart?> UpdateBasket(ShoppingCart basket);
    Task DeleteBasket(string? userName);
    Task<ShoppingCart?> AddBasket(ShoppingCart basket);
}
