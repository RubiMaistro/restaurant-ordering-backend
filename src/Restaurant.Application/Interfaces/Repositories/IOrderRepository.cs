using Restaurant.Application.DTOs;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order, CancellationToken cancellationToken);
        Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
