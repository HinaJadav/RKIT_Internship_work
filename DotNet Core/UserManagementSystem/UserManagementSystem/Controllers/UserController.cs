using Microsoft.AspNetCore.Mvc;
using ControllerInitializationDemo.Models;
using ControllerInitializationDemo.BL;

namespace ControllerInitializationDemo.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly BLIUser _userService;

        public UserController(BLIUser userService)
        {
            _userService = userService;
        }

        // GET api/user
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST api/user
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest();
            var createdUser = _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        // PUT api/user/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (user == null)
                return BadRequest();
            var updatedUser = _userService.UpdateUser(id, user);
            if (updatedUser == null)
                return NotFound();
            return Ok(updatedUser);
        }

        // DELETE api/user/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var success = _userService.DeleteUser(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
