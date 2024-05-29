using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.POCO;
using System;
using System.Web.Http;

namespace ATM_Simulation_Demo.Controllers
{
    /// <summary>
    /// API controller for managing ATM withdrawal limits.
    /// </summary>
    [RoutePrefix("api/limit")]
    public class LimitController : ApiController
    {
        private readonly IBLLimitService _limitService;
        private Response _objResponse;

        public LimitController(IBLLimitService limitService)
        {
            _limitService = limitService;
            _objResponse = new Response();
        }

        /// <summary>
        /// Retrieves the ATM withdrawal limit for the specified account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <returns>The ATM withdrawal limit for the specified account.</returns>
        [HttpGet]
        [Route("{accountID}")]
        public IHttpActionResult GetATMLimit(int accountID)
        {
            _objResponse = _limitService.GetATMLimitByAccountID(accountID);
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
        [Route("update/{accountID}")]
        public IHttpActionResult UpdateATMLimit(int accountID, decimal newWithdrawlLimitAmount)
        {
            try
            {
                _objResponse = _limitService.UpdateATMLimit(accountID, newWithdrawlLimitAmount);
                return Ok(_objResponse);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Deletes the ATM withdrawal limit for the specified account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <returns>ActionResult indicating the result of the operation.</returns>
        [HttpDelete]
        [Route("delete/{accountID}")]
        public IHttpActionResult DeleteATMLimit(int accountID)
        {
            try
            {
                _limitService.DeleteATMLimit(accountID);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return InternalServerError(ex);
            }
        }
    }
}
