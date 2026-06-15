using System.Threading.Tasks;
using NotificationService.Dtos;

namespace NotificationService.Repositories
{
    public interface INotificationRepository
    {
        Task AddAsync(NotificationDto entity);
        Task CreateNotificationAsync(NotificationDto notification);
        Task SaveChangesAsync();
    }
}