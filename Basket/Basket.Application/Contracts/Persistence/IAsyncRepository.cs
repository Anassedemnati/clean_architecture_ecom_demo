using Basket.Domain.Common;
using Basket.Domain.Entities;


namespace Basket.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T : EntityBase
{
    Task<T> GetByIdAsync(int id);
}
