using CatalogService.Dtos;
using Microsoft.AspNetCore.Mvc;


// problem with Product entity and ProductDto, i decide to use Product entity for all layers to avoid error on convert type between ProductRep-Service
//here i have problem with Product and not with ProductDto 

namespace CatalogService.Services
{
    public interface IProductService
    {
       
            Task<IEnumerable<ProductDto>> GetProductsAsync();
            Task<ProductDto?> GetProductByIdAsync(Guid id);
            Task<ProductDto> CreateProductAsync(ProductDto dto);
            Task<bool> DeleteProductAsync(Guid id);

            Task<ProductDto?> UpdateProductAsync(Guid id, ProductDto dto);

            Task<bool> ReduceStockAsync(Guid productId, int quantity);
           

    }
}
