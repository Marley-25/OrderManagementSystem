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
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(NotificationDto entity)
        {
            await _context.AddAsync(entity);
        }
    }
}
