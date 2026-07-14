using System;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:7026");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
 
builder.Services.AddValidation();
var app = builder.Build();
app.MapGet("/", () => "API run");

app.UseAuthorization();
app.MapControllers();
app.Run();

