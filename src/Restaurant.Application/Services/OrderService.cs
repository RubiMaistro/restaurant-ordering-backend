using FluentValidation;
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
        private readonly IValidator<CreateOrderDto> _validator;

        public OrderService(IOrderRepository repository, IValidator<CreateOrderDto> validator)
        {
            _orderRepository = repository;
            _validator = validator;
        }

        public async Task<Guid> CreateAsync(CreateOrderDto createOrderDto, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(createOrderDto, cancellationToken);
            if(!validationResult.IsValid)
            {
                throw new InvalidOperationException("Invalid order data: " +
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

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
