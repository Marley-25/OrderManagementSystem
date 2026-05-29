namespace OrderService.Dtos
{
    public class ProductDto
    {
        public Guid? Id { get; set; } 

        public required string Name { get; set; } 

        public required decimal Price { get; set; }
        public required int AvailableQuantity { get; set; }
    }
}
