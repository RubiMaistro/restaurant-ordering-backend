namespace Restaurant.Api.IntergrationTests.DTOs
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public List<OrderItemResponse> Items { get; set; } = new();
    }
}
