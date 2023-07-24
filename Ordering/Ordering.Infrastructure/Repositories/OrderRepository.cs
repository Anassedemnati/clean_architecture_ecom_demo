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
        var query = "exec ps_add_order @UserName, @FirstName, @LastName, @Email, @AddressLine, @Country, @TotalPrice, @PaymentMethod";
        using var connection = _orderContext.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@UserName", entity.UserName);
        parameters.Add("@FirstName", entity.FirstName);
        parameters.Add("@LastName", entity.LastName);
        parameters.Add("@Email", entity.EmailAddress);
        parameters.Add("@AddressLine", entity.AddressLine);
        parameters.Add("@Country", entity.Country);
        parameters.Add("@TotalPrice", entity.TotalPrice);
        parameters.Add("@PaymentMethod", entity.PaymentMethod);
        return await connection.QuerySingleOrDefaultAsync<Order>(query, parameters, commandType: System.Data.CommandType.Text);
    }

    public async Task DeleteAsync(Order entity)
    {
        using var connection = _orderContext.CreateConnection();
        var query = "DELETE FROM Orders WHERE OrderId = @OrderId";
        await connection.ExecuteAsync(query, new { entity.Id });
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        using var connection = _orderContext.CreateConnection();

        var query = "SELECT * FROM Orders WHERE OrderId = @OrderId";
        return await connection.QuerySingleOrDefaultAsync<Order>(query, new { OrderId = id });
    }

    public async Task<IEnumerable<Order>> GetOrdersByEmailAddress(string emailAddress)
    {
        using var connection = _orderContext.CreateConnection();

        var query = "SELECT * FROM Orders WHERE Email = @Email";
        return await connection.QueryAsync<Order>(query, new { Email = emailAddress });
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        using var connection = _orderContext.CreateConnection();

        var query = "SELECT * FROM Orders WHERE UserName = @UserName";
        return await connection.QueryAsync<Order>(query, new { UserName = userName });
    }

    public async Task UpdateAsync(Order entity)
    {
        using var connection = _orderContext.CreateConnection();

        var query = "exec ps_update_order @OrderId, @UserName, @FirstName, @LastName, @Email, @AddressLine, @Country, @TotalPrice, @PaymentMethod";
        var parameters = new DynamicParameters();
        parameters.Add("@OrderId", entity.Id);
        parameters.Add("@UserName", entity.UserName);
        parameters.Add("@FirstName", entity.FirstName);
        parameters.Add("@LastName", entity.LastName);
        parameters.Add("@Email", entity.EmailAddress);
        parameters.Add("@AddressLine", entity.AddressLine);
        parameters.Add("@Country", entity.Country);
        parameters.Add("@TotalPrice", entity.TotalPrice);
        parameters.Add("@PaymentMethod", entity.PaymentMethod);
        await connection.ExecuteAsync(query, parameters, commandType: System.Data.CommandType.Text);
    }
}
