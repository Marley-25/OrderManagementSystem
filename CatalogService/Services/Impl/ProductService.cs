using CatalogService.Dtos;
using CatalogService.Repositories;
using System.Diagnostics.Eventing.Reader;




namespace CatalogService.Services.Impl
{
    public class ProductService : IProductService //classe base 
    {
        //aggiungi injection IProductRepository 
        private readonly IProductRepository _productRepository; //rig 20-25 dependency injection

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductDto> CreateProductAsync(ProductDto dto)
        {
            return await _productRepository.CreateAsync(dto);
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                { 
                    return false;
                }
            await _productRepository.DeleteAsync(id);
            return true;
         }
        


        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<ProductDto?> UpdateProductAsync(Guid id, ProductDto dto)
        {
            var result = await _productRepository.UpdateAsync(id, dto);
            return result;
        }
       
    }
}
