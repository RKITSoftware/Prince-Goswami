using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Routing.Models;

namespace Routing.Controllers
{
    /// <summary>
    /// Controller for managing users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly List<User> _users;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        public UserController()
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
        /// Gets all users.
        /// </summary>
        /// <returns>json result of The list of all users.</returns>
        [HttpGet]
        public JsonResult GetAllUsers()
        {
            return new JsonResult(_users);
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _users.Find(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Create USer
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="password">password</param>
        /// <returns>Empty Result</returns>
        [HttpPost("Create")]
        public EmptyResult CreateUser(string name, string password)
        {
            User usr= new User()
            {
                Id = 10,
                Username = name,
                Password = password,
                Email = name+"@xyz.com"
            };
            _users.Add(usr);
            return new EmptyResult();
        }

    }
}
