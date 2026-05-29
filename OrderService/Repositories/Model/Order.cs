using System;


namespace OrderService.Repositories.Model
{
    public class Order
    {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal TotalPrice { get; set; }
            public DateTime CreatedDate { get; set; }


    }
}

