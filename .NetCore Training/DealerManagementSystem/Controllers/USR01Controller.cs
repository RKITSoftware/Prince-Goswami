using System.Collections.Generic;
using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.Models.POCO;
using Microsoft.AspNetCore.Mvc;

namespace DealerManagementSystem.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class USR01Controller : ControllerBase
    {
        private readonly IBLUSR01 _userService;

        public USR01Controller(IBLUSR01 userService)
        {
            _userService = userService;
        }

        [Route("All")]
        [HttpGet]
        public List<USR01> AllUsers()
        {
            return _userService.GetAllUsers();
        }

        // GET api/usr01/5
        [Route("{id}")]
        [HttpGet]
        public IActionResult UserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/usr01
        [Route("Add")]
        [HttpPost]
        public IActionResult AddUser(USR01 user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _userService.AddUser(user);
            return StatusCode(StatusCodes.Status200OK);
        }

        // PUT api/usr01/5
        [HttpPut("Update")]
        public IActionResult UpdateUser(int id, USR01 user)
        {
            if (!ModelState.IsValid || id != user.R01F01)
            {
                return BadRequest();
            }
            _userService.UpdateUser(user);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("id")]
        public IActionResult DeleteUser(int id)
        {
            var existingUser = _userService.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            _userService.RemoveUser(id);
            return Ok(existingUser);
        }
    }
}
