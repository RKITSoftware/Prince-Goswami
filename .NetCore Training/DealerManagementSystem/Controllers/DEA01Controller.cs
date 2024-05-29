using System.Collections.Generic;
using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.Models.POCO;
using Microsoft.AspNetCore.Mvc;

namespace DealerManagementSystem.Controllers
{
    [Route("api/Dealer")]
    [ApiController]
    public class DEA01Controller : ControllerBase
    {
        private readonly IBLDEA01 _dealerService;

        public DEA01Controller(IBLDEA01 dealerService)
        {
            _dealerService = dealerService;
        }

        [Route("All")]
        [HttpGet]
        public IEnumerable<DEA01> AllDealers()
        {
            return _dealerService.GetAllDealers();
        }

        // GET api/dea/5
        [Route("{id}")]
        [HttpGet]
        public IActionResult DealerById(int id)
        {
            var dealer = _dealerService.GetDealerById(id);
            if (dealer == null)
            {
                return NotFound();
            }
            return Ok(dealer);
        }

        // POST api/dea
        [Route("Add")]
        [HttpPost]
        public IActionResult AddDealer(DEA01 dealer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dealerService.AddDealer(dealer);
            return StatusCode(StatusCodes.Status200OK);
        }

        // PUT api/dea/5
        [HttpPut("Update")]
        public IActionResult UpdateDealer(int id, DEA01 dealer)
        {
            if (!ModelState.IsValid || id != dealer.A01F01)
            {
                return BadRequest();
            }
            _dealerService.UpdateDealer(dealer);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("id")]
        public IActionResult DeleteDealer(int id)
        {
            var existingDealer = _dealerService.GetDealerById(id);
            if (existingDealer == null)
            {
                return NotFound();
            }
            _dealerService.RemoveDealer(id);
            return Ok(existingDealer);
        }
    }
}
