using Basket.Application.Contracts.Handlers.Dtos;
using Basket.Application.Models;
using Basket.Domain.Entities;


namespace Basket.Application.Contracts.Handlers;

public interface IBasketHandler
{
    Task<ShoppingCartDto?> GetBasketAsync(string? userName);
    Task<ShoppingCartDto?> UpdateBasketAsync(ShoppingCartDto basket);
    Task DeleteBasketAsync(string? userName);
    Task<ShoppingCartDto?> AddBasketAsync(ShoppingCartDto basket);
    Task<int> Checkout(BasketCheckout basketCheckout);
}
