using OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseDto>> GetOrdersAsync();
        Task<OrderResponseDto?> GetOrderByIdAsync(Guid id);
        Task<OrderResponseDto> CreateOrderAsync(OrderProductDto dto);
    }
}
