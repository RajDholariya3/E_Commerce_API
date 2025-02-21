using E_Commerce_API.Data;
using E_Commerce_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository customerRepository;

        public CustomerController(CustomerRepository _customerRepository)
        {
            customerRepository = _customerRepository;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = customerRepository.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = customerRepository.SelectCustomerById(id);
            if (customer == null)
            {
                return NotFound(new { Message = $"Customer with ID {id} not found." });
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] CustomerModel customer)
        {
            if (customer == null)
                return BadRequest(new { Message = "Invalid customer data." });

            bool isAdded = customerRepository.InsertCustomer(customer);

            if (isAdded)
                return Ok(new { Message = "Customer added successfully!" });

            return StatusCode(500, new { Message = "An error occurred while adding the customer." });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerModel customer)
        {
            if (customer == null || id != customer.CustomerId)
                return BadRequest(new { Message = "Invalid data or mismatched customer ID." });

            bool isUpdated = customerRepository.UpdateCustomer(customer);

            if (!isUpdated)
                return NotFound(new { Message = $"Customer with ID {id} not found." });

            return Ok(new { Message = "Customer updated successfully!" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            bool isDeleted = customerRepository.DeleteCustomer(id);

            if (!isDeleted)
                return NotFound(new { Message = $"Customer with ID {id} not found or already deleted." });

            return Ok(new { Message = $"Customer with ID {id} has been deleted." });
        }
        [HttpGet("Drop-Down")]
        public IActionResult GetCustomerDropdown()
        {
            var customers = customerRepository.GetCustomerDropdowns();
            if (customers == null || customers.Count == 0)
            {
                return NotFound();
            }
            return Ok(customers);
        }
    }
}
