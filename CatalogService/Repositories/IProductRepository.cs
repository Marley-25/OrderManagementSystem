using CatalogService.Dtos;

namespace CatalogService.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> CreateAsync(ProductDto product);
        Task<ProductDto?> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<ProductDto?> UpdateAsync(ProductDto product);
        Task SaveChangesAsync();
       
    }

}
