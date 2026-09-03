using ECommerceApi.Application.DTOs;
using ECommerceApi.Application.DTOs.pagination_and_filtering;
using ECommerceApi.Application.DTOs.ProductDto;
using ECommerceApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedResultDto<ProductDto>>> GetAll([FromQuery] ProductQueryParameters queryParams) // Changed return type to PagedResultDto<ProductDto>
        {
            var result = await _productService.GetPagedAsync(queryParams);
            return Ok(result);
        }

        /// <summary>
        /// Gets a product by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found." });
            }
            return Ok(product);
        }

        /// <summary>
        /// Gets all products by category ID
        /// </summary>
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetByCategory(int categoryId)
        {
            var products = await _productService.GetByCategoryAsync(categoryId);
            return Ok(products);
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            if (!ModelState.IsValid) // Check if the model state is valid
            {
                return BadRequest(ModelState);
            }

            var existingProduct = await _productService.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found." });
            }

            await _productService.UpdateAsync(id, dto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a product
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingProduct = await _productService.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found." });
            }

            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
