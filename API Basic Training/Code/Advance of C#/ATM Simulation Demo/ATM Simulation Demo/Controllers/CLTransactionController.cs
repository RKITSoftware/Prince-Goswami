
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.DAL;
using System;
using System.Web.Http;
using ATM_Simulation_Demo.BAL.Services;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.DAL;

namespace ATM_Simulation_Demo.Controllers
{
    /// <summary>
    /// API controller for managing transaction-related operations.
    /// </summary>
    [RoutePrefix("api/transactions")]
    public class TransactionController : ApiController
    {
        #region fields
        private readonly static IBLPinModule _pinModule = new PinModule();
        private readonly static IBLAccountRepository _accountRepo = new AccountRepository(_pinModule);
        private readonly static IBLTransactionRepository _transactionRepo = new TransactionRepository();
        private readonly IBLTransactionService _transactionService = new TransactionService(_accountRepo,_transactionRepo);
        #endregion

        #region Actions

        /// <summary>
        /// Adds a transaction to the account's transaction history.
        /// </summary>
        /// <param name="request">The request containing accountId and transaction details.</param>
        /// <returns>Action result indicating the result of the operation.</returns>
        //[CustomAuthenticationFilter]
        //[CustomAuthorizationFilter(Roles = "DEO,User")]
        [HttpPost]
        [Route("addTransaction")]
        public IHttpActionResult AddTransaction(AddTransactionRequestV2 request)
        {
            try
            {
                // Assuming AddTransactionRequest is a model containing accountId and transaction details
                _transactionService.AddTransaction(request.AccountId, request.amount, request.TransactionType, request.Description);
                return Ok("Transaction added successfully.");
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
        [HttpGet]
        [Route("viewTransactionHistory/{accountId}")]
        public IHttpActionResult ViewTransactionHistory(int accountId)
        {
            try
            {
                var transactions = _transactionService.ViewTransactionHistory(accountId);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return BadRequest("Internal Server Error");
            }
        }

        #endregion
    }

    public class AddTransactionRequestV2
    {
        public int AccountId { get; set; }
        public decimal amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; }
    }
}
