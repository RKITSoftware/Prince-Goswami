using System;
using static FinalDemo.BL.BLHelper;
using FinalDemo.BL.Interface.Service;
using FinalDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using FinalDemo.Models.DTO;
using FinalDemo.Models;
using FinalDemo.Extension;
using System.Data;
using FinalDemo.BL.Interface;

namespace FinalDemo.BL.Services
{
    /// <summary>
    /// Service class for managing Customer operations (BLCUS01).
    /// </summary>
    public class BLCUS01 : IBLCUS01
    {
        #region Private Fields

        /// <summary>
        /// Database connection factory for ORM Lite.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Holds the instance of CUS01 being operated on.
        /// </summary>
        private CUS01 _objCUS01;

        #endregion

        #region Public Fields

        /// <summary>
        /// Specifies the type of operation (Add or Edit).
        /// </summary>
        public enmOperation Type { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the BLCUS01 class with configuration.
        /// </summary>
        /// <param name="configuration">The configuration interface.</param>
        public BLCUS01(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares the internal CUS01 object for saving based on the provided DTO.
        /// </summary>
        /// <param name="objDto">The DTO containing data to convert to CUS01.</param>
        public void PreSave(DTOCUS01 objDto)
        {
            _objCUS01 = objDto.Convert<CUS01>();
        }

        /// <summary>
        /// Validates the CUS01 object before saving based on the operation type.
        /// </summary>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        public Response ValidationOnSave()
        {
            if (Type == enmOperation.A)
            {
                if (IfPhoneNumberExists(_objCUS01.S01F05))
                {
                    return PreConditionFailedResponse("PhoneNumber Already exists");
                }
                if (IfEmailExists(_objCUS01.S01F04))
                {
                    return PreConditionFailedResponse("Email Already exists");
                }
            }
            else if (Type == enmOperation.E)
            {
                if (!CustomerExists(_objCUS01.S01F01).Data)
                {
                    return PreConditionFailedResponse("Customer not found");
                }
            }
            return OkResponse();
        }

        /// <summary>
        /// Saves the CUS01 object to the database based on the operation type.
        /// </summary>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        public Response Save()
        {
            string message = "";
            if (Type == enmOperation.A)
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(_objCUS01);
                }
                message = "Customer added successfully..!!";
            }
            else if (Type == enmOperation.E)
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Update(_objCUS01);
                }
                message = "Updated successfully..!!";
            }
            return OkResponse(message);
        }

        /// <summary>
        /// Validates if the CUS01 object exists in the database for deletion.
        /// </summary>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        public Response ValidationOnDelete(int customerId)
        {
            if (!CustomerExists(customerId).Data)
            {
                return PreConditionFailedResponse("Customer not found");
            }
            return OkResponse();
        }

        /// <summary>
        /// Removes a customer from the database by their ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer to remove.</param>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        public Response RemoveCustomer(int customerId)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<CUS01>(customerId);
            }
            return OkResponse("Customer removed successfully..!!");
        }

        /// <summary>
        /// Retrieves a customer from the database by their ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer to retrieve.</param>
        /// <returns>A Response object indicating success or failure with the retrieved customer data.</returns>
        public Response GetCustomerById(int customerId)
        {
            if (customerId >= 0)
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    _objCUS01 = db.SingleById<CUS01>(customerId);
                }
            }
            return OkResponse("Data fetched!!", _objCUS01);
        }

        /// <summary>
        /// Retrieves all customers from the database.
        /// </summary>
        /// <returns>A Response object indicating success or failure with the list of customers.</returns>
        public Response GetAllCustomers()
        {
            List<CUS01> lstCUS01;
            using (var db = _dbFactory.OpenDbConnection())
            {
                lstCUS01 = db.Select<CUS01>();
            }
            return OkResponse("All Customer Details", lstCUS01);
        }

        /// <summary>
        /// Checks if a customer exists in the database by their ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer to check.</param>
        /// <returns>A Response object indicating success or failure with a message and boolean result.</returns>
        public Response CustomerExists(int customerId)
        {
            bool flag = false;
            using (var db = _dbFactory.OpenDbConnection())
            {
                flag = db.SingleById<CUS01>(customerId) != null;
            }
            if (flag)
            {
                return OkResponse("Customer found", flag);
            }
            return OkResponse("Customer not found", flag);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Checks if a phone number already exists in the database.
        /// </summary>
        /// <param name="phoneNumber">The phone number to check.</param>
        /// <returns>True if the phone number exists in the database; otherwise, false.</returns>
        private bool IfPhoneNumberExists(string phoneNumber)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                // Check if the phone number exists
                bool phoneNumberExists = db.Single<CUS01>(c => c.S01F05 == phoneNumber) != null;

                // Return true if either phone number or email exists
                return phoneNumberExists;
            }
        }
        
        /// <summary>
        /// Checks if a email already exists in the database.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>True if the email exists in the database; otherwise, false.</returns>
        private bool IfEmailExists(string email)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                // Check if the email exists
                bool emailExists = db.Single<CUS01>(c => c.S01F04 == email) != null;

                // Return true if either phone number or email exists
                return emailExists;
            }
        }

        #endregion
    }
}

