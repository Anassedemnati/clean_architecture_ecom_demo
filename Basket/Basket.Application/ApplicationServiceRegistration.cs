using Basket.Application.Contracts.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation.AspNetCore;
using FluentValidation;
using Basket.Application.Contracts.Handlers.Dtos;

namespace Basket.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)//extention method
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IBasketHandler, BasketHandler>();

        return services;
    }

}
