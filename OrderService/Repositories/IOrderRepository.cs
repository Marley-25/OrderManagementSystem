using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderService.Repositories;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task CreateOrderAsync(Order order);
        Task<Order?> GetOrderByIdAsync(Guid id);
        Task SaveChangesAsync();
    }
}

