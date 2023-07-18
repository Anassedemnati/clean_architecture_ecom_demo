using Basket.Domain.Common;


namespace Basket.Domain.Entities;

public class ShoppingCart: EntityBase
{
    public string? UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    public decimal TotalPrice
    {
        get
        {
            return Items.Sum(item => item.Price * item.Quantity);
        }
    }
    public ShoppingCart(string? userName)
    {
        UserName = userName;
    }
}
