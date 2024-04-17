//using Microsoft.AspNetCore.Authentication;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JWT_Token.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {

        [HttpGet]
        [Route("Authenticate")]
        [AllowAnonymous]
        public HttpResponseMessage Authenticate(string userName, string password)
        {
            try
            {
                // Check if the provided credentials are valid
                if (TokenAuthenticationService.ValidateCredentials(userName, password))
                {
                    // Generate a JWT token using the authentication service
                    string token = TokenAuthenticationService.GenerateToken(userName);

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

        [CustomAuthenticationFilter]
        [CustomAuthorizationFilter(Roles = "Admin")]
        [Route("AdminOnly")]
        [HttpGet]
        public HttpResponseMessage AdminOnlyAction()
        {
            // Your action logic for admin-only access
            return Request.CreateResponse(HttpStatusCode.OK, "Admin-only action");
        }

        /// <summary>
        /// Validates the provided username and password and returns a JWT token if valid.
        /// </summary>
        /// <param name="userName">The username to validate.</param>
        /// <param name="password">The password associated with the username.</param>
        /// <returns>
        /// HttpResponseMessage with OK status and a JWT token if the provided credentials are valid,
        /// or HttpResponseMessage with Unauthorized status and an error message if the credentials are invalid.
        /// </returns>
        [HttpGet]
        public HttpResponseMessage isUser(string userName, string password)
        {
            try
            {
                // Check if the provided credentials are valid (dummy check for demonstration purposes)
                if (TokenAuthenticationService.ValidateCredentials(userName,password))
                {
                    // Generate a JWT token using the TokenManager
                    string token = TokenManager.GenerateToken(userName);

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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred");
            }
        }


        [CustomAuthenticationFilter]
        [Route("GetUser")]
        [HttpGet]
        public HttpResponseMessage GetUsers()
        {
            // Your action logic for authenticated users
            return Request.CreateResponse(HttpStatusCode.OK, "Successfully valid");
        }
    }
}
