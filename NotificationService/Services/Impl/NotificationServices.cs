using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotificationService.Dtos;
using NotificationService.Repositories;
using NotificationService.Repositories.Impl;
using NotificationService.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


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
                OrderId = Guid.NewGuid(),
                //Email = notificationDto.Email,
                Message = notificationDto.Message,
                CreatedAt = DateTime.UtcNow
            };
            await _notificationRepository.CreateNotificationAsync(notification);

            return notification;
        }

        public async Task SendNotificationAsync(NotificationDto dto)
        {
            var body = $"Hi {dto.Message}";   //text content of the email 
            await _notificationRepository.SendAsync(dto.Message, "Notification", body);
        }
    }
}
                