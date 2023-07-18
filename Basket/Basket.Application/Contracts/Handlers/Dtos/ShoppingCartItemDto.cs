namespace Basket.Application.Contracts.Handlers.Dtos;

public class ShoppingCartItemDto
{
    public int Quantity { get; set; }
    public string? Color { get; set; }
    public decimal Price { get; set; }
    public string? ProductId { get; set; }
    public string? ProductName { get; set; }
}