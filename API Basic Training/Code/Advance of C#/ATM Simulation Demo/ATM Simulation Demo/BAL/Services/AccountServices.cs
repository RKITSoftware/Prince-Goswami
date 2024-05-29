using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Extension;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.DTO;
using ATM_Simulation_Demo.Models.POCO;
using ATM_Simulation_Demo.Others.Auth.Account;
using ATM_Simulation_Demo.Others.Security;
using System;
using System.Collections.Generic;
using static ATM_Simulation_Demo.BAL.BLHelper;

namespace ATM_Simulation_Demo.BAL.Services
{

    public class AccountService : IBLAccountService
    {
        private readonly IBLAccountRepository _AccountRepository;
        private readonly IBLPinModule _pinModule;
        private readonly IBLLimitService _limitService;

        /// <summary>
        /// Insatnce of <see cref="ACC01"/>.
        /// </summary>
        private ACC01 _objACC01;

        #region Public Properties

        public Response objResponse;
        /// <summary>
        /// Specifies the operation to perform.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion Public Properties

        public AccountService(IBLAccountRepository AccountRepository, IBLPinModule pinModule)
        {
            _AccountRepository = AccountRepository;
            _pinModule = pinModule;
            _limitService = new LimitService();
            objResponse = new Response();
        }

        /// <summary>
        /// Checking the id exists or not for category.
        /// </summary>
        /// <param name="objDTOACC01">DTO for ACC01 Model.</param>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response PreValidation(DTO_ACC01 objDTOACC01)
        {
            if (!IsIDValid(Operation, objDTOACC01.C01F01))
            {
                return PreConditionFailedResponse("Id id invalid for operation");
            }

            if (Operation == EnmOperation.E)
            {
                // Checks the category exists or not.
                if (_AccountRepository.GetAccountByID(objDTOACC01.C01F01) == null)
                {
                    return NotFoundResponse("Category not found.");
                }
            }
            return OkResponse();
        }


        /// <summary>
        /// Prepares the objects for create or update operation.
        /// </summary>
        /// <param name="objACC01DTO">The DTO object representing the customer.</param>
        public void PreSave(DTO_ACC01 objACC01DTO)
        {

            if (Operation == EnmOperation.A)
            {
                objACC01DTO.C01F02 = GenerateCardNumber();
                objACC01DTO.C01F04 = BLCrypto.Encrypt(DOBToPin(objACC01DTO.C01F08));

            }
            _objACC01 = objACC01DTO.Convert<ACC01>();
        }

        /// <summary>
        /// Validates customer information.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Validation()
        {
            if (Operation == EnmOperation.A)
            {
                if (_AccountRepository.IsCardNumberExists(_objACC01.C01F02))
                    return PreConditionFailedResponse("Card Number already exists.");
            }

            return OkResponse();
        }

        /// <summary>
        /// Saves changes according to the operation.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Save()
        {
            if (Operation == EnmOperation.A)
            {
                _AccountRepository.AddAccount(_objACC01);
                return OkResponse("Account added successfully..!!", _objACC01);
            }

            _AccountRepository.UpdateAccount(_objACC01);

            return OkResponse("Account updated successfully..!!", _objACC01);
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
                    _objACC01 = _AccountRepository.GetAccount(cardNumber, pin);
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
            if (_AccountRepository.IsUserExists(Id))
            {
                _objACC01 = _AccountRepository.GetAccountByID(Id);
                if (_objACC01 != null)
                {
                    return OkResponse("Account info ", _objACC01);
                }
                else
                    return PreConditionFailedResponse("Account not Found");
            }
            else
                return PreConditionFailedResponse("Invalid ID");
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
            string message = _pinModule.ChangePin(Id, currentPin, newPin);

            if (message != "PIN changed successfully.")
                return PreConditionFailedResponse(message);

            return OkResponse(message);
        }

        /// <summary>
        /// Updates the mobile number associated with the current session's account.
        /// </summary>
        /// <param name="newMobileNumber">The new mobile number to be associated with the account.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public Response UpdateMobileNumber(string newMobileNumber)
        {
            try
            {
                if (_AccountRepository.UpdateMobileNumber(TokenManager.sessionId, newMobileNumber))
                    return OkResponse("Number updated successfully..!");
                else
                    return PreConditionFailedResponse("There was some problem updating MobileNumber..!!");
            }
            catch (Exception ex)
            {
                return PreConditionFailedResponse(ex.Message);
            }
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

                if (lstACC01 != null)
                    return OkResponse("Data fetched successfully..!!", lstACC01);
                else
                    return PreConditionFailedResponse("There was some problem while fetching the data.");
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
                if (Id > 0)
                {

                    if (_AccountRepository.Delete(Id))
                        return OkResponse("Account Deleted successfully..!!");
                    else
                        return PreConditionFailedResponse("There was error while deleting accout");
                }
                return PreConditionFailedResponse("Invalid account ID");
            }
            catch (Exception ex)
            {
                return PreConditionFailedResponse(ex.Message);
            }
        }


        #region helper methods
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

        #endregion
    }


}