using Basket.Application.Contracts.Handlers.Dtos;
using Basket.Application.Contracts.Persistence;
using Basket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Contracts.Handlers;

public interface IBasketHandler
{
    Task<ShoppingCartDto?> GetBasketAsync(string? userName);
    Task<ShoppingCartDto?> UpdateBasketAsync(ShoppingCartDto basket);
    Task DeleteBasketAsync(string? userName);
    Task<ShoppingCartDto?> AddBasketAsync(ShoppingCartDto basket);
}
