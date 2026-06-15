using Microsoft.EntityFrameworkCore;
using NotificationService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;

namespace NotificationService.Repositories;

    public class NotificationDb : DbContext
    {
    public NotificationDb(DbContextOptions<NotificationDb> options) : base(options) { }
    public DbSet<Notification> Notifications { get; set; }
    }

    public class Notification
    {
        ///public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    } 
