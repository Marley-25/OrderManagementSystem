using CatalogService.Repositories;



namespace CatalogService.Dtos
{
    public class ProductDto
    {
        public Guid? Id { get; set; } = Guid.NewGuid(); //autogenerato 

        public required string Name { get; set; } = string.Empty;

        public required decimal Price { get; set; }
        public required int AvailableQuantity { get; set; }

    }
    
    public class UpdateStockDto   //in order too??'
    {
        public int AvailableQuantity { get; set; }
    }
}
   