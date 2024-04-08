using System.Collections.Generic;
using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DealerManagementSystem.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CUS01Controller : ControllerBase
    {
        private readonly IBLCUS01 _customerService;

        public CUS01Controller(IBLCUS01 customerService)
        {
            _customerService = customerService;
        }

        [Route("All")]
        [HttpGet]
        public IEnumerable<CUS01> AllCustomers()
        {
            return _customerService.GetAllCustomers();
        }

        // GET api/cus01/5
        [Route("{id}")]
        [HttpGet]
        public IActionResult CustomerById(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST api/cus01
        [Route("Add")]
        [HttpPost]
        public IActionResult AddCustomer(CUS01 customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _customerService.AddCustomer(customer);
            return StatusCode(StatusCodes.Status200OK);
        }

        // PUT api/cus01/5
        [HttpPut("Update")]
        public IActionResult UpdateCustomer(int id, CUS01 customer)
        {
            if (!ModelState.IsValid || id != customer.S01F01)
            {
                return BadRequest();
            }
            _customerService.UpdateCustomer(customer);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("id")]
        public IActionResult DeleteCustomer(int id)
        {
            var existingCustomer = _customerService.GetCustomerById(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            _customerService.RemoveCustomer(id);
            return Ok(existingCustomer);
        }
    }
}
