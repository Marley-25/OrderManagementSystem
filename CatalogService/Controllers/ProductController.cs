using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogService.Repositories;
using CatalogService.Services;
using CatalogService.Dtos;
using Microsoft.Extensions.Validation;
using CatalogService.Services.Impl;


namespace CatalogService.Controllers
{
    [ApiController]
    [Route("api/catalog")] 
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("products")] ///GET /api/products
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _productService.GetProductsAsync(); //
            return Ok(products);
        }

        [HttpGet("products/{id}")] //GET /api/products/{id}
        public async Task<ActionResult<ProductDto?>> GetById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            return Ok(product);
        }

        [HttpPost("products")] //POST /api/products
        public async Task<ActionResult<ProductDto>> Create([FromBody] ProductDto dto)

        {
            if (!ModelState.IsValid) //attriubuti di validazione con ModelValid 
                return BadRequest(ModelState);
            var createdProduct = await _productService.CreateProductAsync(dto);
            return createdProduct;

        }

        [HttpPut("products/{id}")] //PUT /api/products/{id}
        public async Task<ActionResult<ProductDto>> UpdateProductAsync(Guid id, [FromBody] ProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

;
            var updated = await _productService.UpdateProductAsync(id, dto);
            if (updated == null)
            {
                return NotFound(new { message = "Product not found" });
            }
            return Ok(updated);
        }


        [HttpDelete("products/{id}")] //DELETE /api/products/{id}
        public async Task<ActionResult<bool>> DeleteProductAsync(Guid id)

        {
            bool isDeleted = await _productService.DeleteProductAsync(id);

            ///var deleted = await _productService.DeleteProductAsync(Id);
            if (!isDeleted)
            {
                return NotFound(new { message = $"Product not found" });
            }
            return Ok(new { message = "Product deleted successfully" });

        }

        [HttpPut("products/{id}/stock")] //PUT /api/catalog/update-stock/{id}
        public async Task<IActionResult> UpdateStock(Guid id, [FromBody] UpdateStockDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (dto.Quantity <= 0)
            {
                return BadRequest(new { message = "Quantity must be greater than 0" });
            }

            var result = await _productService.ReduceStockAsync(id, dto.Quantity);
            if (!result)
            {
                return BadRequest(new { message = "Not enough stock available" });
            }
            return Ok(new { message = "Stock updated successfully" });
       
        }
    }
}

