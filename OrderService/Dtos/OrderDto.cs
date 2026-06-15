using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;  //i need for Range 
using Microsoft.OpenApi.MicrosoftExtensions;



namespace OrderService.Dtos
{

    public class OrderProductDto //useful for POST orders 
    {
        public required Guid ProductId { get; set; }
        /// public required string ProductName { get; set; }
        ///public required decimal Price { get; set; }

        //dpoubts 
        [Required] 
        [Range(1, int.MaxValue, ErrorMessage = "Quantity > 0")]
        public required int Quantity { get; set; }   ///[Range] ever above the properties 
       
    }
        public class OrderResponseDto //response from GET 
        {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal TotalPrice { get; set; }
            public DateTime CreatedAt { get; set; }
        }


        public class CatalogProductDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int AvailableQuantity { get; set; }
        }
 }