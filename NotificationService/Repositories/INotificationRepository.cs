using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotificationService.Repositories;

namespace NotificationService.Repositories
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification entity);
        Task CreateNotificationAsync(Notification notification);
        Task SendAsync(string message, string v, string body);

        Task SaveChangesAsync();
    } 
}