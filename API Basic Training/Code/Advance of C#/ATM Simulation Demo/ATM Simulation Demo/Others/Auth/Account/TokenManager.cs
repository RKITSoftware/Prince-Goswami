using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace ATM_Simulation_Demo.Others.Auth.Account
{
    /// <summary>
    /// Utility class for managing JSON Web Tokens (JWT).
    /// </summary>
    public class TokenManager
    {
        #region Constants

        private static readonly string SecretKey = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        #endregion

        public static int sessionId;

        #region Token Generation

        /// <summary>
        /// Generates a JWT token for the specified username.
        /// </summary>
        /// <param name="userName">The username to include in the JWT token.</param>
        /// <returns>The generated JWT token as a string.</returns>
        public static string GenerateToken(int accountId)
        {
            byte[] key = Convert.FromBase64String(SecretKey);

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(key);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, accountId.ToString()) }),
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

        #region ValidateUserId
        public static void setSessionId(string token)
        {
            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

                JwtSecurityToken jwtToken = (JwtSecurityToken)handler.ReadToken(token);

                // Retrieve user ID from the claims
                var claims = jwtToken?.Claims;
                var userIdClaim = claims?.FirstOrDefault(c => c.Type == "nameid");

                // Compare the user ID from the token with the expected user ID
                sessionId = int.Parse(userIdClaim.Value);
            }
            catch (SecurityTokenExpiredException)
            {
                // Handle token expiration (returning false in this case)
                Console.WriteLine("Token has expired.");
                throw new Exception("Token has Expired");
            }
            catch (Exception ex)
            {
                // Handle other token validation exceptions
                throw new Exception($"Token validation error: {ex.Message}");
                
            }
        }
        #endregion
    }

}
