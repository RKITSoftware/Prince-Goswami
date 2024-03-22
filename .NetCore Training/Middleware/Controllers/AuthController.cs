using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;

namespace Middleware.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("token")]
        public IActionResult GenerateBasicToken(string username, string password)
        {
            // Check if username and password are provided
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Username and password are required.");
            }

            // Concatenate username and password with a colon
            string credentials = $"{username}:{password}";

            // Encode the credentials in base64
            string encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

            // Construct the basic authentication token
            string basicToken = $"Basic {encodedCredentials}";

            return Ok(basicToken);
        }
    }
}
