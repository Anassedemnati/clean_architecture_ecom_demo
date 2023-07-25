using Basket.Application.Contracts.ExtApiService;
using Basket.Application.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace Basket.Infrastructure.ExtApiServices;

public class OrderService : IOrderService
{
    private readonly HttpClient _client;
    private readonly ILogger<OrderService> _logger;
    public OrderService(HttpClient client, ILogger<OrderService> logger)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<int> Checkout(OrderCheckoutModel orderCheckoutModel)
    {
        _logger.LogInformation("OrderService - Checkout");
        var response = await _client.PostAsJsonAsync("CheckoutOrder", orderCheckoutModel);
        return await response.Content.ReadAsAsync<int>();
    }
}
