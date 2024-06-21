
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.BAL.Services;
using ATM_Simulation_Demo.DAL;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.DTO;
using ATM_Simulation_Demo.Models.POCO;
using ATM_Simulation_Demo.Others.Auth;
using System;
using System.Web.Http;
using ZstdSharp.Unsafe;

namespace ATM_Simulation_Demo.Controllers
{
    /// <summary>
    /// API controller for managing transaction-related operations.
    /// </summary>
    [RoutePrefix("api/transactions")]
    [CustomAuthenticationFilter]
    public class TransactionController : ApiController
    {
        #region fields
        private readonly static IBLPinModule _pinModule = new PinModule();
        /// <summary>
        /// Represents the account repository used for handling account-related operations.
        /// </summary>
        private readonly static IBLAccountRepository _accountRepo = new AccountRepository(_pinModule);

        /// <summary>
        /// Represents the transaction repository used for handling transaction-related operations.
        /// </summary>
        private readonly static IBLTransactionRepository _transactionRepo = new TransactionRepository();

        private readonly IBLTransactionService _transactionService = new TransactionService(_accountRepo, _transactionRepo);

        /// <summary>
        /// Represents the response object used for handling responses.
        /// </summary>
        private Response _objResponse = new Response();

        #endregion

        #region Actions

        /// <summary>
        /// Adds a transaction to the account's transaction history.
        /// </summary>
        /// <param name="request">The request containing accountId and transaction details.</param>
        /// <returns>Action result indicating the result of the operation.</returns>
        [HttpPost]
        [CustomAuthorizationFilter(Roles = "AccountHolder")]
        [Route("addTransaction")]

        public IHttpActionResult AddTransaction(DTOTRN01 request)
        {
            try
            {
                _objResponse = _transactionService.PreValidation(request);

                if (!_objResponse.IsError)
                {
                    _transactionService.PreSave(request);
                    _transactionService.Validation();
                    _objResponse = _transactionService.Save();
                }
                return Ok(_objResponse);

            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return Ok(ex);
            }
        }
        //// roles 
        /// <summary>
        /// View transaction history for a account.
        /// </summary>
        /// <param name="accountId">The account's ID.</param>
        /// <returns>List of transactions in the account's history.</returns>
        //[CustomAuthenticationFilter]
        //[CustomAuthorizationFilter(Roles = "Admin,DEO,User")]
        [CustomAuthorizationFilter(Roles = "AccountHolder")]
        [HttpGet]
        [Route("viewTransactionHistory/{accountId}")]
        public IHttpActionResult ViewTransactionHistoryByID(int accountId)
        {
            try
            {
                _objResponse = _transactionService.ViewTransactionHistory(accountId);
                return Ok(_objResponse);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return BadRequest("Internal Server Error");
            }
        }

        /// <summary>
        /// View transaction history for a account.
        /// </summary>
        /// <param name="accountId">The account's ID.</param>
        /// <returns>List of transactions in the account's history.</returns>
        //[CustomAuthenticationFilter]
        //[CustomAuthorizationFilter(Roles = "Admin,DEO,User")]
        [CustomAuthorizationFilter(Roles = "AccountHolder")]
        [HttpGet]
        [Route("viewTransactionHistory")]
        public IHttpActionResult ViewTransactionHistory()
        {
            try
            {
                _objResponse = _transactionService.ViewTransactionHistory(TokenManager.AccountSId);
                return Ok(_objResponse);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return BadRequest("Internal Server Error");
            }
        }

        /// <summary>
        /// View transaction history for a account.
        /// </summary>
        /// <param name="accountId">The account's ID.</param>
        /// <returns>List of transactions in the account's history.</returns>
        [CustomAuthorizationFilter(Roles = "Admin,User")]
        [HttpGet]
        [Route("viewTransactionHistory/{accountId}")]
        public IHttpActionResult GetAllTransactions()
        {
            try
            {
                _objResponse = _transactionService.GetAllTransactions();
                return Ok(_objResponse);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return BadRequest("Internal Server Error");
            }
        }

        #endregion
    }
}
