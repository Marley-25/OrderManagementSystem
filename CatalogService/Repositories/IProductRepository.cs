using CatalogService.Dtos;

namespace CatalogService.Repositories
{
    public interface IProductRepository
    {
        // I change Product with ProductDto because i have problem with implicit conversion between them
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> CreateAsync(ProductDto product);
        Task<ProductDto?> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<ProductDto?> UpdateAsync(ProductDto product);
        Task SaveChangesAsync();
        void SaveChanges(); //isd useful??
        Task<bool> ReduceStockAsync(Guid productId, int quantity);
    }

}
