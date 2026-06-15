using CatalogService.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using CatalogService.Repositories;
using System.Runtime.Serialization;

namespace CatalogService.Repositories.Impl
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDb _context;

        public ProductRepository(ProductDb context)
        {
            _context = context;
        }

        public async Task<ProductDto> CreateAsync(ProductDto dto)
        {
            var result = await _context.Products.AddAsync(dto);
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

        public async Task<ProductDto?> UpdateAsync(ProductDto product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> ReduceStockAsync(Guid productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return false;
            }
            if (product.AvailableQuantity < quantity)
            {
                return false;
            }

            product.AvailableQuantity -= quantity;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveChanges() //sync method useful for some cases but generally async is preferred for database operations to avoid blocking threads!!!
        {
            _context.SaveChanges();
        }
    }
}
