using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.DAL;
using ATM_Simulation_Demo.Extension;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.DTO;
using ATM_Simulation_Demo.Models.POCO;
using ATM_Simulation_Demo.Others.Security;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Principal;
using System.Web;
using static ATM_Simulation_Demo.BAL.BLHelper;

namespace ATM_Simulation_Demo.BAL.Services
{

    public class BLAccountService : IBLAccountService
    {
        #region Private Fields

        /// <summary>
        /// Represents an interface for the account repository used for handling account-related operations.
        /// </summary>
        private readonly IBLAccountRepository _AccountRepository;

        /// <summary>
        /// Represents an interface for the pin module used for managing PIN-related functionalities.
        /// </summary>
        private readonly IBLPinModule _pinModule;

        /// <summary>
        /// Represents a factory for creating database connections.
        /// </summary>
        private readonly IDbConnectionFactory dbFactory;


        /// <summary>
        /// Insatnce of <see cref="ACC01"/>.
        /// </summary>
        private ACC01 _objACC01;
        #endregion

        #region Public Properties

        public Response objResponse;
        /// <summary>
        /// Specifies the operation to perform.
        /// </summary>
        public enmOperation Type { get; set; }

        #endregion Public Properties

        #region Constructor

        public BLAccountService(IBLAccountRepository AccountRepository, IBLPinModule pinModule)
        {
            _AccountRepository = AccountRepository;
            _pinModule = pinModule;
            objResponse = new Response();
           // _connectionString = DateRepository.connectionString;
            dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checking the id exists or not for category.
        /// </summary>
        /// <param name="objDTOACC01">DTO for ACC01 Model.</param>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response PreValidation(DTOACC01 objDTOACC01)
        {
            if (!IsIDValid(Type, objDTOACC01.C01F01))
            {
                return PreConditionFailedResponse("Id id invalid for operation");
            }

            if (Type == enmOperation.E)
            {
                // Checks the category exists or not.
                if (_AccountRepository.IsUserExists(objDTOACC01.C01F01))
                {
                    return NotFoundResponse("account not found.");
                }
            }
            return OkResponse();
        }

        /// <summary>
        /// Prepares the objects for create or update operation.
        /// </summary>
        /// <param name="objACC01DTO">The DTO object representing the customer.</param>
        public void PreSave(DTOACC01 objACC01DTO)
        {
            _objACC01 = objACC01DTO.Convert<ACC01>();
            if (Type == enmOperation.A)
            {
                _objACC01.C01F02 = GenerateCardNumber();
                _objACC01.C01F04 = DOBToPin(_objACC01.C01X01);
            }
            _objACC01.C01F04 = BLCrypto.Encrypt(_objACC01.C01F04);
        }

        /// <summary>
        /// Validates customer information.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Validation()
        {
            //// validation on save ? delre
            if (Type == enmOperation.A)
            {
                if (_AccountRepository.IsCardNumberExists(_objACC01.C01F02))
                {
                    return PreConditionFailedResponse("Card Number already exists.");
                }
            }
            return OkResponse();
        }

        /// <summary>
        /// Saves changes according to the operation.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Save()
        {
            if (Type == enmOperation.A)
            {
                int accountId = _AccountRepository.AddAccount(_objACC01);
                if (accountId > 0)
                    return OkResponse("Account added successfully..!!", _objACC01);
                else
                    return PreConditionFailedResponse("There was an error while creating account");
            }

            int rowsAffected = _AccountRepository.UpdateAccount(_objACC01);
            if (rowsAffected > 0)
                return OkResponse("Account updated successfully..!!", _objACC01);
            else
                return PreConditionFailedResponse("There was an error while updating account");

        }

        /// <summary>
        /// Retrieves an account based on card number and PIN.
        /// </summary>
        /// <param name="cardNumber">The card number associated with the account.</param>
        /// <param name="pin">The PIN associated with the account.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public Response GetAccount(string cardNumber, string pin)
        {
            try
            {
                if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(pin))
                {
                    return PreConditionFailedResponse("Card number and PIN cannot be null or empty.");
                }
                else
                {
                    _objACC01 = _AccountRepository.GetAccount(cardNumber, BLCrypto.Encrypt(pin));
                    return OkResponse("Data fetched succesfully", _objACC01);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return PreConditionFailedResponse(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves an account based on its ID.
        /// </summary>
        /// <param name="Id">The ID of the account.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public Response GetAccountByID(int Id)
        {

            _objACC01 = _AccountRepository.GetAccountByID(Id);
            return _objACC01 != null ? OkResponse("Account info ", _objACC01) : PreConditionFailedResponse("Account not Found");

        }

        /// <summary>
        /// Changes the PIN associated with an account.
        /// </summary>
        /// <param name="Id">The ID of the account.</param>
        /// <param name="currentPin">The current PIN associated with the account.</param>
        /// <param name="newPin">The new PIN to be set for the account.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public Response ChangePin(int Id, string currentPin, string newPin)
        {
            string message = _pinModule.ChangePin(Id, BLCrypto.Encrypt(currentPin), BLCrypto.Encrypt(newPin));

            return message != "PIN changed successfully." ? PreConditionFailedResponse(message) : OkResponse(message);
        }

        /// <summary>
        /// Retrieves all accounts.
        /// </summary>
        /// <returns>A response containing a list of all accounts or an error message.</returns>
        public Response GetAllAccounts()
        {
            try
            {
                List<ACC01> lstACC01 = _AccountRepository.GetAllAccounts();

                return lstACC01 != null
                    ? OkResponse("Data fetched successfully..!!", lstACC01)
                    : PreConditionFailedResponse("There was some problem while fetching the data.");
            }
            catch (Exception ex)
            {
                return PreConditionFailedResponse(ex.Message);
            }
        }

        /// <summary>
        /// Deletes an account by its ID.
        /// </summary>
        /// <param name="Id">The ID of the account to be deleted.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public Response Delete(int Id)
        {
            try
            {
                int rowsDeleted = 0;
                if (Id > 0)
                {
                    using (IDbConnection db = dbFactory.Open())
                    {
                        rowsDeleted = db.DeleteById<ACC01>(Id);
                    }
                    return rowsDeleted > 0
                        ? OkResponse("Account Deleted successfully..!!")
                        : PreConditionFailedResponse("There was error while deleting accout");
                }
                return PreConditionFailedResponse("Invalid account ID");
            }
            catch (Exception ex)
            {
                return PreConditionFailedResponse(ex.Message);
            }
        }

        /// <summary>
        /// Updates a specific field of a user account with the provided value.
        /// </summary>
        /// <typeparam name="T">The type of the value to update.</typeparam>
        /// <param name="accountId">The ID of the account to update.</param>
        /// <param name="fieldName">The name of the field to update.</param>
        /// <param name="val">The new value to assign to the field.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        public bool UpdateSpecificField<T>(int accountId, string fieldName, T val)
        {
            // Open a connection to the database
            using (System.Data.IDbConnection db = dbFactory.Open())
            {
                // Retrieve the user account by its ID
                ACC01 user = db.SingleById<ACC01>(accountId);
                if (user != null)
                {
                    // Retrieve the property of the user object with the given field name
                    System.Reflection.PropertyInfo property = typeof(ACC01).GetProperty(fieldName);
                    if (property != null)
                    {
                        // Set the value of the property to the provided value
                        property.SetValue(user, val);
                        // Update the user object in the database
                        _ = db.Update(user);
                        // Return true to indicate successful update
                        return true;
                    }
                    else
                    {
                        // Property not found, return false
                        return false;
                    }
                }
                else
                {
                    // User not found, return false
                    return false;
                }
            }
        }

        #endregion

        #region Helper Methods
        /// <summary>
        /// Generates 4 digit string from Date of Birth
        /// </summary>
        /// <param name="dateTime">Date of Birth in format of DateTime</param>
        /// <returns></returns>
        private string DOBToPin(DateTime dateTime)
        {
            // Get day and month as two-digit strings
            string day = dateTime.Day.ToString("D2");
            string month = dateTime.Month.ToString("D2");

            // Concatenate and return DDMM
            return day + month;
        }

        /// <summary>
        /// Generates a unique card number.
        /// </summary>
        /// <returns>The generated card number.</returns>
        private string GenerateCardNumber()
        {
            // You can customize the prefix based on your needs
            string cardPrefix = "1234"; // Example prefix

            // Generate a unique identifier (you might want to use a more sophisticated approach)
            Random random = new Random();
            int uniqueIdentifier = random.Next(1000, 9999);
            int uniqueIdentifier2 = random.Next(1000, 9999);
            int uniqueIdentifier3 = random.Next(1000, 9999);

            // Concatenate the prefix and unique identifier to form the card number
            string cardNumber = $"{cardPrefix}-{uniqueIdentifier:D4}-{uniqueIdentifier2:D4}-{uniqueIdentifier3:D4}";
            return cardNumber;
        }

        public Response UpdateMobileNumber(string newMobileNumber)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}