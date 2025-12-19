using Restaurant.Application.DTOs;

namespace Restaurant.Application.Tests.Orders
{
    public static class CreateOrderDtoMother
    {
        public static CreateOrderDto Valid()
        {
            return new CreateOrderDto(
                OrderId: Guid.NewGuid(),
                Items: new List<OrderItemDto>
                {
                    new OrderItemDto(DishId: Guid.NewGuid(), Quantity: 2, UnitPrice: 15)
                });
        }

        public static CreateOrderDto WithoutItems()
        {
            return new CreateOrderDto(
                OrderId: Guid.NewGuid(),
                Items: new List<OrderItemDto>());
        }
    }
}
