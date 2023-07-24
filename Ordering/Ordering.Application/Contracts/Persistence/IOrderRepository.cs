using Ordering.Domain.Entities;
using System.Linq.Expressions;

namespace Ordering.Application.Contracts.Persistence;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    Task<IEnumerable<Order>> GetOrdersByEmailAddress(string emailAddress);
    Task<Order> GetByIdAsync(int id);
    Task<Order> AddAsync(Order entity);
    Task UpdateAsync(Order entity);
    Task DeleteAsync(Order entity);


}
