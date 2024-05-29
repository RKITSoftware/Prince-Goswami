using System;
using ATM_Simulation_Demo.Others.Auth.User;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.DAL;
using System.Web.Http.Cors;
using ATM_Simulation_Demo.BAL.Services;
using ATM_Simulation_Demo.Models.POCO;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.DTO;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Net;

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
        private readonly static IBLUserRepository _userRepository = new UserRepository();
        private readonly IBLUserService _userService = new UserService(_userRepository);
        private Response _objResponse;
        #endregion

        #region Actions
        /// <summary>
        /// Authenticates a user based on provided credentials and generates a JWT token if the credentials are valid.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>
        /// HttpResponseMessage:
        ///     - HttpStatusCode.OK with a JWT token if authentication is successful.
        ///     - HttpStatusCode.Unauthorized with an error message if authentication fails.
        ///     - HttpStatusCode.InternalServerError if an unexpected error occurs during authentication.
        /// </returns>
        [HttpPost]
        [Route("Authenticate")]
        [AllowAnonymous]
        public HttpResponseMessage Authenticate(string userName, string password)
        {
            try
            {
                var user = _userService.GetUserByCredentials(userName, password);
                // Check if the provided credentials are valid
                if (user != null)
                {
                    // Generate a JWT token using the authentication service
                    string token = UserTokenManager.GenerateUserToken(user.R01F01, user.R01F02, user.R01F05);

                    // Return an OK _objResponse with the JWT token
                    return Request.CreateErrorResponse(HttpStatusCode.OK, token);
                }
                else
                {
                    // Return an Unauthorized _objResponse with an error message
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid credentials");
                }
            }
            catch (Exception ex)
            {
                // Log or handle any exceptions that might occur during token generation or validation
                // Log.Error($"An error occurred during authentication: {ex}");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred");
            }
        }

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
        public IHttpActionResult CreateUser(DTO_USR01 request)
        {
            try
            {
                _objResponse = _userService.PreValidation(request);

                if (!_objResponse.IsError)
                {
                    _userService.PreSave(request);

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
                int id = UserTokenManager.sessionId;
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
        public IHttpActionResult UpdateRole(int userId, UserRole userRole)
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
