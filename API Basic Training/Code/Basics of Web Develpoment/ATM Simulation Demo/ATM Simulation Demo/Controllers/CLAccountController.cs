using ATM_Simulation_Demo.DAL.Pin;
using System;
using System.Web.Http;
using System.Collections.Generic;
using ATM_Simulation_Demo.BAL.Interface.V1;
using ATM_Simulation_Demo.Models.V1;
using ATM_Simulation_Demo.DAL.Account.V1;

namespace ATM_Simulation_Demo.Controllers
{
    /// <summary>
    /// API controller for managing account-related operations.
    /// </summary>
    //[ApiVersion("1.0")]
    [RoutePrefix("api/V1/accounts")]
    public class AccountController : ApiController
    {
        #region fields
        private readonly static IBLPinModule _pinModule = new PinModule();
        private readonly static IBLAccountRepository _accountRepo = new AccountRepository(_pinModule);
        private readonly IBLAccountService _accountService = new AccountService(_accountRepo, _pinModule);

   
        // Cache manager instance for caching responses
        private static CacheManager cacheManager = new CacheManager();

        #endregion

        #region Actions

        /// <summary>
        /// Get account information based on card number and PIN.
        /// </summary>
        /// <param name="cardNumber">Account's card number.</param>
        /// <param name="pin">Account's PIN.</param>
        /// <returns>Account information if found, otherwise NotFound.</returns>
        [HttpGet]
        [Route("{cardNumber}/{pin}")]
        public IHttpActionResult GetAccount(string cardNumber, string pin)
        {
            try
            {
                var account = _accountService.GetAccount(cardNumber, pin);
                if (account != null)
                {
                    return Ok(account);
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
        /// Create a new account.
        /// </summary>
        /// <param name="name">Account's name.</param>
        /// <param name="mobileNumber">Account's mobile number.</param>
        /// <param name="DOB">Account's date of birth.</param>
        /// <returns>Newly created account information.</returns>
        /// 
        [CustomAuthenticationFilter]
        [CustomAuthorizationFilter(Roles = "Admin")]
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateAccount(string name, string mobileNumber)
        {
            try
            {
                var newAccount = _accountService.CreateAccount(name, mobileNumber, DateTime.Now);
                return Ok(newAccount);
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
        [CustomAuthenticationFilter]
        [CustomAuthorizationFilter(Roles = "User")]
        [HttpPatch]
        [Route("changePin")]
        public IHttpActionResult ChangePin(ChangePinRequest request)
        {
            try
            {
                // Assuming ChangePinRequest is a model containing currentPin, newPin, and accountId properties
                var account = _accountService.GetAccountByID(request.accountId);
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
        [CustomAuthenticationFilter]
        [CustomAuthorizationFilter(Roles = "DEO,User")]
        [HttpPatch]
        [Route("UpdateMobileNumber")]
        public IHttpActionResult UpdateMobileNumber(UpdateMobileNumberRequest request)
        {
            try
            {
                // Assuming UpdateMobileNumberRequest is a model containing newMobileNumber and accountId properties
                var account = _accountService.GetAccountByID(request.accountId);
                _accountService.UpdateMobileNumber(account, request.newMobileNumber);
                return Ok("Mobile number updated successfully.");
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return BadRequest("Internal Server Error");
            }
        }

        private List<BLAccountModel> fetch()
        {
            return _accountService.GetAllAccounts();
        }

        [CustomAuthenticationFilter]
        [CustomAuthorizationFilter(Roles = "Admin,DEO")]
        [HttpGet]
        [Route("GetAllAccounts")]
        public IHttpActionResult GetAllAccounts()
        {
            return Ok(cacheManager.GetCachedResponse(Request, fetch));
        }
        

        #endregion
    }

    #region Request Model
    public class ChangePinRequest
    {
        public int accountId { get; set; }
        public string currentPin { get; set; }
        public string newPin { get; set; }

    };

    public class UpdateMobileNumberRequest
    {
        public int accountId { get; set; }

        public string newMobileNumber { get; set; }
    }
    #endregion
}

