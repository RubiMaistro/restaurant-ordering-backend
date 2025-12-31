using Microsoft.AspNetCore.Mvc;
using Restaurant.Api.Filters;
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
        [ServiceFilter(typeof(ValidationFilter<CreateOrderDto>))]
        public async Task<IActionResult> Create(
            [FromBody] CreateOrderDto dto,
            CancellationToken ct)
        {
            var orderId = await _orderService.CreateAsync(dto, ct);

            return Ok(orderId);
        }

        [HttpGet("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilter<CreateOrderDto>))]
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
