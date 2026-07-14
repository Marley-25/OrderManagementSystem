using CatalogService;
using CatalogService.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Http;
using NotificationService.Dtos;
using NotificationService.Controllers;
using OrderService.Dtos;
using OrderService.Repositories;
using OrderService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            return orders
                .OrderByDescending(o => o.CreatedAt)
                .Select(o => new OrderResponseDto
                {
                    Id = o.Id,
                    TotalPrice = o.TotalPrice,
                    CreatedAt = o.CreatedAt,

                    Items = o.OrderItems != null
                ? o.OrderItems.Select(item => new OrderItemDetailsDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList()
                : new List<OrderItemDetailsDto>()
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
                TotalPrice = order.TotalPrice,
                CreatedAt = order.CreatedAt,
                Items = order.OrderItems != null
                ? order.OrderItems.Select(item => new OrderItemDetailsDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList() : new List<OrderItemDetailsDto>()
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
                CreatedAt = DateTime.UtcNow,
                OrderItems = new List<OrderItemDto>()
            };


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
                {
                    int availableStock = 0;
                    try
                    {
                        var errorData = await stockResponse.Content.ReadFromJsonAsync<Dictionary<string, System.Text.Json.JsonElement>>();
                        if (errorData != null && errorData.TryGetValue("availableStock", out var element))
                        {

                            if (element.ValueKind == System.Text.Json.JsonValueKind.Number)
                            {
                                availableStock = element.GetInt32();
                            }
                            else if (element.ValueKind == System.Text.Json.JsonValueKind.String && int.TryParse(element.GetString(), out var parsedQty))
                            {
                                availableStock = parsedQty;
                            }
                        }
                    }
                    catch { }

                    throw new InvalidOperationException($"Stock insufficient for product {item.ProductId}, only {availableStock}");
                }


                var itemTotal = product.Price * item.Quantity;

                var orderItem = new OrderItemDto
                {

                    ProductId = item.ProductId,
                    Quantity = item.Quantity,

                };

                newOrder.OrderItems.Add(orderItem);
                newOrder.TotalPrice += itemTotal;
            }

            await _orderRepository.CreateOrderAsync(newOrder);
            await _orderRepository.SaveChangesAsync();

            await _notificationClient.PostAsJsonAsync($"/api/notifications",
                new NotificationDto
                {
                    OrderId = newOrder.Id,
                    Message = $"Order created. Total price: {newOrder.TotalPrice} and created at: {newOrder.CreatedAt}"

                });


            return new List<OrderResponseDto>
            {
                new OrderResponseDto
                {
                    Id = newOrder.Id,
                    TotalPrice = newOrder.TotalPrice,
                    CreatedAt = newOrder.CreatedAt,
                    Items = newOrder.OrderItems.Select(item => new OrderItemDetailsDto
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    }).ToList()
                }


            };
        }
    }
}
   