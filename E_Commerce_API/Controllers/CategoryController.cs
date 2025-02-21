using E_Commerce_API.Data;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_API.Model;
using Microsoft.AspNetCore.Authorization;

namespace sampleapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesRepository CategoryRepository;

        public CategoriesController(CategoriesRepository _CategoryRepository)
        {
            CategoryRepository = _CategoryRepository;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = CategoryRepository.GetAllCategories();
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult InsertCategory([FromBody] CategoriesModel category)
        {
            if (category == null)
                return BadRequest();

            bool isInserted = CategoryRepository.InsertCategory(category);

            if (isInserted)
                return Ok(new { Message = "Category inserted successfully!" });

            return StatusCode(500, "An error occurred while inserting the category.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoriesModel category)
        {
            if (category == null || id != category.CategoryId)
                return BadRequest();

            var isUpdated = CategoryRepository.UpdateCategory(category);

            if (!isUpdated)
                return NotFound(new { Message = $"Category with ID {id} not found." });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var isDeleted = CategoryRepository.DeleteCategory(id);

                if (!isDeleted)
                {
                    return NotFound(new { Message = $"Category with ID {id} not found or already deleted." });
                }

                return Ok(new { Message = $"Category with ID {id} has been deleted." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting category: {ex.Message}");
                return StatusCode(500, new { Message = $"An error occurred while deleting category with ID {id}." });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = CategoryRepository.SelectCategoryById(id);
            if (category == null)
            {
                return NotFound(new { Message = $"Category with ID {id} not found." });
            }
            return Ok(category);
        }

        [HttpGet("Drop-Down")]
        public IActionResult GetCategoryDropdown()
        {
            var categories = CategoryRepository.GetCategoryDropdown();
            if (categories == null || categories.Count == 0)
            {
                return NotFound();
            }
            return Ok(categories);
        }
    }
}
