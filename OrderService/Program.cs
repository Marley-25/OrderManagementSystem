using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Repositories;
using OrderService.Repositories.Impl;
using OrderService.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderDb>(options => options.UseInMemoryDatabase("OrderDb"));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService.Services.Impl.OrderService>();
builder.WebHost.UseUrls("http://localhost:5059");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();
app.MapGet("/", () => "API run");

app.UseAuthorization();
app.MapControllers();

app.Run();


