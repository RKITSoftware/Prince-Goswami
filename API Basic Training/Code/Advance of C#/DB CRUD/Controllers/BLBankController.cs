using DB_CRUD.Models;
using DB_CRUD.BL;
using System.Net.Http;
using System.Web.Http;

namespace DB_CRUD.Controllers
{
    /// <summary>
    /// Controller for managing CLBank data through CRUD operations.
    /// </summary>
    [RoutePrefix("api/CLBank")]
    public class CLBankController : ApiController
    {
        #region private fields
        /// <summary>
        /// Instance of BLBank to handle bank-related business logic.
        /// </summary>
        private BLBank _blBank;

        /// <summary>
        /// Response object to store the result of operations.
        /// </summary>
        private Response _objResponse;
        #endregion

        /// <summary>
        /// Constructor for CLBankController class.
        /// </summary>
        public CLBankController()
        {
            // Initialize BLBank instance for handling bank-related business logic.
            _blBank = new BLBank();

            // Initialize Response object to store operation results.
            _objResponse = new Response();
        }

        /// <summary>
        /// Retrieves all banks.
        /// </summary>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllBank()
        {
            _objResponse = _blBank.GetAll();
            return Ok(_objResponse);
        }

/*        /// <summary>
        /// Retrieves bank information by ID.
        /// </summary>
        ///  /// <param name="id">id of the bank.</param>
        /// <returns>IHttpActionResult indicating the result of the select operation.</returns>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetBankById(int id)
        {
            _objResponse = _blBank.Get(id);
            return Ok(_objResponse);
        }
*/
        /// <summary>
        /// Adds a new bank.
        /// </summary>
        /// <param name="objDTOBNK01">DTO object containing bank data to be add.</param>
        /// <returns>IHttpActionResult indicating the result of the add operation.</returns>        [HttpPost]
        [Route("Add")]
        public IHttpActionResult AddBank(DTOBNK01 objDTOBNK01)
        {
            _blBank.presave(objDTOBNK01);
            _blBank.Operation = EnmOperation.A;
            _objResponse = _blBank.Validation();

            if (!_objResponse.IsError)
            {
                _objResponse = _blBank.Save();
            }

            return Ok(_objResponse);
        }

        /// <summary>
        /// Updates bank data using HTTP PUT method.
        /// </summary>
        /// <param name="objDTOBNK01">DTO object containing bank data to be updated.</param>
        /// <returns>IHttpActionResult indicating the result of the update operation.</returns>
        [HttpPut]
        [Route("Update")]
        public IHttpActionResult UpdateBankData(DTOBNK01 objDTOBNK01)
        {
            _blBank.presave(objDTOBNK01);
            _blBank.Operation = EnmOperation.E;
            _objResponse = _blBank.Validation();

            if (!_objResponse.IsError)
            {
                _objResponse = _blBank.Save();
            }

            return Ok(_objResponse);
        }

        /// <summary>
        /// Deletes an bank by ID.
        /// </summary>
        /// <param name="id">Bank ID</param>
        /// <returns>IHttpActionResult indicating the result of the delete operation.</returns>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteBank(int id)
        {
            _blBank.Operation = EnmOperation.D;
            _objResponse = _blBank.ValidationOnDelete(id);

            if (!_objResponse.IsError)
            {
                _objResponse = _blBank.Save();
            }

            return Ok(_objResponse);
        }
    }
}
