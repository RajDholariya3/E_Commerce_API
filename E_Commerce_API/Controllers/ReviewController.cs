using E_Commerce_API.Data;
using E_Commerce_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ReviewsController : ControllerBase
    {
        private readonly ReviewRepository _repository;

        public ReviewsController(ReviewRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllReviews()
        {
            var reviews = _repository.GetAllReviews();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public IActionResult GetReviewById(int id)
        {
            var review = _repository.GetReviewById(id);
            if (review == null)
                return NotFound();

            return Ok(review);
        }

        [HttpPost]
        public IActionResult InsertReview([FromBody] ReviewModel review)
        {
            if (review == null)
                return BadRequest();

            bool result = _repository.InsertReview(review);
            if (!result)
                return StatusCode(500, "An error occurred while inserting the review.");

            return CreatedAtAction(nameof(GetReviewById), new { id = review.ReviewId }, review);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReview(int id, [FromBody] ReviewModel review)
        {
            if (review == null || id != review.ReviewId)
                return BadRequest();

            bool result = _repository.UpdateReview(review);
            if (!result)
                return StatusCode(500, "An error occurred while updating the review.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id)
        {
            bool result = _repository.DeleteReview(id);
            if (!result)
                return StatusCode(500, "An error occurred while deleting the review.");

            return NoContent();
        }
        [HttpGet("Drop-Down")]
        public IActionResult GetReviewDropdown()
        {
            var reviews = _repository.GetReviewDropdown();
            if (reviews == null || reviews.Count == 0)
            {
                return NotFound();
            }
            return Ok(reviews);
        }
    }
}
