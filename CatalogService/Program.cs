using System;
using CatalogService;
using CatalogService.Dtos;
using CatalogService.Repositories;
using CatalogService.Repositories.Impl;
using CatalogService.Services;
using CatalogService.Services.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Validation;
using Microsoft.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductDb>(options => options.UseInMemoryDatabase("ProductsDb"));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.WebHost.UseUrls("http://localhost:5171");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddHttpClient("CatalogClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5171");
});


//builder.Services.AddValidation(); 

var  app = builder.Build();

app.UseDeveloperExceptionPage();  //here the code stop 

app.MapGet("/", () => "API run");

app.UseAuthorization();
app.MapControllers();
app.Run();


