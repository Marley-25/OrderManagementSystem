using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Repositories;
using OrderService.Repositories.Impl;
using OrderService.Services;
using OrderService.Services.Impl;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderDb>(options => options.UseInMemoryDatabase("OrderDb"));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderServiceImpl>();
builder.WebHost.UseUrls("http://localhost:5059");

// i need communication HTTP with appsetting.json
builder.Services.AddHttpClient("CatalogService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrl:CatalogService"]
        ?? "http://localhost:5171");
});

// i need communication HTTP with NotificationService 
builder.Services.AddHttpClient("NotificationService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrl:NotificationService"]
        ?? "http://localhost:5090");
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();
app.MapGet("/", () => "API run");

app.UseAuthorization();
app.MapControllers();

app.Run();


