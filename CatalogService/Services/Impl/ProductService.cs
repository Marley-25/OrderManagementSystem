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
           await _productRepository.CreateAsync(dto);
           return dto;
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
            //new 
            if (dto.AvailableQuantity < 0)
            {
                throw new ArgumentException("Available quantity cannot be negative");
            }
            var existing = await _productRepository.GetByIdAsync(id);
            if (existing == null)
            {
                return null;
            }

            existing.Name = dto.Name;
            existing.Price = dto.Price;
            existing.AvailableQuantity = dto.AvailableQuantity;

            await _productRepository.UpdateAsync(existing);
            await _productRepository.SaveChangesAsync();

            return new ProductDto
            {
                Id = existing.Id,
                Name = existing.Name,
                Price = existing.Price,
                AvailableQuantity = existing.AvailableQuantity
            };
            //to do check if quantity is not negative
            //var result = await _productRepository.UpdateAsync(id, dto);
            //return result;
        }
       public async Task<bool> ReduceStockAsync(Guid productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            if (product.AvailableQuantity < quantity)
            {
                return false;
            }

            product.AvailableQuantity -= quantity;
            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();
            return true;
        }
    }
}
