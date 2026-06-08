using OrderService.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;

namespace OrderService.Repositories;

///ProductDbContext class is useful as it represents your database context for managing Product entities with Entity Framework Core.
    public class OrderDb : DbContext

    {
        public OrderDb(DbContextOptions<OrderDb> options) : base(options) { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }

        public class Order
        {
            public Guid Id { get; set; }
            public DateTime DateOrderCreated { get; set; } = DateTime.UtcNow;
            public decimal TotalPrice { get; set; }

            public virtual ICollection<OrderItem> Products { get; set; } = new List<OrderItem>();
        
            public void UpdateTotalPrice()
            {
                TotalPrice = Products.Sum(p => p.Price * p.Quantity);
            }

        //internal void UpdateTotalPrice() ///i  have to implement this method or not? verified 
        //{
        //    throw new NotImplementedException();
        //} PROBLEM OF ORDER: LIST OF PRODCUCTS OR NOT?
        }

    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public required string ProductName { get; set; }
        public required decimal Price { get; set; }
        public required int Quantity { get; set; }
    }