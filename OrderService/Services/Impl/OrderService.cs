using CatalogService;
using CatalogService.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Http;
using OrderService.Dtos;
using OrderService.Repositories;
using OrderService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;



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

        public async Task<IEnumerable<OrderResponseDto>> CreateOrderAsync(OrderProductDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("Order must have at least one product");
            }


            var newOrder = new Order
            {
                Id = Guid.NewGuid(),
                TotalPrice = 0,
                CreatedAt = DateTime.UtcNow
            };

            var results = new List<OrderResponseDto>();

            foreach (var item in dto.OrderItems)
            {
                if (item.Quantity <= 0)
                    throw new ArgumentException("Quantity must be greater than 0");

                var response = await _catalogClient.GetAsync($"/api/catalog/products/{item.ProductId}");
                if (!response.IsSuccessStatusCode)
                    throw new KeyNotFoundException($"Product {item.ProductId} not found");

                var product = await response.Content.ReadFromJsonAsync<ProductDto>();
                if (product == null)
                    throw new InvalidOperationException("Product retrieval failed");

                var stockResponse = await _catalogClient.PutAsJsonAsync(
                    $"/api/catalog/products/{item.ProductId}/stock",
                    new { quantity = item.Quantity });

                if (!stockResponse.IsSuccessStatusCode)
                    throw new InvalidOperationException("Stock update failed");

                var itemTotal = product.Price * item.Quantity;

                var orderItem = new OrderItemDto
                {
                    //Id = Guid.NewGuid(),
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    //TotalPrice = product.Price
                };

                newOrder.OrderItems.Add(orderItem);
                newOrder.TotalPrice += itemTotal;
            }

            await _orderRepository.CreateOrderAsync(newOrder);
            await _orderRepository.SaveChangesAsync();

            await _notificationClient.PostAsJsonAsync("/api/notifications", new
            {
                Id = Guid.NewGuid(),
                OrderId = newOrder.Id,
                Message = $"Order processed {newOrder.Id}",
                CreatedAt = DateTime.UtcNow
            });

            return new List<OrderResponseDto>
            {
                new OrderResponseDto
                {
                    Id = newOrder.Id,
                    TotalPrice = newOrder.TotalPrice,
                    CreatedAt = newOrder.CreatedAt
                }
            };
        }
    }
}
   