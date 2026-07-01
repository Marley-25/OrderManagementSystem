using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderService.Dtos;
using OrderService.Repositories;
using OrderService.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


namespace OrderService.Repositories.Impl
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDb _context;

        public OrderRepository(OrderDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }


        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await _context.Orders.FindAsync(id);

        }
        public async Task CreateOrderAsync(Order order)
        {
           
           await _context.Orders.AddAsync(order); 
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
