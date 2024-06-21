using System.Collections.Generic;
using FinalDemo.BL.Interface.Service;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using Microsoft.AspNetCore.Mvc;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller for managing User operations via API endpoints.
    /// </summary>
    [Route("api/User")]
    [ApiController]
    public class BLUSR01Controller : ControllerBase
    {
        #region Private Fields

        private readonly IBLUSR01 _userService;
        private Response _objResponse;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for initializing BLUSR01Controller with user service dependency.
        /// </summary>
        /// <param name="userService">Instance of IBLUSR01 service interface.</param>
        public BLUSR01Controller(IBLUSR01 userService)
        {
            _userService = userService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>ActionResult with a list of all users.</returns>
        [Route("All")]
        [HttpGet]
        public IActionResult AllUsers()
        {
            _objResponse = _userService.GetAllUsers();
            return Ok(_objResponse);
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>ActionResult with the user details.</returns>
        [Route("{id}")]
        [HttpGet]
        public IActionResult UserById(int id)
        {
            _objResponse = _userService.GetUserById(id);
            return Ok(_objResponse);
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="objDTOUSR01">DTO containing user details to add.</param>
        /// <returns>ActionResult with success or error message.</returns>
        [Route("Add")]
        [HttpPost]
        public IActionResult AddUser(DTOUSR01 objDTOUSR01)
        {
            _userService.Type = enmOperation.A;
            _userService.PreSave(objDTOUSR01);
            _objResponse = _userService.ValidationOnSave();

            if (!_objResponse.IsError)
            {
                _objResponse = _userService.Save();
                return Ok(_objResponse);
            }

            return BadRequest(_objResponse);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="objDTOUSR01">DTO containing updated user details.</param>
        /// <returns>ActionResult with success or error message.</returns>
        [HttpPut("Update")]
        public IActionResult UpdateUser(DTOUSR01 objDTOUSR01)
        {
            _userService.Type = enmOperation.E;
            _userService.PreSave(objDTOUSR01);
            _objResponse = _userService.ValidationOnSave();

            if (!_objResponse.IsError)
            {
                _objResponse = _userService.Save();
                return Ok(_objResponse);
            }

            return BadRequest(_objResponse);
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>ActionResult with success or error message.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _objResponse = _userService.ValidationOnDelete(id);

            if (!_objResponse.IsError)
            {
                _objResponse = _userService.RemoveUser(id);
                return Ok(_objResponse);
            }

            return BadRequest(_objResponse);
        }

       
        #endregion
    }
}
