using ATM_Simulation_Demo.Models;
using System.Collections.Generic;

namespace ATM_Simulation_Demo.BAL.Interface
{

    /// <summary>
    /// Interface for managing the account repository.
    /// </summary>
    public interface IBLAccountRepository
    {
        /// <summary>
        /// Adds a new account to the repository.
        /// </summary>
        /// <param name="newAccount">The new account to add.</param>
        void AddAccount(ACC01 newAccount);

        /// <summary>
        /// Update current account with new account to the repository.
        /// </summary>
        /// <param name="newAccount">The new account to add.</param>
        void UpdateAccount(ACC01 newAccount);

        /// <summary>
        /// Retrieves a account based on card number and PIN.
        /// </summary>
        /// <param name="cardNumber">The card number of the account.</param>
        /// <param name="pin">The PIN of the account.</param>
        /// <returns>The account object if found, null otherwise.</returns>
        ACC01 GetAccount(string cardNumber, string pin);

        /// <summary>
        /// Retrieves a account based on card number and PIN.
        /// </summary>
        /// <param name="Id">The Id of the account.</param>
        /// <returns>The account object if found, null otherwise.</returns>
        ACC01 GetAccountByID(int Id);
        /// <summary>
        /// Checks if a account with the given card number exists in the repository.
        /// </summary>
        /// <param name="cardNumber">The card number to check.</param>
        /// <returns>True if the account exists, false otherwise.</returns>
        bool IsCardNumberExists(string cardNumber);

        /// <summary>
        /// Changes the PIN for a account.
        /// </summary>
        /// <param name="account">The account to change the PIN for.</param>
        /// <param name="currentPin">The current PIN of the account.</param>
        /// <param name="newPin">The new PIN to set.</param>
        void ChangePin(ACC01 account, string currentPin, string newPin);

        /// <summary>
        /// Updates the mobile number for a account.
        /// </summary>
        /// <param name="account">The account to update the mobile number for.</param>
        /// <param name="newMobileNumber">The new mobile number to set.</param>
        void UpdateMobileNumber(ACC01 account, string newMobileNumber);

        /// <summary>
        /// Retrieves a list of all accounts in the repository.
        /// </summary>
        /// <returns>A list of all accounts.</returns>
        List<ACC01> GetAllAccounts();

        /// <summary>
        /// Retrieves a account based on card number and PIN.
        /// </summary>
        /// <param name="Id">The Id of the account.</param>
        /// <returns>The account object if found, null otherwise.</returns>
        void Delete(int Id);
    }

}