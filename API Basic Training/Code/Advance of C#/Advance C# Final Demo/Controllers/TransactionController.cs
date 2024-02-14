using Advance_C__Final_Demo.BL.Interface;
using Advance_C__Final_Demo.DAL.Transaction;
using Advance_C__Final_Demo.DAL.User;
using Advance_C__Final_Demo.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Advance_C__Final_Demo.Controllers
{
    [RoutePrefix("api/atm")]
    public class ATMController : ApiController
    {
        private readonly IBLUserService _userService = new UserService();
        private readonly IBLTransactionService _transactionService = new TransactionService();

        //public ATMController(IBLUserService userService, IBLTransactionService transactionService)
        //{
        //    _userService = userService;
        //    _transactionService = transactionService;
        //}

        // Endpoint to get user details by ID
        [HttpGet]
        [Route("user/{userId}")]
        public IHttpActionResult GetUserDetails(int userId)
        {
            USR01 user = _userService.GetUserDetails(userId);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound(); // User not found
        }

        // Endpoint to get user details by card number
        [HttpGet]
        [Route("user/card/{cardNumber}")]
        public IHttpActionResult GetUserDetailsByCardNumber(string cardNumber)
        {
            USR01 user = _userService.GetUserDetailsByCardNumber(cardNumber);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound(); // User not found
        }

        // Endpoint to deposit money
        [HttpPost]
        [Route("deposit")]
        public IHttpActionResult Deposit([FromBody] TransactionRequest request)
        {
            if (request != null)
            {
                decimal newBalance = _userService.Deposit(request.UserId, request.Amount);
                return Ok(newBalance);
            }

            return BadRequest(); // Invalid request
        }

        // Endpoint to withdraw money
        [HttpPost]
        [Route("withdraw")]
        public IHttpActionResult Withdraw([FromBody] TransactionRequest request)
        {
            if (request != null)
            {
                decimal newBalance = _userService.Withdraw(request.UserId, request.Amount);
                return Ok(newBalance);
            }

            return BadRequest(); // Invalid request
        }

        // Endpoint to get transaction history for a user
        [HttpGet]
        [Route("transactions/{userId}")]
        public IHttpActionResult GetTransactionHistory(int userId)
        {
            List<TRN01> transactions = _transactionService.GetTransactionsByUserId(userId);

            if (transactions != null && transactions.Count > 0)
            {
                return Ok(transactions);
            }

            return NotFound(); // No transactions found
        }

       
    }
}
