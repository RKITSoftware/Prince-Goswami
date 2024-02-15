
using Advance_C__Final_Demo.BL.Interface;
using Advance_C__Final_Demo.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Advance_C__Final_Demo.Controllers
{
    [RoutePrefix("api/transaction")]
    public class TransactionController : ApiController
    {
        private readonly IBLTransactionService _transactionService;

        public TransactionController(IBLTransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // Endpoint to get transaction details by ID
        [HttpGet]
        [Route("{transactionId}")]
        public IHttpActionResult GetTransactionDetails(int transactionId)
        {
            TRN01 transaction = _transactionService.GetTransactionById(transactionId);

            if (transaction != null)
            {
                return Ok(transaction);
            }

            return NotFound(); // Transaction not found
        }

        // Endpoint to get transactions by user ID
        [HttpGet]
        [Route("user/{userId}")]
        public IHttpActionResult GetTransactionsByUserId(int userId)
        {
            List<TRN01> transactions = _transactionService.GetTransactionsByUserId(userId);

            if (transactions != null && transactions.Count > 0)
            {
                return Ok(transactions);
            }

            return NotFound(); // No transactions found for the user
        }

        // Endpoint to add a new transaction
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddTransaction([FromBody] TransactionRequest request)
        {
            if (request != null)
            {
                TRN01 newTransaction = new TRN01
                {
                    N01F02 = request.UserId,
                    N01F03 = request.TransactionType,
                    N01F04 = request.Amount,
                    N01F05 = DateTime.Now
                };

                _transactionService.AddTransaction(newTransaction);

                return Ok(); // Transaction added successfully
            }

            return BadRequest(); // Invalid request
        }

        // Endpoint to delete a transaction
        [HttpDelete]
        [Route("delete/{transactionId}")]
        public IHttpActionResult DeleteTransaction(int transactionId)
        {
            _transactionService.DeleteTransaction(transactionId);

            return Ok(); // Transaction deleted successfully
        }


    }
}
