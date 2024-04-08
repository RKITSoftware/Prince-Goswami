using System.Collections.Generic;
using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DealerManagementSystem.Controllers
{
    [Route("api/CustomerTransactions")]
    [ApiController]
    public class CUS02Controller : ControllerBase
    {
        private readonly IBLCUS02 _customerService;

        public CUS02Controller(IBLCUS02 customerService)
        {
            _customerService = customerService;
        }

        [Route("All")]
        [HttpGet]
        public IEnumerable<CUS02> AllCustomers()
        {
            return _customerService.GetAllCustomerTransactions();
        }

        // GET api/cus02/5
        [Route("{id}")]
        [HttpGet]
        public IActionResult CustomerById(int id)
        {
            var customer = _customerService.GetCustomerTransactionById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST api/cus02
        [Route("Add")]
        [HttpPost]
        public IActionResult AddCustomer(CUS02 customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _customerService.AddCustomerTransaction(customer);
            return StatusCode(StatusCodes.Status200OK);
        }

        // PUT api/cus02/5
        [HttpPut("Update")]
        public IActionResult UpdateCustomer(int id, CUS02 customerTransaction)
        {
            if (!ModelState.IsValid || id != customerTransaction.S02F01)
            {
                return BadRequest();
            }
            _customerService.UpdateCustomerTransaction(customerTransaction);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("id")]
        public IActionResult DeleteCustomer(int id)
        {
            var existingCustomer = _customerService.GetCustomerTransactionById(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            _customerService.RemoveCustomerTransaction(id);
            return Ok(existingCustomer);
        }
    }
}
