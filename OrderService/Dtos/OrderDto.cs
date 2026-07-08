using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.MicrosoftExtensions;



namespace OrderService.Dtos
{
     public class OrderProductDto
    {
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    } 
    public class OrderItemDto
    {
        [Key] public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity > 0")]
        public required int Quantity { get; set; }
    }
  
        public class OrderResponseDto
        {
            public Guid Id { get; set; }
            public int Quantity { get; set; }
            public decimal TotalPrice { get; set; }
            public DateTime CreatedAt { get; set; }
            public List<OrderItemDetailsDto> Items { get; set; } = new List<OrderItemDetailsDto>();
        }
    public class OrderItemDetailsDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

}

