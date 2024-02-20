using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ATM_Simulation_Demo
{
    /// <summary>
    /// Utility class for managing JSON Web Tokens (JWT).
    /// </summary>
    public class TokenManager
    {
        #region Constants

        private static readonly string SecretKey = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        #endregion

        #region Token Generation

        /// <summary>
        /// Generates a JWT token for the specified username.
        /// </summary>
        /// <param name="userName">The username to include in the JWT token.</param>
        /// <returns>The generated JWT token as a string.</returns>
        public static string GenerateToken(string userName, UserRole role)
        {
            byte[] key = Convert.FromBase64String(SecretKey);

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(key);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userName), new Claim(ClaimTypes.Role, role.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(3), // Consider making this configurable
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = handler.CreateJwtSecurityToken(tokenDescriptor);

            // Set Key ID (kid) in the token header
            jwtSecurityToken.Header.Add("kid", "RKIT");

            return handler.WriteToken(jwtSecurityToken);
        }

        #endregion

        #region Token Validation

        /// <summary>
        /// Validates a JWT token and returns the associated ClaimsPrincipal.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <returns>
        /// The ClaimsPrincipal representing the authenticated user if the token is valid,
        /// or null if the token is invalid or an error occurs during validation.
        /// </returns>
        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

                JwtSecurityToken jwtToken = (JwtSecurityToken)handler.ReadToken(token);

                // Retrieve Key ID (kid) from the token header
                string keyId = jwtToken.Header?.Kid;

                if (jwtToken == null)
                {
                    return null;
                }

                byte[] key = Convert.FromBase64String(SecretKey);

                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                SecurityToken securityToken;
                var principal = handler.ValidateToken(token, parameters, out securityToken);

                return principal;
            }
            catch (SecurityTokenExpiredException)
            {
                // Handle token expiration (returning null in this case)
                Console.WriteLine("Token has expired.");
                return null;
            }
            catch (Exception ex)
            {
                // Handle other token validation exceptions
                Console.WriteLine($"Token validation error: {ex.Message}");
                return null;
            }
        }

        #endregion
    }

    #region AuthServices
    /// <summary>
    /// Contains methods for token Authentication
    /// </summary>
    public static class TokenAuthenticationService
    {
        ///// <summary>
        ///// Validates the provided username and password.
        ///// </summary>
        ///// <param name="userName">The username to validate.</param>
        ///// <param name="password">The password associated with the username.</param>
        ///// <returns>True if the credentials are valid; otherwise, false.</returns>
        //public static bool ValidateCredentials(string userName, string password)
        //{
        //    // Replace this with your actual logic to validate credentials
        //    return userName == "admin" && password == "password";
        //}

        /// <summary>
        /// Generates a JWT token for the specified username.
        /// </summary>
        /// <param name="userName">The username to include in the JWT token.</param>
        /// <returns>The generated JWT token as a string.</returns>
        public static string GenerateToken(string userName, UserRole role)
        {
            // Use TokenManager or your token generation logic here
            return TokenManager.GenerateToken(userName, role);
        }
    }
    #endregion
}
