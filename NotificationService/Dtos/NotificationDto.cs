using Microsoft.AspNetCore.SignalR;

namespace NotificationService.Dtos
{
    public class NotificationDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set;  }
        public Guid OrderId { get; set;  }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Message { get; set; } = string.Empty;
    }

}
