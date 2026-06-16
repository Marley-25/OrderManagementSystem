using NotificationService.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationService.Services
{
    public interface INotificationService
    {
        Task<NotificationDto> CreateNotificationAsync(NotificationDto dto);

    }
}
