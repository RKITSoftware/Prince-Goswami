using System.Collections.Generic;
using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DealerManagementSystem.Controllers
{
    [Route("api/SaleDeal")]
    [ApiController]
    public class SAL01Controller : ControllerBase
    {
        private readonly IBLSAL01 _saleDealService;

        public SAL01Controller(IBLSAL01 saleDealService)
        {
            _saleDealService = saleDealService;
        }

        [Route("All")]
        [HttpGet]
        public IEnumerable<SAL01> AllSaleDeals()
        {
            return _saleDealService.GetAllSaleDeals();
        }

        // GET api/sal01/5
        [Route("{id}")]
        [HttpGet]
        public IActionResult SaleDealById(int id)
        {
            var saleDeal = _saleDealService.GetSaleDealById(id);
            if (saleDeal == null)
            {
                return NotFound();
            }
            return Ok(saleDeal);
        }

        // POST api/sal01
        [Route("Add")]
        [HttpPost]
        public IActionResult AddSaleDeal(SAL01 saleDeal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _saleDealService.AddSaleDeal(saleDeal);
            return StatusCode(StatusCodes.Status200OK);
        }

        // PUT api/sal01/5
        [HttpPut("Update")]
        public IActionResult UpdateSaleDeal(int id, SAL01 saleDeal)
        {
            if (!ModelState.IsValid || id != saleDeal.L01F01)
            {
                return BadRequest();
            }
            _saleDealService.UpdateSaleDeal(saleDeal);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("id")]
        public IActionResult DeleteSaleDeal(int id)
        {
            var existingSaleDeal = _saleDealService.GetSaleDealById(id);
            if (existingSaleDeal == null)
            {
                return NotFound();
            }
            _saleDealService.RemoveSaleDeal(id);
            return Ok(existingSaleDeal);
        }
    }
}
