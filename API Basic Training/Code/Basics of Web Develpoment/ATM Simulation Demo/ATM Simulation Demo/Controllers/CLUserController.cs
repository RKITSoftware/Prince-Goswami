using ATM_Simulation_Demo.BAL;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.DAL.User;
using ATM_Simulation_Demo.Others;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ATM_Simulation_Demo.Controllers
{
    /// <summary>
    /// API controller for managing user-related operations.
    /// </summary>


    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        #region fields
        /// <summary>
        /// Represents the user repository used for interacting with user data.
        /// </summary>
        private static readonly IBLUserRepository _userRepo = new UserRepository();

        /// <summary>
        /// Represents the user service used for handling user-related operations.
        /// </summary>
        private readonly IBLUserService _userService = new UserService(_userRepo);

        /// <summary>
        /// Represents the response object for managing responses.
        /// </summary>
        public Response objResponse;
        #endregion

        #region public methods

        /// <summary>
        /// Authenticates a user using the provided username and password.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>
        /// An HTTP response message indicating the result of the authentication:
        ///   - If the provided credentials are valid, returns an OK response with a JWT token.
        ///   - If the provided credentials are invalid, returns an Unauthorized response with an error message.
        ///   - If an error occurs during authentication, returns an InternalServerError response.
        /// </returns>
        [HttpPost]
        [Route("Authenticate")]
        [AllowAnonymous]
        public HttpResponseMessage Authenticate(string userName, string password)
        {
            try
            {
                UserModel user = _userService.GetUserByCredentials(userName, password);
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
            catch (Exception)
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
                objResponse = _userService.GetUserByID(userId);
                return Ok(objResponse);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Newly created user information.</returns>
        [CustomAuthenticationFilter]
        [CustomAuthorizationFilter(Roles = "Admin,DEO")]
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateUser(CreateUserRequest request)
        {
            try
            {
                objResponse = _userService.CreateUser(request.UserName, request.MobileNumber, request.Password, request.DOB, request.Role);
                return Ok(objResponse);
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
            objResponse = _userService.ChangePassword(request.userId, request.currentPassword, request.newPassword);
            return Ok(objResponse);
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
                objResponse = _userService.UpdateRole(request.userId, request.newRole);
                return Ok(objResponse);
            }
            catch (Exception)
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
                objResponse = _userService.GetAllUsers();
                return Ok(objResponse);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Deletes a user with the specified user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>An IHttpActionResult representing the result of the delete operation.</returns>
        [CustomAuthenticationFilter]
        [CustomAuthorizationFilter(Roles = "user")]
        [HttpDelete]
        [Route("{userID}")]
        public IHttpActionResult DeleteUser(int userId)
        {
            // Call the DeleteUser method of the user service and return the result
            return Ok(_userService.DeleteUser(userId));
        }

        #endregion
    }


}
