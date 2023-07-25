using Basket.Application.Contracts.ExtApiService;
using Basket.Application.Contracts.Persistence;
using Basket.Infrastructure.ExtApiServices;
using Basket.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetSection("CacheSettings").GetSection("ConnectionString").Value;
        });
        services.AddHttpClient<IOrderService, OrderService>(c =>
        {
            c.BaseAddress = new Uri(configuration["ApiSettings:OrderingUrl"]!);
            
        });
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        services.AddScoped<IBasketRepository, BasketRepository>();
        return services;
    }

}
