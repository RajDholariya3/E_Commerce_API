using E_Commerce_API.Data;
using E_Commerce_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class OrdersController : ControllerBase
    {
        private readonly OrderRepository _repository;

        public OrdersController(OrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _repository.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _repository.GetOrderById(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public IActionResult InsertOrder([FromBody] OrderModel order)
        {
            if (order == null)
                return BadRequest();

            bool result = _repository.InsertOrder(order);
            if (!result)
                return StatusCode(500, "An error occurred while inserting the order.");

            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderModel order)
        {
            if (order == null || id != order.OrderId)
                return BadRequest();

            bool result = _repository.UpdateOrder(order);
            if (!result)
                return StatusCode(500, "An error occurred while updating the order.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            bool result = _repository.DeleteOrder(id);
            if (!result)
                return StatusCode(500, "An error occurred while deleting the order.");

            return NoContent();
        }

        [HttpGet("Drop-Down")]
        public IActionResult GetOrderDropdown()
        {
            var orders = _repository.GetOrderDropdowns();
            if (orders == null || orders.Count == 0)
            {
                return NotFound();
            }
            return Ok(orders);
        }
    }
}
