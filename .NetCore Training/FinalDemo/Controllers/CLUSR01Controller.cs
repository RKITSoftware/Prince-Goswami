using System.Collections.Generic;
using FinalDemo.BL.Interface.Service;
using FinalDemo.Models.POCO;
using Microsoft.AspNetCore.Mvc;
using FinalDemo.Models;
using FinalDemo.Models.DTO;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller for managing users.
    /// </summary>
    [Route("api/Users")]
    [ApiController]
    public class CLUSR01Controller : ControllerBase
    {
        #region Private Fields
        private readonly IBLUSR01 _userService;
        private Response _objResponse;
        #endregion
     
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CLUSR01Controller"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public CLUSR01Controller(IBLUSR01 userService)
        {
            _userService = userService;
        }

        #endregion
        #region Public methods
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A response containing all users.</returns>
        [HttpGet("All")]
        public IActionResult AllUsers()
        {
            _objResponse = _userService.GetAllUsers();
            if (_objResponse == null || _objResponse.Data == null)
            {
                return NotFound("No user transactions found.");
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>A response containing the user if found; otherwise, NotFound result.</returns>
        [HttpGet("{id}")]
        public IActionResult UserById(int id)
        {
            _objResponse = _userService.GetUserById(id);
            if (_objResponse == null || _objResponse.Data == null)
            {
                return NotFound("User transaction not found.");
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="objDTOUSR01">The user data transfer object.</param>
        /// <returns>A response indicating the result of the add operation.</returns>
        [HttpPost("Add")]
        public IActionResult AddUser(DTOUSR01 objDTOUSR01)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ModelState);
            }

            _userService.PreSave(objDTOUSR01);
            _userService.Type = enmOperation.A;
            _objResponse = _userService.ValidationOnSave();
            if (_objResponse.IsError)
            {
                return Ok(_objResponse);
            }

            _objResponse = _userService.Save();
            if (_objResponse.IsError)
            {
                return Ok(_objResponse);
            }

            return Ok(_objResponse);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="objDTOUSR01">The user data transfer object.</param>
        /// <returns>A response indicating the result of the update operation.</returns>
        [HttpPut("Update/{id}")]
        public IActionResult UpdateUser(DTOUSR01 objDTOUSR01)
        {
            _userService.Type = enmOperation.E;
            _userService.PreSave(objDTOUSR01);
            Response validationResponse = _userService.ValidationOnSave();
            if (validationResponse.IsError)
            {
                return Ok(validationResponse.Message);
            }

            var saveResponse = _userService.Save();
            if (saveResponse.IsError)
            {
                return Ok(saveResponse.Message);
            }

            return StatusCode(StatusCodes.Status200OK, saveResponse.Message);
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A response indicating the result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _objResponse = _userService.ValidationOnDelete(id);

            _objResponse = _userService.RemoveUser(id);
            if (_objResponse.IsError)
            {
                return Ok(_objResponse);
            }

            return Ok(_objResponse);
        } 
        #endregion
    }
}
