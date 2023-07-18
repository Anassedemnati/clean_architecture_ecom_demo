using Basket.Application.Contracts.Persistence;
using Basket.Domain.Common;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repositories;

public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
{
    protected readonly IDistributedCache _redisCache;
    public RepositoryBase(IDistributedCache redisCache)
    {
        _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
    }
    public async Task<T> GetByIdAsync(int id)
    {
        var basket = await _redisCache.GetStringAsync(id.ToString());
        return string.IsNullOrEmpty(basket) ? throw new Exception("Basket not found.") : JsonConvert.DeserializeObject<T>(basket)!;
    }
}
