using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.Models.V2;

namespace ATM_Simulation_Demo.BAL.Interface.V2
{
    /// <summary>
    /// Interface for account-related operations in the ATM simulation.
    /// </summary>
    public interface IBLAccountService
    {
        /// <summary>
        /// Creates a new account with the provided details and returns the account model.
        /// </summary>
        /// <param name="name">Name of the account.</param>
        /// <param name="mobileNumber">Mobile number of the account.</param>
        /// <param name="DOB">Date of Birth of the account.</param>
        /// <returns>The created account model.</returns>
        AccountModel CreateAccount(string name, string mobileNumber, DateTime DOB);

        /// <summary>
        /// Retrieves a account based on the provided card number and PIN.
        /// </summary>
        /// <param name="cardNumber">Card number associated with the account.</param>
        /// <param name="pin">PIN associated with the account.</param>
        /// <returns>The account model if found, null otherwise.</returns>
        AccountModel GetAccount(string cardNumber, string pin);

        /// <summary>
        /// Retrieves a account based on the provided account ID.
        /// </summary>
        /// <param name="Id">Account ID.</param>
        /// <returns>The account model if found, null otherwise.</returns>
        AccountModel GetAccountByID(int Id);

        /// <summary>
        /// Changes the PIN for the specified account.
        /// </summary>
        /// <param name="account">Account model for whom the PIN needs to be changed.</param>
        /// <param name="currentPin">Current PIN of the account.</param>
        /// <param name="newPin">New PIN to be set for the account.</param>
        void ChangePin(AccountModel account, string currentPin, string newPin);

        /// <summary>
        /// Updates the mobile number for the specified account.
        /// </summary>
        /// <param name="account">Account model for whom the mobile number needs to be updated.</param>
        /// <param name="newMobileNumber">New mobile number to be set for the account.</param>
        void UpdateMobileNumber(AccountModel account, string newMobileNumber);

        /// <summary>
        /// Retrieves a list of all accounts.
        /// </summary>
        /// <returns>List of account models.</returns>
        List<AccountModel> GetAllAccounts();
    }
}
