
using ATM_Simulation_Demo.DAL;
using System;
using System.Web.Http;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.BAL.Services;
using ATM_Simulation_Demo.Extension;
using System.Net.Http;
using System.Net;
using ATM_Simulation_Demo.Others.Auth;
using ATM_Simulation_Demo.Models.DTO;
using ATM_Simulation_Demo.Models.POCO;
using ATM_Simulation_Demo.Models;

namespace ATM_Simulation_Demo.Controllers
{
    /// <summary>
    /// API controller for managing account-related operations.
    /// </summary>
    [RoutePrefix("api/accounts")]
    [CustomAuthenticationFilter]
    public class AccountController : ApiController
    {
        #region fields
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

        /// <summary>
        /// The response object.
        /// </summary>
        private Response _objResponse;
        #endregion


        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        public AccountController()
        {
            _pinModule = new PinModule();
            _accountRepo = new AccountRepository(_pinModule);
            _accountService = new BLAccountService(_accountRepo, _pinModule);
            _objResponse = new Response();
        }
        #endregion

        #region Actions

        /// <summary>
        /// Create a new account.
        /// </summary>
        /// <param name="objDTO_ACC01">Account DTO model</param>
        /// <returns>Newly created account information.</returns>

        [CustomAuthorizationFilter(Roles = "Admin,User")]
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateAccount(DTOACC01 objDTO_ACC01)
        {
            try
            {
                _accountService.Type = enmOperation.A;
                _objResponse = _accountService.PreValidation(objDTO_ACC01);
                if (!_objResponse.IsError)
                {
                    _accountService.PreSave(objDTO_ACC01);
                    _objResponse = _accountService.Validation();
                    if (!_objResponse.IsError)
                    {
                        _objResponse = _accountService.Save();
                    }
                }
                return Ok(_objResponse);

            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return Ok(ex);
            }
        }

        /// <summary>
        /// Create a new account.
        /// </summary>
        /// <param name="name">Account's name.</param>
        /// <param name="mobileNumber">Account's mobile number.</param>
        /// <param name="DOB">Account's date of birth.</param>
        /// <returns>Newly created account information.</returns>
        [CustomAuthorizationFilter(Roles = "Admin,AccountHolder")]
        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateAccount(DTOACC01 objDTOACC01)
        {
            try
            {
                _accountService.Type = enmOperation.E;
                _objResponse = _accountService.PreValidation(objDTOACC01);

                if (!_objResponse.IsError)
                {
                    _accountService.PreSave(objDTOACC01);
                    _accountService.Validation();
                    _objResponse = _accountService.Save();
                }
                return Ok(_objResponse);

            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return Ok(ex);
            }
        }

        /// <summary>
        /// Changes the PIN for a account.
        /// </summary>
        /// <param name="objDTOACC01">The objDTOACC01 containing currentPin, newPin, and accountId.</param>
        /// <returns>Action result indicating the result of the operation.</returns>
        [CustomAuthorizationFilter(Roles = "AccountHolder")]
        [HttpPatch]
        [Route("changePin")]
        public IHttpActionResult ChangePin(string C01F04, string C01X01)
        {
            try
            {
                _objResponse = _accountService.ChangePin(TokenManager.AccountSId, C01F04, C01X01);
                return Ok(_objResponse);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return Ok(ex);
            }
        }

        /// <summary>
        /// Updates the mobile number for a account.
        /// </summary>
        /// <param name="objDTOACC01">The objDTOACC01 containing newMobileNumber and accountId.</param>
        /// <returns>Action result indicating the result of the operation.</returns>
        [CustomAuthorizationFilter(Roles = "AccountHolder")]
        [HttpPatch]
        [Route("UpdateMobileNumber")]
        public IHttpActionResult UpdateMobileNumber(string newMobileNumber)
        {
            try
            {
                // Assuming UpdateMobileNumberRequest is a model containing newMobileNumber and accountId properties
                var account = _accountService.GetAccountByID(TokenManager.AccountSId);
                _objResponse.IsError = _accountService.UpdateSpecificField(TokenManager.AccountSId, "C01F05", newMobileNumber);
                return Ok(_objResponse);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return Ok(ex);
            }
        }

        /// <summary>
        /// Retrieves all user accounts.
        /// </summary>
        /// <remarks>
        /// Requires authentication and authorization with the role "Admin" to access.
        /// </remarks>
        /// <returns>
        /// IHttpActionResult:
        ///     - IHttpActionResult with HTTP 200 OK status code and the list of all user accounts if successful.
        ///     - IHttpActionResult with appropriate HTTP status code and error message if authorization fails or an error occurs.
        /// </returns>
        [CustomAuthorizationFilter(Roles = "Admin")]
        [HttpGet]
        [Route("GetAllAccounts")]
        public IHttpActionResult GetAllAccounts()
        {
            _objResponse = _accountService.GetAllAccounts();
            return Ok(_objResponse);
        }

        /// <summary>
        /// Get account information based on card number and PIN.
        /// </summary>
        /// <param name="cardNumber">Account's card number.</param>
        /// <param name="pin">Account's PIN.</param>
        /// <returns>Account information if found, otherwise NotFound.</returns>
        [CustomAuthorizationFilter(Roles = "AccountHolder")]
        [HttpDelete]
        [Route("DeleteAccount")]
        public IHttpActionResult Delete()
        {
            try
            {
                //// validation on delete
                _accountService.Delete(TokenManager.AccountSId);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Deletes a user account with the specified ID.
        /// </summary>
        /// <remarks>
        /// Requires authentication and authorization with the role "Admin" to access.
        /// </remarks>
        /// <param name="id">The ID of the user account to delete.</param>
        /// <returns>
        /// IHttpActionResult:
        ///     - IHttpActionResult with HTTP 200 OK status code if the account is successfully deleted.
        ///     - IHttpActionResult with appropriate HTTP status code and error message if authorization fails or an error occurs.
        /// </returns>
        [CustomAuthorizationFilter(Roles = "Admin")] // Custom authorization filter with role-based access control
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

    }
}

