using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.POCO;
using ATM_Simulation_Demo.Others.Auth;
using System;
using System.Web.Http;

namespace ATM_Simulation_Demo.Controllers
{
    /// <summary>
    /// API controller for managing ATM withdrawal limits.
    /// </summary>
    [RoutePrefix("api/limit")]
    [CustomAuthenticationFilter]
    public class LimitController : ApiController
    {
        #region Private Fields

        /// <summary>
        /// Interface instance for the limit service
        /// </summary>
        private readonly IBLLimitService _limitService;

        /// <summary>
        /// Response object to handle controller responses
        /// </summary>
        private Response _objResponse;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for LimitController.
        /// </summary>
        /// <param name="limitService">Instance of the limit service interface.</param>
        public LimitController(IBLLimitService limitService)
        {
            // Assigning the limit service instance
            _limitService = limitService;
            // Initializing the response object
            _objResponse = new Response();
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Retrieves the ATM withdrawal limit for the current account.
        /// </summary>
        /// <returns>The ATM withdrawal limit for the current account.</returns>
        [HttpGet]
        [CustomAuthorizationFilter(Roles = "AccountHolder")]
        [Route("{accountID}")]
        public IHttpActionResult GetATMLimit()
        {
            _objResponse = _limitService.GetATMLimitByAccountID(TokenManager.AccountSId);
            if (_objResponse == null)
            {
                return NotFound();
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Updates the ATM withdrawal limit for the specified account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <param name="newWithdrawlLimitAmount">The new withdrawal limit amount.</param>
        /// <returns>ActionResult indicating the result of the operation.</returns>
        [HttpPost]
        [CustomAuthorizationFilter(Roles = "AccountHolder")]
        [Route("update/{accountID}")]
        public IHttpActionResult UpdateATMLimit(decimal newWithdrawlLimitAmount)
        {
            try
            {
                _objResponse = _limitService.UpdateATMLimit(TokenManager.AccountSId, newWithdrawlLimitAmount);
                return Ok(_objResponse);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Retrieves the ATM withdrawal limit for the specified account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <returns>The ATM withdrawal limit for the specified account.</returns>
        [HttpGet]
        [CustomAuthorizationFilter(Roles = "AccountHolder")]
        [Route("{accountID}")]
        public IHttpActionResult GetATMLimitById(int accountId)
        {
            _objResponse = _limitService.GetATMLimitByAccountID(accountId);
            if (_objResponse == null)
            {
                return NotFound();
            }
            return Ok(_objResponse);
        }

        #endregion
    }
}
