using System;
using System.Threading.Tasks;
using NotificationService.Dtos;
using NotificationService.Repositories;
using NotificationService.Services;

namespace NotificationService.Services.Impl
{
    public class NotificationServiceImpl : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationServiceImpl(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<NotificationDto> CreateNotificationAsync(NotificationDto notificationDto)
        {
            if (notificationDto == null)
            {
                throw new ArgumentNullException(nameof(notificationDto));
            }

            var notification = new NotificationDto
            {
                OrderId = notificationDto.OrderId,
                ProductId = notificationDto.ProductId,
                Message = notificationDto.Message,
                CreatedAt = DateTime.UtcNow
            };

            await _notificationRepository.CreateNotificationAsync(notification);
            await _notificationRepository.SaveChangesAsync();

            return notification;
        }
    }
}
                