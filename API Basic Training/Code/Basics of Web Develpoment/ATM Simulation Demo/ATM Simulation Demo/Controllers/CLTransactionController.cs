
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.BAL.Interface.V1;
using ATM_Simulation_Demo.DAL.Account.V1;
using ATM_Simulation_Demo.DAL.Pin;
using ATM_Simulation_Demo.DAL.Transaction.V1;
using ATM_Simulation_Demo.Models.V1;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ATM_Simulation_Demo.Controllers
{
    /// <summary>
    /// API controller for managing transaction-related operations.
    /// </summary>
    [RoutePrefix("api/V1/transactions")]
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
        [CustomAuthenticationFilter]
        [CustomAuthorizationFilter(Roles = "DEO,User")]
        [HttpPost]
        [Route("addTransaction")]
        public IHttpActionResult AddTransaction(int AccountId, decimal Amount, string Description )
        {
            try
            {
                // Assuming AddTransactionRequest is a model containing accountId and transaction details
                var account = _accountRepo.GetAccountByID(AccountId);
                _transactionService.AddTransaction(account,Amount,Description);
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
        [CustomAuthenticationFilter]
        [CustomAuthorizationFilter(Roles = "Admin,DEO,User")]
        [HttpGet]
        [Route("viewTransactionHistory/{accountId}")]
        public IHttpActionResult ViewTransactionHistory(int accountId)
        {
            try
            {
                var account = _accountRepo.GetAccountByID(accountId);
                var transactions = _transactionService.ViewTransactionHistory(account);
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

   
}
