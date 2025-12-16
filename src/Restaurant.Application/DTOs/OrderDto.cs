using Restaurant.Domain.Enums;

namespace Restaurant.Application.DTOs
{
    public record OrderDto(
        Guid Id,
        OrderStatus Status,
        decimal TotalAmount,
        DateTime CreatedAt
    );
}
