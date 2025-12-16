namespace Restaurant.Application.DTOs
{
    public record CreateOrderDto(
        Guid OrderId,
        IReadOnlyCollection<OrderItemDto> Items
    );
}
