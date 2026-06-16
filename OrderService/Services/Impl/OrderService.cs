using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OrderService.Dtos;
using OrderService.Repositories;
using OrderService.Services;
using Microsoft.Extensions.Http;
using CatalogService;
using System.Reflection.Metadata.Ecma335;



namespace OrderService.Services.Impl
{
    public class OrderServiceImpl : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly HttpClient _catalogClient;
        private readonly HttpClient _notificationClient;

        public OrderServiceImpl(IOrderRepository orderRepository, IHttpClientFactory httpClientFactory) 
        {
            _orderRepository = orderRepository;
            _catalogClient = httpClientFactory.CreateClient("CatalogClient");   
            _notificationClient = httpClientFactory.CreateClient("NotificationService");
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrdersAsync()  

        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            return orders.Select(o => new OrderResponseDto

            {
                Id = o.Id,  
                ProductId = o.ProductId,
                TotalPrice = o.TotalPrice,
                Quantity = o.Quantity,
                CreatedAt = o.CreatedAt
            }).ToList();
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return null;
            }
            return new OrderResponseDto

            {
                Id = order.Id,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                TotalPrice = order.TotalPrice,
                CreatedAt = order.CreatedAt
            };
        }

        public async Task<IEnumerable<OrderResponseDto>> CreateOrderAsync(IEnumerable<OrderProductDto> dtos) 
        {
            if (dtos == null)
            {
                throw new ArgumentException("Order cannot have 0 products");
            }
            var results = new List<OrderResponseDto>();
            foreach (var dto in dtos)
            {
           
                if (dto.Quantity <= 0)
                {
                    throw new ArgumentException("Order cannot have 0 products");
                }

                var productResponse = await _catalogClient.GetAsync($"/api/products/{dto.ProductId}");
                if (!productResponse.IsSuccessStatusCode)
                {
                    throw new KeyNotFoundException("Product not found");
                }

                var product = await productResponse.Content.ReadFromJsonAsync<CatalogProductDto>();
                if (product == null)
                {
                    throw new InvalidOperationException("Without success");
                }

                

                var stockUpdate = new { quantity = dto.Quantity };
                var stockResponse = await _catalogClient.PutAsJsonAsync($"/api/products/{dto.ProductId}/stock", stockUpdate);

                if (!stockResponse.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException("Insufficient stock");
                }

                var TotalPrice = product.Price * dto.Quantity;

                var newOrder = new Order
                {
                    Id = Guid.NewGuid(),
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    TotalPrice = TotalPrice,
                    CreatedAt = DateTime.UtcNow
                };

                await _orderRepository.CreateOrderAsync(newOrder);
                await _orderRepository.SaveChangesAsync();

                var notificationOrder = new
                {
                    orderId = newOrder.Id,
                    productId = newOrder.ProductId,
                    message = $"Order processed for {product.Name}",
                    createdAt = DateTime.UtcNow
                };

                _ = _notificationClient.PostAsJsonAsync("/api/notifications", notificationOrder);
            }
            await _orderRepository.SaveChangesAsync();
            return results;
        }
    }
}