using System;
using System.Collections.Generic;



namespace OrderService.Dtos
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderProductDto> Products { get; set; } = new List<OrderProductDto>();

    }

    public class OrderProductDto
    {
        public Guid ProductId { get; set; }
        public required string ProductName { get; set; }
        public required decimal Price { get; set;  }
        public required int Quantity { get; set;  }
    }
}

