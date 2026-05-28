//using CatalogService;
//using CatalogService.Dtos;
//using CatalogService.Repositories;
//using CatalogService.Repositories.Impl;
//using CatalogService.Services;
//using CatalogService.Services.Impl;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Hosting;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<OrderDb>(options => options.UseInMemoryDatabase("OrderDb"));
//builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//builder.Services.AddScoped<IOrderService, OrderService>();
//builder.WebHost.UseUrls("http://localhost:5059");

//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();


//var app = builder.Build();
//app.MapGet("/", () => "API run");

//app.UseAuthorization();
//app.MapControllers();
//app.Run();


