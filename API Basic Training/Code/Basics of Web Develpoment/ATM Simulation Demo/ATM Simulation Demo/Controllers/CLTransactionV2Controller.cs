
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.DAL.Pin;
using System;
using System.Web.Http;
using ATM_Simulation_Demo.BAL.Interface.V2;
using ATM_Simulation_Demo.DAL.Account.v2;
using ATM_Simulation_Demo.DAL.Transaction.V2;
using ATM_Simulation_Demo.Models.V2;

namespace ATM_Simulation_Demo.Controllers
{
    /// <summary>
    /// API controller for managing transaction-related operations.
    /// </summary>
    [RoutePrefix("api/V2/transactions")]
    public class TransactionV2Controller : ApiController
    {
        #region fields
        private readonly static BAL.Interface.V2.IBLPinModule _pinModule = new PinModuleV2();
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
        public IHttpActionResult AddTransaction(AddTransactionRequestV2 request)
        {
            try
            {
                // Assuming AddTransactionRequest is a model containing accountId and transaction details
                _transactionService.AddTransaction(request.AccountId, request.Transaction);
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

  
}
