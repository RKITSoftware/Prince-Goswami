using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.BAL.Services;
using ATM_Simulation_Demo.DAL;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.POCO;
using ATM_Simulation_Demo.Others.Auth;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ATM_Simulation_Demo.Controllers
{
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        #region fields
        /// <summary>
        /// Represents a class that manages user-related operations by utilizing repositories for user data storage and a service for user-related business logic.
        /// </summary>
        private readonly IBLUserRepository _userRepository;

        /// <summary>
        /// Represents a service for handling user-related business logic.
        /// </summary>
        private readonly IBLUserService _userService;

        /// <summary>
        /// The pin module interface.
        /// </summary>
        private static IBLPinModule _pinModule;

        /// <summary>
        /// The account repository interface.
        /// </summary>
        private static IBLAccountRepository _accountRepo;

        /// <summary>
        /// The account service interface.
        /// </summary>
        private IBLAccountService _accountService;

        #endregion

        #region Constructor
        public LoginController()
        {
            _userRepository = new UserRepository();
            _userService = new UserService(_userRepository);
            _pinModule = new PinModule();
            _accountRepo = new AccountRepository(_pinModule);
            _accountService = new BLAccountService(_accountRepo, _pinModule);
        }
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
        [Route("User")]
        public HttpResponseMessage UserLogin(string userName, string password)
        {
            try
            {
                var user = _userService.GetUserByCredentials(userName, password);
                // Check if the provided credentials are valid
                if (user != null)
                {
                    // Generate a JWT token using the authentication service
                    string token = TokenManager.GenerateToken(user.R01F01, user.R01F05.Value());

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
        /// Get account information based on card number and PIN.
        /// </summary>
        /// <param name="cardNumber">Account's card number.</param>
        /// <param name="pin">Account's PIN.</param>
        /// <returns>Account information if found, otherwise NotFound.</returns>
        [HttpGet]
        [Route("Account")]
        public HttpResponseMessage AccountLogin(string cardNumber, string pin)
        {
            try
            {
                ACC01 account = _accountService.GetAccount(cardNumber, pin).Data;
                if (account != null)
                {
                    // Generate a JWT token using the authentication service
                    string token = TokenManager.GenerateToken(account.C01F01, "AccountHolder");

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
        #endregion
    }
}
