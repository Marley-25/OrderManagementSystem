using Microsoft.AspNetCore.SignalR;

namespace NotificationService.Dtos
{
    public class NotificationDto
    {
        public Guid OrderId { get; set; }
        public string Message { get; set; } = string.Empty;

    }
}
