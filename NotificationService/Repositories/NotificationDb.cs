using Microsoft.EntityFrameworkCore;
using NotificationService.Dtos;

namespace NotificationService.Repositories;

public class NotificationDb : DbContext
{
    public NotificationDb(DbContextOptions<NotificationDb> options) : base(options) { }
    public DbSet<NotificationDto> Notifications { get; set; }
}
