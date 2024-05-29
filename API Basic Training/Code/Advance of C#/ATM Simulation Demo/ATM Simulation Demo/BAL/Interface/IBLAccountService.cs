using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.DTO;
using ATM_Simulation_Demo.Models.POCO;

namespace ATM_Simulation_Demo.BAL.Interface
{
    /// <summary>
    /// Interface for account-related operations in the ATM simulation.
    /// </summary>
    public interface IBLAccountService
    {
        /// <summary>
        /// Checking the id exists or not for category.
        /// </summary>
        /// <param name="objDTOACC01">DTO for ACC01 Model.</param>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        Response PreValidation(DTO_ACC01 objDTOACC01);
        
        EnmOperation Operation { get; set; }

        Response Validation();
        void PreSave(DTO_ACC01 objACC01DTO);
        Response Save();

      
        /// <summary>
        /// Retrieves a account based on the provided card number and PIN.
        /// </summary>
        /// <param name="cardNumber">Card number associated with the account.</param>
        /// <param name="pin">PIN associated with the account.</param>
        /// <returns>The account model if found, null otherwise.</returns>
        Response GetAccount(string cardNumber, string pin);

        /// <summary>
        /// Retrieves a account based on the provided account ID.
        /// </summary>
        /// <param name="Id">Account ID.</param>
        /// <returns>The account model if found, null otherwise.</returns>
        Response GetAccountByID(int Id);

        /// <summary>
        /// Changes the PIN for the specified account.
        /// </summary>
        /// <param name="account">Account model for whom the PIN needs to be changed.</param>
        /// <param name="currentPin">Current PIN of the account.</param>
        /// <param name="newPin">New PIN to be set for the account.</param>
        Response ChangePin(int id, string currentPin, string newPin);

        /// <summary>
        /// Updates the mobile number for the specified account.
        /// </summary>
        /// <param name="account">Account model for whom the mobile number needs to be updated.</param>
        /// <param name="newMobileNumber">New mobile number to be set for the account.</param>
        Response UpdateMobileNumber(string newMobileNumber);

        /// <summary>
        /// Retrieves a list of all accounts.
        /// </summary>
        /// <returns>List of account models.</returns>
        Response GetAllAccounts();

        /// <summary>
        /// deletes a account based on the provided account ID.
        /// </summary>
        /// <param name="Id">Account ID.</param>
        Response Delete(int Id);

    }
}
