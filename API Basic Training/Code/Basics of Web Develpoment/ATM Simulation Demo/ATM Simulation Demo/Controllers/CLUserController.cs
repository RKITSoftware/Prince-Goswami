
using ATM_Simulation_Demo.DAL.Transaction;
using ATM_Simulation_Demo.DAL.User;
using System;
using System.Web.Http;
namespace ATM_Simulation_Demo.Controllers
{
    /// <summary>
    /// API controller for managing user-related operations.
    /// </summary>
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly UserService _userService;
        private readonly TransactionService _transactionService;

        /// <summary>
        /// Constructor for UserController.
        /// </summary>
        /// <param name="userService">Instance of UserService.</param>
        /// <param name="transactionService">Instance of TransactionService.</param>
        public UserController(UserService userService, TransactionService transactionService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        }

        #region Actions

        /// <summary>
        /// Get user information based on card number and PIN.
        /// </summary>
        /// <param name="cardNumber">User's card number.</param>
        /// <param name="pin">User's PIN.</param>
        /// <returns>User information if found, otherwise NotFound.</returns>
        [HttpGet]
        [Route("{cardNumber}/{pin}")]
        public IHttpActionResult GetUser(string cardNumber, string pin)
        {
            try
            {
                var user = _userService.GetUser(cardNumber, pin);
                if (user != null)
                {
                    return Ok(user);
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
        /// Create a new user.
        /// </summary>
        /// <param name="name">User's name.</param>
        /// <param name="mobileNumber">User's mobile number.</param>
        /// <param name="DOB">User's date of birth.</param>
        /// <returns>Newly created user information.</returns>
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateUser(string name, string mobileNumber, DateTime DOB)
        {
            try
            {
                var newUser = _userService.CreateUser(name, mobileNumber, DOB);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return InternalServerError(ex);
            }
        }

        // Add more actions as needed for your API

        #endregion
    }
}

