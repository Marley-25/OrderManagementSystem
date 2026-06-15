using System.Threading.Tasks;
using NotificationService.Dtos;
using NotificationService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace NotificationService.Repositories.Impl
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationDb _context;

        public NotificationRepository(NotificationDb context)
        {
            _context = context;
        }

        public async Task CreateNotificationAsync(NotificationDto notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Notification Order {notification.OrderId}: {notification.Message}");
        }
        
    }
}
