using CatalogService.Dtos;
using Microsoft.EntityFrameworkCore;
using System;


namespace CatalogService.Repositories;
///ProductDbContext class is useful as it represents your database context for managing Product entities with Entity Framework Core.
public class ProductDb : DbContext

{
    public DbSet<ProductDto> Products { get; set; }
    public ProductDb(DbContextOptions<ProductDb> options) : base(options)
    {
    }

    public class Product
    {
        public Guid? Id { get; set; } = Guid.NewGuid(); //autogenerato 

        public required string Name { get; set; } = string.Empty;

        public required decimal Price { get; set; }
        public required int AvailableQuantity { get; set; }

    }
}
