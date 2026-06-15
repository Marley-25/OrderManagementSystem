using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotificationService.Dtos;
using NotificationService.Repositories;
using NotificationService.Services;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task CreateNotificationAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Notification entity)
        {
            await _context.AddAsync(entity);
        }
        
        public async Task SendAsync(Notification);
        {
            await _context.SendAsync(Notification);
        }
    }
}
