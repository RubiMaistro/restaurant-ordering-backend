namespace Restaurant.Application.DTOs
{
    public record OrderItemDto(
        Guid DishId,
        int Quantity,
        decimal UnitPrice
    );
}
