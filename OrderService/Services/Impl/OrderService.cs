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

        public OrderServiceImpl(IOrderRepository orderRepository, IHttpClientFactory httpClientFactory) //ctor with Interface http?
        {
            _orderRepository = orderRepository;
            _catalogClient = httpClientFactory.CreateClient("CatalogClient");   //dependency injection to request at catalog 
            _notificationClient = httpClientFactory.CreateClient("NotificationService");
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrdersAsync()  ///orderDto or orderproduct from ICollection 

        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            return orders.Select(o => new OrderResponseDto

            {
                Id = o.Id,   //remember o not p for Guid 
                ProductId = o.ProductId,
                //ProductName = p.ProductName,
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

        public async Task<IEnumerable<OrderResponseDto>> CreateOrderAsync(IEnumerable<OrderProductDto> dtos)  //i need to improve request and response dto 
        {
            if (dtos == null)
            {
                throw new ArgumentException("Order cannot have 0 products");
            }
            var results = new List<OrderResponseDto>();
            foreach (var dto in dtos)
            {
                ///////validation quantity new part 
                if (dto.Quantity <= 0)
                {
                    throw new ArgumentException("Order cannot have 0 products");
                }

                //call to GET api/products for price 
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

                //call PUT api/product/id/stock
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
                        //return new OrderResponseDto
                        //{
                        //    Id = newOrder.Id,
                        //    ProductId = newOrder.ProductId,
                        //    Quantity = newOrder.Quantity,
                        //    TotalPrice = newOrder.TotalPrice,
                        //    CreatedAt = newOrder.CreatedAt
                        //};
     