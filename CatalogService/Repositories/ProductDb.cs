using CatalogService.Dtos;
using Microsoft.EntityFrameworkCore;
using System;


namespace CatalogService.Repositories;
public class ProductDb : DbContext

{
    public ProductDb(DbContextOptions<ProductDb> options) : base(options) { }
    public DbSet<ProductDto> Products { get; set; }

}
