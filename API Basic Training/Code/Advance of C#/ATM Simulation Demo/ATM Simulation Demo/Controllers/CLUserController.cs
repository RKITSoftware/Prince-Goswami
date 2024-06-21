﻿using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.BAL.Services;
using ATM_Simulation_Demo.DAL;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.DTO;
using ATM_Simulation_Demo.Others.Auth;
using System;
using System.Web.Http;

namespace ATM_Simulation_Demo.Controllers
{
    /// <summary>
    /// API controller for managing user-related operations.
    /// </summary>
    [RoutePrefix("api/users")]
    [CustomAuthenticationFilter]
    public class UserController : ApiController
    {
        #region fields
        /// <summary>
        /// Represents a static instance of an implementation of the IBLUserRepository interface.
        /// </summary>
        private readonly static IBLUserRepository _userRepository = new UserRepository();

        /// <summary>
        /// Represents an instance of a service for user-related operations.
        /// </summary>
        private readonly IBLUserService _userService = new UserService(_userRepository);

        /// <summary>
        /// Represents a response object for handling service responses.
        /// </summary>
        private Response _objResponse;

        #endregion

        #region Actions

        /// <summary>
        /// Get user information based on user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>User information if found, otherwise NotFound.</returns>

        [CustomAuthorizationFilter(Roles = "Admin,User")]
        [HttpGet]
        [Route("{userId}")]
        public IHttpActionResult GetUser(int userId)
        {
            try
            {
                _objResponse = _userService.GetUserByID(userId);
                return Ok(_objResponse);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="userName">User's username.</param>
        /// <param name="password">User's password.</param>
        /// <param name="role">User's role.</param>
        /// <returns>Newly created user information.</returns>

        [CustomAuthorizationFilter(Roles = "Admin")]
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateUser(DTOUSR01 request)
        {
            try
            {
                //// pre validation not rwuired
                _objResponse = _userService.PreValidation(request);

                if (!_objResponse.IsError)
                {
                    _userService.PreSave(request);
                    /// validation
                    _userService.Validation();
                    _objResponse = _userService.Save();
                }
                return Ok(_objResponse);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Change the password for a user.
        /// </summary>
        /// <param name="currentPassword">currentPassword of user.</param>
        /// <param name="newPassword">newPassword to set.</param>
        /// <returns>Action result indicating the result of the operation.</returns>

        [CustomAuthorizationFilter(Roles = "Admin,User")]
        [HttpPatch]
        [Route("changePassword")]
        public IHttpActionResult ChangePassword([FromBody] string currentPassword, [FromBody] string newPassword)
        {
            try
            {
                // Assuming ChangePasswordRequest is a model containing userId, currentPassword, and newPassword properties
                int id = TokenManager.UserSId;
                Response _objResponse = _userService.ChangePassword(id, currentPassword, newPassword);
                return Ok(_objResponse);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return BadRequest("Internal Server Error");
            }
        }

        /// <summary>
        /// Updates the role for a user.
        /// </summary>
        /// <param name="request">The request containing userId and newRole.</param>
        /// <returns>Action result indicating the result of the operation.</returns>  

        [CustomAuthorizationFilter(Roles = "Admin")]
        [HttpPatch]
        [Route("updateRole")]
        public IHttpActionResult UpdateRole(int userId, enmUserRole userRole)
        {
            try
            {
                // Assuming UpdateRoleRequest is a model containing userId and newRole properties
                _objResponse = _userService.UpdateRole(userId, userRole);
                return Ok(_objResponse);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return BadRequest("Internal Server Error");
            }
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>List of all users.</returns>

        [CustomAuthorizationFilter(Roles = "Admin")]
        [AllowAnonymous]
        [HttpGet]
        [Route("allUsers")]
        public IHttpActionResult GetAllUsers()
        {
            try
            {
                _objResponse = _userService.GetAllUsers();
                return Ok(_objResponse);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete user by id.
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>List of all users.</returns>

        [CustomAuthorizationFilter(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteUser/{userID}")]
        public IHttpActionResult DeleteUser(int userId)
        {
            _objResponse = _userService.DeleteUser(userId);
            return Ok(_objResponse);
        }


        #endregion
    }

}
