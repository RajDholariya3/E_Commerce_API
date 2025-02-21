using E_Commerce_API.Data;
using E_Commerce_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailsRepository _repository;

        public OrderDetailsController(OrderDetailsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllOrderDetails()
        {
            var orderDetails = _repository.GetAllOrderDetails();
            return Ok(orderDetails);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderDetailById(int id)
        {
            var orderDetail = _repository.SelectOrderDetailById(id);
            if (orderDetail == null)
                return NotFound();

            return Ok(orderDetail);
        }

        [HttpPost]
        public IActionResult InsertOrderDetail([FromBody] OrderDetailsModel orderDetail)
        {
            if (orderDetail == null)
                return BadRequest();

            bool result = _repository.InsertOrderDetail(orderDetail);
            if (!result)
                return StatusCode(500, "An error occurred while inserting the order detail.");

            return CreatedAtAction(nameof(GetOrderDetailById), new { id = orderDetail.OrderDetailId }, orderDetail);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrderDetail(int id, [FromBody] OrderDetailsModel orderDetail)
        {
            if (orderDetail == null || id != orderDetail.OrderDetailId)
                return BadRequest();

            bool result = _repository.UpdateOrderDetail(orderDetail);
            if (!result)
                return StatusCode(500, "An error occurred while updating the order detail.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetail(int id)
        {
            bool result = _repository.DeleteOrderDetail(id);
            if (!result)
                return StatusCode(500, "An error occurred while deleting the order detail.");

            return NoContent();
        }

        [HttpGet("Drop-Down")]
        public IActionResult GetOrderDetailsDropdown()
        {
            var orderDetails = _repository.GetOrderDetailsDropdown();
            if (orderDetails == null || orderDetails.Count == 0)
            {
                return NotFound();
            }
            return Ok(orderDetails);
        }
    }
}
