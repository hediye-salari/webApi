using hediyeCrudApi.DTO;
using hediyeCrudApi.Interface;
using Microsoft.AspNetCore.Mvc;

namespace hediyeCrudApi.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class ProductsController : ControllerBase
        {
            private readonly IProductRepository _repository;

            public ProductsController(IProductRepository repository)
            {
                _repository = repository;
            }

            // GET: api/products
            [HttpGet]
            public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
            {
                var products = await _repository.GetAllProductsAsync();
                return Ok(products);
            }

            // GET: api/products/{id}
            [HttpGet("{id}")]
            public async Task<ActionResult<ProductDto>> GetProductById(int id)
            {
                var product = await _repository.GetProductByIdAsync(id);
                if (product == null) return NotFound();

                return Ok(product);
            }

            // POST: api/products
            [HttpPost]
            public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto productDto)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var createdProduct = await _repository.CreateProductAsync(productDto);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
            }

            // PUT: api/products/{id}
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
            {
                if (id != productDto.Id) return BadRequest();

                await _repository.UpdateProductAsync(productDto);
                return NoContent();
            }

            // DELETE: api/products/{id}
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteProduct(int id)
            {
                await _repository.DeleteProductAsync(id);
                return NoContent();
            }
        }
    }

