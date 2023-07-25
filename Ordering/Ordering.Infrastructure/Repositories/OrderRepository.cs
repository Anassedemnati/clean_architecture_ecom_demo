using Dapper;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;


namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderContext _orderContext;

    public OrderRepository(OrderContext orderContext)
    {
        _orderContext = orderContext;
    }

    public async Task<Order> AddAsync(Order entity)
    {
        var parameters = new
        {
            entity.UserName,
            entity.TotalPrice,
            entity.FirstName,
            entity.LastName,
            entity.EmailAddress,
            entity.AddressLine,
            entity.Country,
            entity.State,
            entity.ZipCode,
            entity.PaymentMethod
        };
        using var connection = _orderContext.CreateConnection();
        return await connection.QuerySingleAsync<Order>("ps_add_order", parameters, commandType: System.Data.CommandType.StoredProcedure);

    }

    public async Task DeleteAsync(Order entity)
    {
        using var connection = _orderContext.CreateConnection();
        var query = "DELETE FROM Orders WITH(NOLOCK) WHERE Id = @OrderId";
        await connection.ExecuteAsync(query, new { entity.Id });
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        using var connection = _orderContext.CreateConnection();

        var query = "SELECT * FROM Orders WITH(NOLOCK) WHERE Id = @OrderId";
        return await connection.QuerySingleOrDefaultAsync<Order>(query, new { OrderId = id });
    }

    public async Task<IEnumerable<Order>> GetOrdersByEmailAddress(string emailAddress)
    {
        using var connection = _orderContext.CreateConnection();

        var query = "SELECT * FROM Orders WITH(NOLOCK) WHERE EmailAddress = @Email";
        return await connection.QueryAsync<Order>(query, new { Email = emailAddress });
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        using var connection = _orderContext.CreateConnection();

        var query = "SELECT * FROM Orders WITH(NOLOCK) WHERE UserName = @UserName";
        return await connection.QueryAsync<Order>(query, new { UserName = userName });
    }

    public async Task UpdateAsync(Order entity)
    {
        using var connection = _orderContext.CreateConnection();
        var parameters = new
        {
            entity.Id,
            entity.UserName,
            entity.FirstName,
            entity.LastName,
            entity.EmailAddress,
            entity.AddressLine,
            entity.Country,
            entity.TotalPrice,
            entity.State,
            entity.ZipCode,
            entity.PaymentMethod
        };
        await connection.ExecuteAsync("ps_update_order", parameters, commandType: System.Data.CommandType.StoredProcedure);
    }
}
