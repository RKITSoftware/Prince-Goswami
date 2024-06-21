using FinalDemo.BL.Interface.Service;
using FinalDemo.Models.POCO;
using FinalDemo.Models.DTO;
using FinalDemo.Models;
using FinalDemo.Extension;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;
using static FinalDemo.BL.BLHelper;
namespace FinalDemo.BL.Services
{
    /// <summary>
    /// Service class for managing Customer Transaction operations (BLCUS02).
    /// </summary>
    public class BLCUS02 : IBLCUS02
    {
        #region Private Fields

        /// <summary>
        /// Database connection factory for ORM Lite.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Holds the instance of CUS02 being operated on.
        /// </summary>
        private CUS02 _objCUS02;

        #endregion

        #region Public Fields

        /// <summary>
        /// Specifies the type of operation (Add or Edit).
        /// </summary>
        public enmOperation Type { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the BLCUS02 class with configuration.
        /// </summary>
        /// <param name="configuration">The configuration interface.</param>
        public BLCUS02(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares the internal CUS02 object for saving based on the provided DTO.
        /// </summary>
        /// <param name="objDTOCUS02">The DTO containing data to convert to CUS02.</param>
        public void PreSave(DTOCUS02 objDTOCUS02)
        {
            _objCUS02 = objDTOCUS02.Convert<CUS02>();
        }

        /// <summary>
        /// Validates the CUS02 object before saving based on the operation type.
        /// </summary>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        public Response ValidationOnSave()
        {
            if (Type == enmOperation.A)
            {
                if (CustomerTransactionExists(_objCUS02.S02F01))
                {
                    return PreConditionFailedResponse("Transaction ID already exists");
                }
            }
            else if (Type == enmOperation.E)
            {
                if (!CustomerTransactionExists(_objCUS02.S02F01))
                {
                    return PreConditionFailedResponse("Transaction not found");
                }
            }
            return OkResponse();
        }

        /// <summary>
        /// Saves the CUS02 object to the database based on the operation type.
        /// </summary>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        public Response Save()
        {
            string message = "";
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (Type == enmOperation.A)
                {
                    db.Insert(_objCUS02);
                    message = "Customer transaction added successfully..!!";
                }
                else if (Type == enmOperation.E)
                {
                    db.Update(_objCUS02);
                    message = "Customer transaction updated successfully..!!";
                }
            }
            return OkResponse(message);
        }

        /// <summary>
        /// Validates if the CUS02 object exists in the database for deletion.
        /// </summary>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        public Response ValidationOnDelete(int N01F01)
        {
            if (!CustomerTransactionExists(N01F01))
            {
                return PreConditionFailedResponse("Transaction not found");
            }
            return OkResponse();
        }

        /// <summary>
        /// Removes a customer transaction from the database by their ID.
        /// </summary>
        /// <param name="N01F01">The ID of the customer transaction to remove.</param>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        public Response RemoveCustomerTransaction(int N01F01)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<CUS02>(N01F01);
            }
            return OkResponse("Customer transaction removed successfully..!!");
        }

        /// <summary>
        /// Retrieves a customer transaction from the database by their ID.
        /// </summary>
        /// <param name="N01F01">The ID of the customer transaction to retrieve.</param>
        /// <returns>A Response object indicating success or failure with the retrieved customer transaction data.</returns>
        public Response GetCustomerTransactionById(int N01F01)
        {
            if (N01F01 >= 0)
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    _objCUS02 = db.SingleById<CUS02>(N01F01);
                }
            }
            return OkResponse("Data fetched!!", _objCUS02);
        }

        /// <summary>
        /// Retrieves all customer transactions from the database.
        /// </summary>
        /// <returns>A Response object indicating success or failure with the list of customer transactions.</returns>
        public Response GetAllCustomerTransactions()
        {
            List<CUS02> lstCUS02;
            using (var db = _dbFactory.OpenDbConnection())
            {
                lstCUS02 = db.Select<CUS02>();
            }
            return OkResponse("All Customer Transactions Details", lstCUS02);
        }

        /// <summary>
        /// Checks if a customer transaction exists in the database by its ID.
        /// </summary>
        /// <param name="N01F01">The ID of the customer transaction to check.</param>
        /// <returns>A Response object indicating success or failure with a message and boolean result.</returns>
        public bool CustomerTransactionExists(int N01F01)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.SingleById<CUS02>(N01F01) != null;
            }
        }

        #endregion
    }
}
