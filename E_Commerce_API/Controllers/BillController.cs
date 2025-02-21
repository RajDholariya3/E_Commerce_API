using E_Commerce_API.Data;
using E_Commerce_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BillController : ControllerBase
    {
        private readonly BillRepository billRepository;

        public BillController(BillRepository _billRepository)
        {
            billRepository = _billRepository;
        }

        [HttpGet]
        public IActionResult GetAllBills()
        {
            var bills = billRepository.GetAllBills();
            return Ok(bills);
        }

        [HttpGet("{id}")]
        public IActionResult GetBillById(int id)
        {
            var bill = billRepository.SelectBillById(id);
            if (bill == null)
            {
                return NotFound(new { Message = $"Bill with ID {id} not found." });
            }
            return Ok(bill);
        }

        [HttpPost]
        public IActionResult AddBill([FromBody] BillsModel bill)
        {
            if (bill == null)
                return BadRequest(new { Message = "Invalid bill data." });

            bool isAdded = billRepository.InsertBill(bill);

            if (isAdded)
                return Ok(new { Message = "Bill added successfully!" });

            return StatusCode(500, new { Message = "An error occurred while adding the bill." });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBill(int id, [FromBody] BillsModel bill)
        {
            if (bill == null || id != bill.BillId)
                return BadRequest(new { Message = "Invalid data or mismatched bill ID." });

            bool isUpdated = billRepository.UpdateBill(bill);

            if (!isUpdated)
                return NotFound(new { Message = $"Bill with ID {id} not found." });

            return Ok(new { Message = "Bill updated successfully!" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBill(int id)
        {
            bool isDeleted = billRepository.DeleteBill(id);

            if (!isDeleted)
                return NotFound(new { Message = $"Bill with ID {id} not found or already deleted." });

            return Ok(new { Message = $"Bill with ID {id} has been deleted." });
        }
        [HttpGet("Drop-Down")]
        public IActionResult GetBillDropdown()
        {
            var bills = billRepository.GetBillDropdown();
            if (bills == null || bills.Count == 0)
            {
                return NotFound();
            }
            return Ok(bills);
        }
    }
}
