
using ATM_Simulation_Demo.DAL;
using System;
using System.Web.Http;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.BAL.Services;
using ATM_Simulation_Demo.Others.Auth.User;
using System.Net.Http;
using System.Net;
using System.Web.ModelBinding;
using ATM_Simulation_Demo.Others.Auth.Account;
using System.Collections.Generic;
using ATM_Simulation_Demo.Models;

namespace ATM_Simulation_Demo.Controllers
{
    /// <summary>
    /// API controller for managing account-related operations.
    /// </summary>
    //[ApiVersion("1.0")]
    [RoutePrefix("api/accounts")]
    public class AccountController : ApiController
    {
        #region fields
        private readonly static IBLPinModule _pinModule = new PinModule();
        private readonly static IBLAccountRepository _accountRepo = new AccountRepository(_pinModule);
        private readonly IBLAccountService _accountService = new AccountService(_accountRepo, _pinModule);



        #endregion

        #region Actions

        /// <summary>
        /// Get account information based on card number and PIN.
        /// </summary>
        /// <param name="cardNumber">Account's card number.</param>
        /// <param name="pin">Account's PIN.</param>
        /// <returns>Account information if found, otherwise NotFound.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("{cardNumber}/{pin}")]
        public HttpResponseMessage Login(string cardNumber, string pin)
        {
            try
            {
                var account = _accountService.GetAccount(cardNumber, pin);
                if (account != null)
                {
                    // Generate a JWT token using the authentication service
                    string token = TokenManager.GenerateToken(account.C01F01);

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
        /// Create a new account.
        /// </summary>
        /// <param name="name">Account's name.</param>
        /// <param name="mobileNumber">Account's mobile number.</param>
        /// <param name="DOB">Account's date of birth.</param>
        /// <returns>Newly created account information.</returns>
        /// 
        [Others.Auth.User.CustomAuthenticationFilter]
        [Others.Auth.User.CustomAuthorizationFilter(Roles = "Admin")]
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateAccount(CreateAccountRequest request)
        {
            try
            {
                _accountService.CreateAccount(request.name, request.mobileNumber, request.DOB);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// Changes the PIN for a account.
        /// </summary>
        /// <param name="request">The request containing currentPin, newPin, and accountId.</param>
        /// <returns>Action result indicating the result of the operation.</returns>
        [Others.Auth.Account.CustomAuthenticationFilter]
        [Others.Auth.Account.CustomAuthorizationFilter]
        [HttpPatch]
        [Route("changePin")]
        public IHttpActionResult ChangePin(ChangePinRequest request)
        {
            try
            {
                // Assuming ChangePinRequest is a model containing currentPin, newPin, and accountId properties
                var account = _accountService.GetAccountByID(TokenManager.sessionId);
                _accountService.ChangePin(account, request.currentPin, request.newPin);
                return Ok("PIN changed successfully.");
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return BadRequest("Internal Server Error");
            }
        }

        /// <summary>
        /// Updates the mobile number for a account.
        /// </summary>
        /// <param name="request">The request containing newMobileNumber and accountId.</param>
        /// <returns>Action result indicating the result of the operation.</returns>
        [Others.Auth.Account.CustomAuthenticationFilter]
        [Others.Auth.Account.CustomAuthorizationFilter]
        [HttpPatch]
        [Route("UpdateMobileNumber")]
        public IHttpActionResult UpdateMobileNumber( string newMobileNumber)
        {
            try
            {
                // Assuming UpdateMobileNumberRequest is a model containing newMobileNumber and accountId properties
                var account = _accountService.GetAccountByID(TokenManager.sessionId);
                _accountService.UpdateMobileNumber(account, newMobileNumber);
                return Ok("Mobile number updated successfully.");
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return BadRequest("Internal Server Error");
            }
        }

        [Others.Auth.User.CustomAuthenticationFilter]
        [Others.Auth.User.CustomAuthorizationFilter(Roles = "Admin")]
        [HttpGet]
        [Route("GetAllAccounts")]
        public IHttpActionResult fetch()
        {
            return Ok(_accountService.GetAllAccounts());
        }

        /// <summary>
        /// Get account information based on card number and PIN.
        /// </summary>
        /// <param name="cardNumber">Account's card number.</param>
        /// <param name="pin">Account's PIN.</param>
        /// <returns>Account information if found, otherwise NotFound.</returns>
        [Others.Auth.User.CustomAuthenticationFilter]
        [Others.Auth.User.CustomAuthorizationFilter(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteAccount")]
        public IHttpActionResult Delete()
        {
            try
            {
                _accountService.Delete(TokenManager.sessionId);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get account information based on card number and PIN.
        /// </summary>
        /// <param name="cardNumber">Account's card number.</param>
        /// <param name="pin">Account's PIN.</param>
        /// <returns>Account information if found, otherwise NotFound.</returns>

        [Others.Auth.User.CustomAuthenticationFilter]
        [Others.Auth.User.CustomAuthorizationFilter(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _accountService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return InternalServerError(ex);
            }
        }


        #endregion

        #region Request Model
        public class ChangePinRequest
        {
            public string currentPin { get; set; }
            public string newPin { get; set; }

        };

     

        public class CreateAccountRequest
        {
            public string name { get; set;}
            public string mobileNumber { get; set;}
            public DateTime DOB { get; set; }
        }
        #endregion
    }
}

