using System.Collections.Generic;
using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.Models.POCO;
using Microsoft.AspNetCore.Mvc;

namespace DealerManagementSystem.Controllers
{
    [Route("api/Vehicle")]
    [ApiController]
    public class VEH01Controller : ControllerBase
    {
        private readonly IBLVEH01 _vehicleService;

        public VEH01Controller(IBLVEH01 vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [Route("All")]
        [HttpGet]
        public IEnumerable<VEH01> AllVehicles()
        {
            return _vehicleService.GetAllVehicles();
        }

        // GET api/veh01/5
        [Route("{id}")]
        [HttpGet]
        public IActionResult VehicleById(int id)
        {
            var vehicle = _vehicleService.GetVehicleById(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }

        // POST api/veh01
        [Route("Add")]
        [HttpPost]
        public IActionResult AddVehicle(VEH01 vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _vehicleService.AddVehicle(vehicle);
            return StatusCode(StatusCodes.Status200OK);
        }

        // PUT api/veh01/5
        [HttpPut("Update")]
        public IActionResult UpdateVehicle(int id, VEH01 vehicle)
        {
            if (!ModelState.IsValid || id != vehicle.H01F01)
            {
                return BadRequest();
            }
            _vehicleService.UpdateVehicle(vehicle);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("id")]
        public IActionResult DeleteVehicle(int id)
        {
            if(!_vehicleService.VehicleExists(id)) return NotFound();
            _vehicleService.RemoveVehicle(id);
            return Ok();
        }
    }
}
