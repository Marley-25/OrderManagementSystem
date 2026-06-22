using Microsoft.AspNetCore.Mvc;
using OrderService.Dtos;
using OrderService.Services;
using OrderService.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;


namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrdersAsync()
        {
            var orders = await _orderService.GetOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("orders/{id}")] //GET /api/orders/{id}:
        public async Task<ActionResult<OrderResponseDto>> GetOrderByIdAsync(Guid id)
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
        public async Task<ActionResult<OrderResponseDto>> CreateOrderAsync([FromBody] OrderProductDto dtos)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               
                var createdOrders = await _orderService.CreateOrderAsync(dtos);
                return Ok(createdOrders);
            }

            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (InvalidOperationException ex) { return BadRequest(new {message = ex.Message}); }
        }
    }
}
