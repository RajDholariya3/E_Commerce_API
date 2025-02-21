using E_Commerce_API.Data;
using E_Commerce_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PaymentsController : ControllerBase
    {
        private readonly PaymentRepository _repository;

        public PaymentsController(PaymentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllPayments()
        {
            var payments = _repository.GetAllPayments();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public IActionResult GetPaymentById(int id)
        {
            var payment = _repository.GetPaymentById(id);
            if (payment == null)
                return NotFound();

            return Ok(payment);
        }

        [HttpPost]
        public IActionResult InsertPayment([FromBody] PaymentsModel payment)
        {
            if (payment == null)
                return BadRequest();

            bool result = _repository.InsertPayment(payment);
            if (!result)
                return StatusCode(500, "An error occurred while inserting the payment.");

            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.PaymentId }, payment);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePayment(int id, [FromBody] PaymentsModel payment)
        {
            if (payment == null || id != payment.PaymentId)
                return BadRequest();

            bool result = _repository.UpdatePayment(payment);
            if (!result)
                return StatusCode(500, "An error occurred while updating the payment.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            bool result = _repository.DeletePayment(id);
            if (!result)
                return StatusCode(500, "An error occurred while deleting the payment.");

            return NoContent();
        }

        [HttpGet("Drop-Down")]
        public IActionResult GetPaymentDropdown()
        {
            var payments = _repository.GetPaymentDropdown();
            if (payments == null || payments.Count == 0)
            {
                return NotFound();
            }
            return Ok(payments);
        }
    }
}
