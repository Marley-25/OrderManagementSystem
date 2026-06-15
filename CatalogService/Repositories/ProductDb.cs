using CatalogService.Dtos;
using Microsoft.EntityFrameworkCore;
using System;


namespace CatalogService.Repositories;
///ProductDbContext class is useful as it represents your database context for managing Product entities with Entity Framework Core.
public class ProductDb : DbContext

{
    public ProductDb(DbContextOptions<ProductDb> options) : base(options) { }
    public DbSet<ProductDto> Products { get; set; }

}
