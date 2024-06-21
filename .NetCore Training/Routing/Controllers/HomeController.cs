using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Routing.Models;

namespace Routing.Controllers
{
    /// <summary>
    /// Controller for managing users.
    /// </summary>
    [ApiController]
    [Route("api/Home")]
    public class HomeController : ControllerBase
    {
        private readonly List<User> _users;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        public HomeController()
        {
            // Initialize a list of users (this could come from a database in a real application)
            _users = new List<User>
            {
                new User { Id = 1, Username = "user1", Password = "password1", Email = "user1@example.com" },
                new User { Id = 2, Username = "user2", Password = "password2", Email = "user2@example.com" },
                // Add more users as needed
            };
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_users);
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        [HttpGet("GetUSerById")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _users.Find(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user object to create.</param>
        /// <returns>The newly created user.</returns>
        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            // Logic to add user to database or other storage
            _users.Add(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updatedUser">The updated user object.</param>
        /// <returns>No content.</returns>
        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var existingUser = _users.Find(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }
            // Update user properties
            existingUser.Username = updatedUser.Username;
            existingUser.Password = updatedUser.Password;
            existingUser.Email = updatedUser.Email;
            // Other properties...
            return NoContent();
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The deleted user.</returns>
        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var userToDelete = _users.Find(u => u.Id == id);
            if (userToDelete == null)
            {
                return NotFound();
            }
            _users.Remove(userToDelete);
            return Ok(userToDelete);
        }
    }
}
