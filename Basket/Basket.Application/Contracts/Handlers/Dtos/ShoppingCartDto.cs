namespace Basket.Application.Contracts.Handlers.Dtos;

public class ShoppingCartDto
{
    public string? UserName { get; set; }
    public List<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();
    public decimal TotalPrice
    {
        get
        {
            return Items.Sum(item => item.Price * item.Quantity);
        }
    }
    public ShoppingCartDto(string? userName)
    {
        UserName = userName;
    }
}
