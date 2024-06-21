using System.Collections.Generic;
using FinalDemo.BL.Interface.Service;
using FinalDemo.Models.POCO;
using Microsoft.AspNetCore.Mvc;
using FinalDemo.Models;
using FinalDemo.Models.DTO;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller for managing sale deals.
    /// </summary>
    [Route("api/SaleDeals")]
    [ApiController]
    public class CLSAL01Controller : ControllerBase
    {
        #region Private Fields
        private readonly IBLSAL01 _saleDealService;
        private Response _objResponse;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CLSAL01Controller"/> class.
        /// </summary>
        /// <param name="saleDealService">The sale deal service.</param>
        public CLSAL01Controller(IBLSAL01 saleDealService)
        {
            _saleDealService = saleDealService;
        }

        #endregion
       
        #region Public Methods
        /// <summary>
        /// Gets all sale deals.
        /// </summary>
        /// <returns>A response containing all sale deals.</returns>
        [HttpGet("All")]
        public IActionResult AllSaleDeals()
        {
            _objResponse = _saleDealService.GetAllSaleDeals();
            if (_objResponse == null || _objResponse.Data == null)
            {
                return NotFound("No sale deal transactions found.");
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Gets a sale deal by ID.
        /// </summary>
        /// <param name="id">The ID of the sale deal.</param>
        /// <returns>A response containing the sale deal if found; otherwise, NotFound result.</returns>
        [HttpGet("{id}")]
        public IActionResult SaleDealById(int id)
        {
            _objResponse = _saleDealService.GetSaleDealById(id);
            if (_objResponse == null || _objResponse.Data == null)
            {
                return NotFound("Sale deal not found.");
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Adds a new sale deal.
        /// </summary>
        /// <param name="objDTOSAL01">The sale deal data transfer object.</param>
        /// <returns>A response indicating the result of the add operation.</returns>
        [HttpPost("Add")]
        public IActionResult AddSaleDeal(DTOSAL01 objDTOSAL01)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ModelState);
            }

            _saleDealService.PreSave(objDTOSAL01);
            _saleDealService.Type = enmOperation.A;
            _objResponse = _saleDealService.ValidationOnSave();
            if (_objResponse.IsError)
            {
                return Ok(_objResponse);
            }

            _objResponse = _saleDealService.Save();
            if (_objResponse.IsError)
            {
                return Ok(_objResponse);
            }

            return Ok(_objResponse);
        }

        /// <summary>
        /// Updates an existing sale deal.
        /// </summary>
        /// <param name="objDTOSAL01">The sale deal data transfer object.</param>
        /// <returns>A response indicating the result of the update operation.</returns>
        [HttpPut("Update/{id}")]
        public IActionResult UpdateSaleDeal(DTOSAL01 objDTOSAL01)
        {
            _saleDealService.Type = enmOperation.E;
            _saleDealService.PreSave(objDTOSAL01);
            Response validationResponse = _saleDealService.ValidationOnSave();
            if (validationResponse.IsError)
            {
                return Ok(validationResponse.Message);
            }

            var saveResponse = _saleDealService.Save();
            if (saveResponse.IsError)
            {
                return Ok(saveResponse.Message);
            }

            return StatusCode(StatusCodes.Status200OK, saveResponse.Message);
        }

        /// <summary>
        /// Deletes a sale deal by ID.
        /// </summary>
        /// <param name="id">The ID of the sale deal to delete.</param>
        /// <returns>A response indicating the result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteSaleDeal(int id)
        {
            _objResponse = _saleDealService.GetSaleDealById(id);

            _objResponse = _saleDealService.RemoveSaleDeal(id);
            if (_objResponse.IsError)
            {
                return Ok(_objResponse);
            }

            return Ok(_objResponse);
        } 
        #endregion
    }
}
