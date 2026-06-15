using NotificationService.Repositories;
using NotificationService.Repositories.Impl;
using NotificationService.Services;
using NotificationService.Services.Impl;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NotificationDb>(options => options.UseInMemoryDatabase("NotificationsDb"));

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationServiceImpl>();

builder.WebHost.UseUrls("http://localhost:5060");
builder.Services.AddControllers();
 builder.Services.AddEndpointsApiExplorer();

builder.Services.AddValidation();
var app = builder.Build();
app.MapGet("/", () => "API run");

app.UseAuthorization();
app.MapControllers();
app.Run();

