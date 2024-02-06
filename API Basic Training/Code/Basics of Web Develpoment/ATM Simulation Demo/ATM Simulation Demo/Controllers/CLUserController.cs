using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using ATM_Simulation_Demo.BAL;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.DAL.User;
using System.Web.Http.Cors;

namespace ATM_Simulation_Demo.Controllers
{
    /// <summary>
    /// API controller for managing user-related operations.
    /// </summary>


    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        #region fields
        private readonly static IBLUserRepository _userRepo = new UserRepository();
        private readonly IBLUserService _userService = new UserService(_userRepo);
        #endregion

        #region Actions
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
                    string token = TokenAuthenticationService.GenerateToken(user.UserName, user.Role);

                    // Return an OK response with the JWT token
                    return Request.CreateErrorResponse(HttpStatusCode.OK, token);
                }
                else
                {
                    // Return an Unauthorized response with an error message
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
        [CustomAuthenticationFilter]
        [CustomAuthorizationFilter(Roles = "Admin,DEO,User")]
        [HttpGet]
        [Route("{userId}")]
        public IHttpActionResult GetUser(int userId)
        {
            try
            {
                var user = _userService.GetUserByID(userId);
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
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
        [CustomAuthenticationFilter]
        [CustomAuthorizationFilter(Roles = "Admin,DEO")]
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateUser(CreateUserRequest request)
        {
            try
            {
                var newUser = _userService.CreateUser(request.UserName, request.MobileNumber, request.Password,request.DOB, request.Role);
                return Ok(newUser);
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
        /// <param name="request">The request containing userId, currentPassword, and newPassword.</param>
        /// <returns>Action result indicating the result of the operation.</returns>
        [CustomAuthenticationFilter]
        [CustomAuthorizationFilter(Roles = "Admin,DEO,User")]
        [HttpPatch]
        [Route("changePassword")]
        public IHttpActionResult ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                // Assuming ChangePasswordRequest is a model containing userId, currentPassword, and newPassword properties
                var user = _userService.GetUserByID(request.userId);
                _userService.ChangePassword(user, request.currentPassword, request.newPassword);
                return Ok("Password changed successfully.");
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
        [CustomAuthenticationFilter]
        [CustomAuthorizationFilter(Roles = "Admin")]
        [HttpPatch]
        [Route("updateRole")]
        public IHttpActionResult UpdateRole(UpdateRoleRequest request)
        {
            try
            {
                // Assuming UpdateRoleRequest is a model containing userId and newRole properties
                var user = _userService.GetUserByID(request.userId);
                _userService.UpdateRole(user, request.newRole);
                return Ok("Role updated successfully.");
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
        [CustomAuthenticationFilter]
        [CustomAuthorizationFilter(Roles = "Admin,DEO")]
        [HttpGet]
        [Route("allUsers")]
        public IHttpActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return InternalServerError(ex);
            }
        }

        [CustomAuthenticationFilter]
        [CustomAuthorizationFilter(Roles = "Admin,DEO")]
        [HttpDelete]
        [Route("DeleteUser/{userID}")]
        public void DeleteUser(int userId)
        {
            _userService.DeleteUser(userId);
        }

        
        #endregion
    }

    #region Request Models
    public class ChangePasswordRequest
    {
        public int userId { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
    }

    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public DateTime DOB{ get; set; }
        public UserRole Role{ get; set; }
    }

    public class UpdateRoleRequest
    {
        public int userId { get; set; }
        public UserRole newRole { get; set; }
    }
    #endregion
}
