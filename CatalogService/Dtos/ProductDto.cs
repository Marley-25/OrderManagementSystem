using CatalogService.Repositories;



namespace CatalogService.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string Name { get; set; } = string.Empty;

        public required decimal Price { get; set; }
        public required int AvailableQuantity { get; set; }

    }
    
    public class UpdateStockDto
    {
        public required int Quantity { get; set; }
    }
}
   