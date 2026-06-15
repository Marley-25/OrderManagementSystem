using System.Threading.Tasks;
using NotificationService.Dtos;

namespace NotificationService.Repositories
{
    public interface INotificationRepository
    {
        Task CreateNotificationAsync(NotificationDto notification);
    }
}