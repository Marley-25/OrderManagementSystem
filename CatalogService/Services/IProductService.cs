using CatalogService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Services
{
    public interface IProductService
    {
       
            Task<IEnumerable<ProductDto>> GetProductsAsync();
            Task<ProductDto?> GetProductByIdAsync(Guid id);
            Task<ProductDto> CreateProductAsync(ProductDto dto);
            Task<bool> DeleteProductAsync(Guid id);

            Task<ProductDto?> UpdateProductAsync(Guid id, ProductDto dto);

            
           

    }
}
