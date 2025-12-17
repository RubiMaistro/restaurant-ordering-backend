using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.DTOs;
using Restaurant.Application.Interfaces.Services;

namespace Restaurant.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateOrderDto dto,
            CancellationToken ct)
        {
            var orderId = await _orderService.CreateAsync(dto, ct);

            return CreatedAtAction(
                nameof(_orderService.GetByIdAsync),
                new { id = orderId },
                new { OrderId = orderId });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken ct)
        {
            var order = await _orderService.GetByIdAsync(id, ct);

            return order is null
                ? NotFound()
                : Ok(order);
        }
    }
}
