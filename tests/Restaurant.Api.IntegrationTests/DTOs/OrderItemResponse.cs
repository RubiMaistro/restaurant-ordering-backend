namespace Restaurant.Api.IntergrationTests.DTOs
{
    public class OrderItemResponse
    {
        public Guid DishId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
