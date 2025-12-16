using Restaurant.Application.DTOs;
using Restaurant.Application.Interfaces.Repositories;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Domain.Entities;
using Restaurant.Domain.ValueObjects;

namespace Restaurant.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository repository)
        {
            _orderRepository = repository;
        }

        public async Task<Guid> CreateAsync(CreateOrderDto createOrderDto, CancellationToken cancellationToken)
        {
            var order = new Order(Guid.NewGuid());

            foreach(var item in createOrderDto.Items)
            {
                var money = Money.Create(item.UnitPrice);
                var orderItem = new OrderItem(item.DishId, item.Quantity, money);

                order.AddItem(orderItem);   
            }

            await _orderRepository.AddAsync(order, cancellationToken);
            await _orderRepository.SaveChangesAsync(cancellationToken); 

            return order.Id;
        }

        public async Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
            if (order is null)
                return null;

            return new OrderDto(
                order.Id,
                order.Status,
                order.GetTotalAmount().Amount,
                order.CreatedAt
            );
        }
    }
}
