using Basket.Application.Contracts.Persistence;
using Basket.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repositories;

public class BasketRepository : RepositoryBase<ShoppingCart>, IBasketRepository
{
    public BasketRepository(IDistributedCache redisCache) : base(redisCache){}

    public async Task<ShoppingCart?> AddBasket(ShoppingCart basket)
    {
        if (basket is null) return null;
        //keep the basket olde items and add new ones
        var existingBasket = await GetBasket(basket.UserName!);
        if (existingBasket is null) return await UpdateBasket(basket);
        existingBasket.Items.AddRange(basket.Items);
        return await UpdateBasket(existingBasket);
    }

    public async Task DeleteBasket(string? userName)
    {
        await _redisCache.RemoveAsync(userName!);
    }

    public async Task<ShoppingCart?> GetBasket(string? userName)
    {
        var basket = await _redisCache.GetStringAsync(userName!);
        return string.IsNullOrEmpty(basket) ? null : JsonConvert.DeserializeObject<ShoppingCart>(basket)!;
    }

    public async Task<ShoppingCart?> UpdateBasket(ShoppingCart basket)
    {
       if (basket is null) return null;
       await _redisCache.SetStringAsync(basket.UserName!, JsonConvert.SerializeObject(basket));
       return await GetBasket(basket.UserName!);
    }
}
