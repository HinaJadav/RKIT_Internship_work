using Microsoft.AspNetCore.Mvc;
using ControllerInitializationDemo.Models;
using ControllerInitializationDemo.BL;

namespace ControllerInitializationDemo.Controllers
{
    /// <summary>
    /// API controller for managing users.
    /// Provides endpoints for CRUD operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly BLIUser _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service for handling business logic.</param>
        public UserController(BLIUser userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of users.</returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The user if found; otherwise, a 404 Not Found response.</returns>
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user object to create.</param>
        /// <returns>The created user with a 201 Created response.</returns>
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest();
            var createdUser = _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="user">The updated user object.</param>
        /// <returns>The updated user if found; otherwise, a 404 Not Found response.</returns>
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

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>A 204 No Content response if successful; otherwise, a 404 Not Found response.</returns>
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
