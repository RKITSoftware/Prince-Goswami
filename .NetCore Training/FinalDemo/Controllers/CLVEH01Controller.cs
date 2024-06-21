using FinalDemo.BL.Interface.Service;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller for managing Vehicle operations via API endpoints.
    /// </summary>
    [Route("api/Vehicle")]
    [ApiController]
    public class CLVEH01Controller : ControllerBase
    {
        #region Private Fields
        private readonly IBLVEH01 _objDTOVEH01Service;
        private Response _objResponse;
        #endregion

        #region Controller
        /// <summary>
        /// Constructor for initializing VEH01Controller with objDTOVEH01 service dependency.
        /// </summary>
        /// <param name="objDTOVEH01Service">Instance of IBLVEH01 service interface.</param>
        public CLVEH01Controller(IBLVEH01 objDTOVEH01Service)
        {
            _objDTOVEH01Service = objDTOVEH01Service;
        }
        #endregion
        //// route name chnages
        #region Public Actions
        /// <summary>
        /// Retrieves all objDTOVEH01s.
        /// </summary>
        /// <returns>OkObjectResult with a Response model containing an enumerable list of all objDTOVEH01s.</returns>
        [HttpGet("All")]
        public IActionResult AllVehicles()
        {
            _objResponse = _objDTOVEH01Service.GetAllVehicles();
            return Ok(_objResponse);
        }

        /// <summary>
        /// Retrieves a objDTOVEH01 by ID.
        /// </summary>
        /// <param name="id">The ID of the objDTOVEH01 to retrieve.</param>
        /// <returns>OkObjectResult with a Response model containing the objDTOVEH01 details if found; NotFound if not found.</returns>
        [HttpGet("{id}")]
        public IActionResult VehicleById(int id)
        {
            _objResponse = _objDTOVEH01Service.GetVehicleById(id);
            if (_objResponse.Data == null)
            {
                return NotFound();
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Adds a new objDTOVEH01.
        /// </summary>
        /// <param name="objDTOVEH01">The objDTOVEH01 object to add.</param>
        /// <returns>OkObjectResult with a Response model indicating success or error.</returns>
        [HttpPost("Add")]
        public IActionResult AddVehicle(DTOVEH01 objDTOVEH01)
        {


            _objDTOVEH01Service.PreSave(objDTOVEH01);
            _objResponse = _objDTOVEH01Service.ValidationOnSave();

            if (_objResponse.IsError)
            {
                return BadRequest(_objResponse);
            }

            _objResponse = _objDTOVEH01Service.Save();
            return Ok(_objResponse);
        }

        /// <summary>
        /// Updates an existing objDTOVEH01.
        /// </summary>
        /// <param name="id">The ID of the objDTOVEH01 to update.</param>
        /// <param name="objDTOVEH01">The updated objDTOVEH01 object.</param>
        /// <returns>OkObjectResult with a Response model indicating success or error.</returns>
        [HttpPut("Update/{id}")]
        public IActionResult UpdateVehicle(int id, DTOVEH01 objDTOVEH01)
        {

            _objDTOVEH01Service.PreSave(objDTOVEH01);
            _objResponse = _objDTOVEH01Service.ValidationOnSave();

            if (_objResponse.IsError)
            {
                return BadRequest(_objResponse);
            }

            _objResponse = _objDTOVEH01Service.Save();
            return Ok(_objResponse);
        }
        /// <summary>
        /// Deletes a objDTOVEH01 by ID.
        /// </summary>
        /// <param name="R01101">The ID of the objDTOVEH01 to delete.</param>
        /// <returns>OkObjectResult with a Response model indicating success or NotFound if objDTOVEH01 not found.</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletehicle(int R01101)
        {
            _objResponse = _objDTOVEH01Service.RemoveVehicle(R01101);
            return Ok(_objResponse);
        }
        #endregion

    }
}
