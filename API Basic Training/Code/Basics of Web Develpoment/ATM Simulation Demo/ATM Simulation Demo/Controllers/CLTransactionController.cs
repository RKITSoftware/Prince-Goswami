using System;
using System.Web.Http;
using ATM_Simulation_Demo.BAL;
using ATM_Simulation_Demo.DAL.Transaction;
using ATM_Simulation_Demo.Models;

namespace ATM_Simulation_Demo.Controllers
{
    /// <summary>
    /// API controller for managing user transactions.
    /// </summary>
    [RoutePrefix("api/transactions")]
    public class TransactionController : ApiController
    {
        private readonly TransactionService _transactionService;

        /// <summary>
        /// Constructor for TransactionController.
        /// </summary>
        /// <param name="transactionService">Instance of TransactionService.</param>
        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        }

        #region Actions

        /// <summary>
        /// Add a transaction for a user.
        /// </summary>
        /// <param name="userId">User's ID.</param>
        /// <param name="description">Transaction description.</param>
        /// <param name="amount">Transaction amount.</param>
        /// <returns>Added transaction information.</returns>
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddTransaction(int userId, string description, decimal amount)
        {
            try
            {
                // Retrieve user based on userId
                BLUserModel user = _transactionService.GetUserById(userId);

                if (user != null)
                {
                    // Create a new transaction model
                    var transaction = new BLTransactionModel
                    {
                        Description = description,
                        Amount = amount,
                        Date = DateTime.Now // You may want to use a service to get the current date
                    };

                    // Add transaction to user's history
                    _transactionService.AddTransaction(user, transaction);
                    return Ok(transaction);
                }
                else
                {
                    // Handle the case where the user is null (not found)
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// View transaction history for a user.
        /// </summary>
        /// <param name="userId">User's ID.</param>
        /// <returns>List of transactions in the user's history.</returns>
        [HttpGet]
        [Route("history/{userId}")]
        public IHttpActionResult ViewTransactionHistory(int userId)
        {
            try
            {
                // Retrieve user based on userId
                BLUserModel user = _transactionService.GetUserById(userId);

                if (user != null)
                {
                    // Get the transaction history for the user
                    var transactionHistory = _transactionService.ViewTransactionHistory(user);
                    return Ok(transactionHistory);
                }
                else
                {
                    // Handle the case where the user is null (not found)
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return InternalServerError(ex);
            }
        }

        // Add more actions as needed for your API

        #endregion
    }
}
