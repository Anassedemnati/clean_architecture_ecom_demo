using Basket.Application.Contracts.Persistence;
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
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        services.AddScoped<IBasketRepository, BasketRepository>();
        return services;
    }

}
