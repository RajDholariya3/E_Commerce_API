using E_Commerce_API.Data;
using E_Commerce_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _repository;

        public ProductsController(ProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _repository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _repository.GetProductById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult InsertProduct([FromBody] ProductModel product)
        {
            if (product == null)
                return BadRequest();

            bool result = _repository.InsertProduct(product);
            if (!result)
                return StatusCode(500, "An error occurred while inserting the product.");

            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductModel product)
        {
            if (product == null || id != product.ProductId)
                return BadRequest();

            bool result = _repository.UpdateProduct(product);
            if (!result)
                return StatusCode(500, "An error occurred while updating the product.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            bool result = _repository.DeleteProduct(id);
            if (!result)
                return StatusCode(500, "An error occurred while deleting the product.");

            return NoContent();
        }

        [HttpGet("Drop-Down")]
        public IActionResult GetProductDropdown()
        {
            var products = _repository.GetProductDropdown();
            if (products == null || products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products);
        }
    }
}
