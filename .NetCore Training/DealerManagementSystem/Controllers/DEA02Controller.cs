using System.Collections.Generic;
using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DealerManagementSystem.Controllers
{
    [Route("api/DealerTransactions")]
    [ApiController]
    public class DEA02Controller : ControllerBase
    {
        private readonly IBLDEA02 _dealerService;

        public DEA02Controller(IBLDEA02 dealerService)
        {
            _dealerService = dealerService;
        }

        [Route("All")]
        [HttpGet]
        public IEnumerable<DEA02> AllDealers()
        {
            return _dealerService.GetAllDealerTransactions();
        }

        // GET api/dea/5
        [Route("{id}")]
        [HttpGet]
        public IActionResult DealerById(int id)
        {
            var dealer = _dealerService.GetDealerTransactionById(id);
            if (dealer == null)
            {
                return NotFound();
            }
            return Ok(dealer);
        }

        // POST api/dea
        [Route("Add")]
        [HttpPost]
        public IActionResult AddDealer(DEA02 dealer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dealerService.AddDealerTransaction(dealer);
            return StatusCode(StatusCodes.Status200OK);
        }

        // PUT api/dea/5
        [HttpPut("Update")]
        public IActionResult UpdateDealer(int id, DEA02 dealer)
        {
            if (!ModelState.IsValid || id != dealer.A02F02)
            {
                return BadRequest();
            }
            _dealerService.UpdateDealerTransaction(dealer);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("id")]
        public IActionResult DeleteDealer(int id)
        {
            var existingDealer = _dealerService.GetDealerTransactionById(id);
            if (existingDealer == null)
            {
                return NotFound();
            }
            _dealerService.RemoveDealerTransaction(id);
            return Ok();
        }
    }
}
