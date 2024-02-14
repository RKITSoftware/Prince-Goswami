using DB_CRUD.Models;
using DB_CRUD.BL;
using System.Net.Http;
using System.Web.Http;

namespace DB_CRUD.Controllers
{
    /// <summary>
    /// Controller for managing Bank data through CRUD operations
    /// </summary>
    [RoutePrefix("api/Bank")]
    public class CLBankController : ApiController
    {
        private BLBank _objOfBLBank;

        /// <summary>
        /// Retrieves all banks
        /// </summary>
        [HttpGet]
        [Route("GetAllBank")]
        public IHttpActionResult GetAllBank()
        {
            _objOfBLBank = new BLBank();
            return Ok(_objOfBLBank.GetAll());
        }

        /// <summary>
        /// Adds a new bank
        /// </summary>
        /// <param name="bank">Bank data to be added</param>
        [HttpPost]
        [Route("AddNewBank")]
        public HttpResponseMessage AddNewBank(BNK01 bank)
        {
            _objOfBLBank = new BLBank();
            return (_objOfBLBank.Add(bank));
        }

        /// <summary>
        /// Updates an existing bank
        /// </summary>
        /// <param name="id">Bank ID</param>
        /// <param name="bank">Updated bank data</param>
        [HttpPut]
        [Route("UpdateBank/{id}")]
        public HttpResponseMessage UpdateBank(int id, BNK01 bank)
        {
            _objOfBLBank = new BLBank();
            return (_objOfBLBank.Update(id, bank));
        }

        /// <summary>
        /// Deletes a bank by ID
        /// </summary>
        /// <param name="bankId">Bank ID to be deleted</param>
        [HttpDelete]
        [Route("DeleteBank")]
        public HttpResponseMessage DeleteBank(int bankId)
        {
            _objOfBLBank = new BLBank();
            return (_objOfBLBank.Delete(bankId));
        }
    }
}
