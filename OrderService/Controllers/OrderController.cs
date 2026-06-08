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

        [HttpGet("orders")]
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
        [HttpPost("orders")] 
        public async Task<ActionResult<OrderDto>> CreateOrderAsync([FromBody] OrderDto dto)
        
        {
            if (!ModelState.IsValid) //attriubuti di validazione con ModelValid 
                return BadRequest(ModelState);
            var createdOrder = await _orderService.CreateOrderAsync(dto);
            return createdOrder;
           
        }
}
}
