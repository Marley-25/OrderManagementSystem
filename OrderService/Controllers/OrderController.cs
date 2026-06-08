using Microsoft.AspNetCore.Mvc;
using OrderService.Dtos;
using OrderService.Services;
using OrderService.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/orders[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)        
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersAsync()
        {
            var orders = await _orderService.GetOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("orders/{id}")] //GET /api/orders/{id}:
        public async Task<ActionResult<OrderDto>> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound(new { message = "Order not found" });
            }
            return Ok(order);
        }


        //POST /api/orders:
        [HttpPost] 
        public async Task<OrderDto> CreateOrderAsync([FromBody] Order dto)
        {
            if (dto == null || dto.Products == null || !dto.Products.Any())

            {
                return BadRequest(new{"Order must have 1 product"});
            }

            var newOrder = await _orderService.CreateOrderAsync(dto);

            return CreatedAtAction(nameof(GetOrdersAsync), new { id = newOrder.Id }, newOrder);
        }

    }
}
