using ATM_Simulation_Demo.BAL;
using ATM_Simulation_Demo.BAL.ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.DAL.Pin;
using ATM_Simulation_Demo.DAL.Transaction;
using ATM_Simulation_Demo.DAL.Account;
using ATM_Simulation_Demo.Models;
using System;
using System.Web.Http;

namespace ATM_Simulation_Demo.Controllers
{
    /// <summary>
    /// API controller for managing transaction-related operations.
    /// </summary>
    //[RoutePrefix("api/transactions")]
    public class TransactionController : ApiController
    {
        private readonly static IBLPinModule _pinModule = new PinModule();
        private readonly static IBLAccountRepository _accountRepo = new AccountRepository(_pinModule);
        private readonly static IBLTransactionRepository _transactionRepo = new TransactionRepository();
        private readonly IBLTransactionService _transactionService = new TransactionService(_accountRepo,_transactionRepo);


        #region Actions

        /// <summary>
        /// Adds a transaction to the account's transaction history.
        /// </summary>
        /// <param name="request">The request containing accountId and transaction details.</param>
        /// <returns>Action result indicating the result of the operation.</returns>
        [HttpPost]
        [Route("addTransaction")]
        public IHttpActionResult AddTransaction([FromBody] AddTransactionRequest request)
        {
            try
            {
                // Assuming AddTransactionRequest is a model containing accountId and transaction details
                var account = _accountRepo.GetAccountByID(request.AccountId);
                _transactionService.AddTransaction(account, request.Transaction);
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

    public class AddTransactionRequest
    {
        public int AccountId { get; set; }
        public BLTransactionModel Transaction { get; set; }
    }
}
