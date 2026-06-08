using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Dtos;
using OrderService.Repositories;
using OrderService.Services;

namespace OrderService.Services.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync()  ///orderDto or orderproduct from ICollection 

        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            return orders.Select(o => new OrderDto //mapping from incoming DTO to db entity 
            {
                OrderId = o.Id,
                TotalPrice = o.TotalPrice,
                Products = o.Products.Select(p => new OrderProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList()

            }).ToList();
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null
                {
                return null;
            }
            return new OrderDto
            {
                OrderId = order.Id,
                TotalPrice = order.TotalPrice,
                Products = order.Products.Select(p => new OrderProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList()
            };
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto dto)
        {
            var orderId = Guid.NewGuid();
            var newOrder = new Order
            {
                Id = orderId,
                DateOrderCreated = DateTime.UtcNow,
                Products = dto.Products.Select(p => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList()
            };

            newOrder.UpdateTotalPrice();

            await _orderRepository.CreateOrderAsync(newOrder);
            await _orderRepository.SaveChangesAsync();

            dto.OrderId = newOrder.Id;
            dto.TotalPrice = newOrder.TotalPrice;
            return dto;
        }
    }
}
