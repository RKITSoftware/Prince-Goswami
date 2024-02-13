using Google.Protobuf.WellKnownTypes;
using ORM_Tools.BL;
using ORM_Tools.Models;
using System;
using System.Web.Http;

namespace ORM_Tools.Controllers
{
    /// <summary>
    /// Controller for managing CLBank data through CRUD operations.
    /// </summary>
    [RoutePrefix("api/CLBank")]
    public class CLBankController : ApiController
    {
        /// <summary>
        /// Retrieves all banks.
        /// </summary>
        [HttpGet]
        [Route("GetAllBank")]
        public IHttpActionResult GetAllBank()
        {
            return Ok(BLBank.GetAll());
        }

        /// <summary>
        /// Retrieves bank information by ID.
        /// </summary>
        [HttpGet]
        [Route("GetBankInfo/{id}")]
        public IHttpActionResult GetBankById(int id)
        {
            return Ok(BLBank.Get(id));
        }

        /// <summary>
        /// Adds a new bank.
        /// </summary>
        /// <param name="bank">Bank data</param>
        [HttpPost]
        [Route("AddBank")]
        public IHttpActionResult AddNewBank(BNK01 bank)
        {
            try
            {
                BLBank.Add(bank);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Updates bank data.
        /// </summary>
        /// <param name="bank">Updated bank data</param>
        [HttpPut]
        [Route("UpdateBank")]
        public IHttpActionResult UpdateBankData(BNK01 bank)
        {
            try
            {
                BLBank.Update(bank);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes an bank by ID.
        /// </summary>
        /// <param name="id">Bank ID</param>
        [HttpDelete]
        [Route("DeleteBank/{id}")]
        public IHttpActionResult DeleteBank(int id)
        {
            try
            {
                BLBank.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
