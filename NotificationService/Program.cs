using System;
using NotificationService;
using NotificationService.Dtos;
using NotificationService.Repositories;
using NotificationService.Repositories.Impl;
using NotificationService.Services;
using NotificationService.Services.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Validation;
using Microsoft.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationServiceImpl>();

builder.WebHost.UseUrls("http://localhost:5059");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddValidation();
var app = builder.Build();
app.MapGet("/", () => "API run");

app.UseAuthorization();
app.MapControllers();
app.Run();

