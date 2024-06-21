using System.Collections.Generic;
using FinalDemo.BL.Interface.Service;
using FinalDemo.Models.POCO;
using Microsoft.AspNetCore.Mvc;
using FinalDemo.Models;
using FinalDemo.Models.DTO;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller for managing customer transactions.
    /// </summary>
    [Route("api/CustomerTransactions")]
    [ApiController]
    public class CLCUS02Controller : ControllerBase
    {
        #region Private Fields
        private readonly IBLCUS02 _customerService;
        private Response _objResponse;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CLCUS02Controller"/> class.
        /// </summary>
        /// <param name="customerService">The customer service.</param>
        public CLCUS02Controller(IBLCUS02 customerService)
        {
            _customerService = customerService;
        }
        #region Public Methods

        /// <summary>
        /// Gets all customer transactions.
        /// </summary>
        /// <returns>A response containing all customer transactions.</returns>
        [HttpGet("All")]
        public IActionResult AllCustomers()
        {
            _objResponse = _customerService.GetAllCustomerTransactions();
            if (_objResponse == null || _objResponse.Data == null)
            {
                return NotFound("No customer transactions found.");
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Gets a customer transaction by ID.
        /// </summary>
        /// <param name="id">The ID of the customer transaction.</param>
        /// <returns>A response containing the customer transaction if found; otherwise, NotFound result.</returns>
        [HttpGet("{id}")]
        public IActionResult CustomerById(int id)
        {
            _objResponse = _customerService.GetCustomerTransactionById(id);
            if (_objResponse == null || _objResponse.Data == null)
            {
                return NotFound("Customer transaction not found.");
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Adds a new customer transaction.
        /// </summary>
        /// <param name="objDTOCUS02">The customer transaction data transfer object.</param>
        /// <returns>A response indicating the result of the add operation.</returns>
        [HttpPost("Add")]
        public IActionResult AddCustomer(DTOCUS02 objDTOCUS02)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ModelState);
            }

            _customerService.PreSave(objDTOCUS02);
            _customerService.Type = enmOperation.A;
            _objResponse = _customerService.ValidationOnSave();
            if (_objResponse.IsError)
            {
                return Ok(_objResponse);
            }

            _objResponse = _customerService.Save();
            if (_objResponse.IsError)
            {
                return Ok(_objResponse);
            }

            return Ok(_objResponse);
        }

        /// <summary>
        /// Updates an existing customer transaction.
        /// </summary>
        /// <param name="objDTOCUS02">The customer transaction data transfer object.</param>
        /// <returns>A response indicating the result of the update operation.</returns>
        [HttpPut("Update/{id}")]
        public IActionResult UpdateCustomer(DTOCUS02 objDTOCUS02)
        {
            _customerService.Type = enmOperation.E;
            _customerService.PreSave(objDTOCUS02);
            Response validationResponse = _customerService.ValidationOnSave();
            if (validationResponse.IsError)
            {
                return Ok(validationResponse.Message);
            }

            var saveResponse = _customerService.Save();
            if (saveResponse.IsError)
            {
                return Ok(saveResponse.Message);
            }

            return StatusCode(StatusCodes.Status200OK, saveResponse.Message);
        }

        /// <summary>
        /// Deletes a customer transaction by ID.
        /// </summary>
        /// <param name="id">The ID of the customer transaction to delete.</param>
        /// <returns>A response indicating the result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            _objResponse = _customerService.GetCustomerTransactionById(id);

            _objResponse = _customerService.RemoveCustomerTransaction(id);
            if (_objResponse.IsError)
            {
                return Ok(_objResponse);
            }

            return Ok(_objResponse);
        }

        #endregion    
    }
}