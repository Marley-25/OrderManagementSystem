using Microsoft.AspNetCore.SignalR;

namespace NotificationService.Dtos
{
    public class NotificationDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set;  }
        public Guid OrderId { get; set;  }
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; }
    }

    ///for notification i need an event for this passage 
   
    public class Event
    {
        public Guid OrderId { get; set; }
        public string Email { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
