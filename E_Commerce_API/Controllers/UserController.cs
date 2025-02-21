using E_Commerce_API.Data;
using E_Commerce_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UsersController : ControllerBase
    {
        private readonly UserRepository _repository;

        public UsersController(UserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _repository.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _repository.GetUserById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult InsertUser([FromBody] UserModel user)
        {
            if (user == null)
                return BadRequest();

            bool result = _repository.InsertUser(user);
            if (!result)
                return StatusCode(500, "An error occurred while inserting the user.");

            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel user)
        {
            if (user == null || id != user.UserId)
                return BadRequest();

            bool result = _repository.UpdateUser(user);
            if (!result)
                return StatusCode(500, "An error occurred while updating the user.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            bool result = _repository.DeleteUser(id);
            if (!result)
                return StatusCode(500, "An error occurred while deleting the user.");

            return NoContent();
        }
        [HttpGet("Drop-Down")]
        public IActionResult GetUserDropDown()
        {
            var user = _repository.GetUserDropDown();
            if (user == null || user.Count == 0)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
