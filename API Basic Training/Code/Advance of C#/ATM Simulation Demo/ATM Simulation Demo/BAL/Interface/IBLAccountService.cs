using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.DTO;
using ATM_Simulation_Demo.Models.POCO;
using ServiceStack.OrmLite;

namespace ATM_Simulation_Demo.BAL.Interface
{
    /// <summary>
    /// Interface for account-related operations in the ATM simulation.
    /// </summary>
    public interface IBLAccountService : IDataHandlerService<DTOACC01>
    {
        Response PreValidation(DTOACC01 objDTOACC01);

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

        /// <summary>
        /// Updates a specific field of a user account with the provided value.
        /// </summary>
        /// <typeparam name="T">The type of the value to update.</typeparam>
        /// <param name="accountId">The ID of the account to update.</param>
        /// <param name="fieldName">The name of the field to update.</param>
        /// <param name="val">The new value to assign to the field.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        bool UpdateSpecificField<T>(int accountId, string fieldName, T val);

    }
}
