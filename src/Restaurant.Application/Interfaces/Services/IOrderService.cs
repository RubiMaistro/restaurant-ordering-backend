using Restaurant.Application.DTOs;

namespace Restaurant.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Guid> CreateAsync(CreateOrderDto createOrderDto, CancellationToken cancellationToken);
        Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
