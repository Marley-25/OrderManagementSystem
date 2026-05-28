using CatalogService.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using CatalogService.Repositories;


namespace CatalogService.Repositories.Impl
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDb _context;

        public ProductRepository(ProductDb context)
        {
            _context = context;
        }

        public async Task<ProductDto> CreateAsync(ProductDto product)
        {
            var result = await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }


        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }
            return product;
            
        }

        public async Task<ProductDto?> UpdateAsync(Guid id, ProductDto dto)
        {
            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct != null)
            {
                existingProduct.AvailableQuantity = dto.AvailableQuantity;
                existingProduct.Price= dto.Price;
                existingProduct.Name = dto.Name;

                var result = _context.Products.Update(existingProduct);

                await _context.SaveChangesAsync();
            }
            return existingProduct;
        }
    }
}
