using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks; // allow multiple parts of code to run concurrently,
using CatalogService.Repositories;
using CatalogService.Services;
using CatalogService.Dtos;
using Microsoft.Extensions.Validation;
using CatalogService.Services.Impl;


///Guid_id problem + async method problem ù

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("api/catalog")] //url di base BRuno 
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService; //rig 20-25 dependency injection

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
        ///public async Task<IActionResult> GetProducts(Guid_id)
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

        //update per il prodotto 
        [HttpPut("products/{id}")] //PUT /api/products/{id} only available quantity 
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


        ////i need a api for Update stock
        [HttpPut("products/{id}/stock")] //PUT /api/catalog/update-stock/{id}
        public async Task<IActionResult>UpdateStock(Guid id, [FromBody] UpdateStockDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (dto.Quantity <= 0)
            {
                return BadRequest(new { message = "Quantity must be greater than 0" });
            }

            try
            {
                var result = await _productService.ReduceStockAsync(id, dto.Quantity);

                if (!result)
                {
                    return BadRequest(new { message = "Not enough stock available" });
                }
                return Ok(new { message = "Stock updated successfully" });
            }

            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Product not found" });
            }
        }
    }
}

