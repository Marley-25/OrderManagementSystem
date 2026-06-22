using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.MicrosoftExtensions;



namespace OrderService.Dtos
{
     public class OrderProductDto
    {
        List<OrderItemDto> OrderItems { get; set; } = new();
    }
    public class OrderItemDto
    {
        public required Guid ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity > 0")]
        public required int Quantity { get; set; }
    }
  
        public class OrderResponseDto
        {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal TotalPrice { get; set; }
            public DateTime CreatedAt { get; set; }
        }

}

