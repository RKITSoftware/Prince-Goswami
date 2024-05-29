
using System;
using System.Collections.Generic;
using System.Linq;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.BAL.Interface.V1;
using ATM_Simulation_Demo.Models.V1;

namespace ATM_Simulation_Demo.DAL.Account.V1
{

    /// <summary>
    /// Class for managing the account repository.
    /// </summary>
    public class AccountRepository : IBLAccountRepository
    {
        #region Fields

        /// <summary>
        /// Database to store accounts.
        /// </summary>
        private static List<AccountModel> _accountsDatabase = new List<AccountModel>();

        /// <summary>
        /// Module for PIN related operations.
        /// </summary>
        public readonly IBLPinModule _pinModule;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an instance of the AccountRepository.
        /// </summary>
        /// <param name="pinModule">The PIN module to use for account operations.</param>
        public AccountRepository(IBLPinModule pinModule)
        {
            this._pinModule = pinModule;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a new account to the repository.
        /// </summary>
        /// <param name="newAccount">The account to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when newAccount is null.</exception>
        public void AddAccount(AccountModel newAccount)
        {
            if (newAccount == null)
            {
                throw new ArgumentNullException(nameof(newAccount), "Account cannot be null.");
            }


            _accountsDatabase.Add(newAccount);
        }

        /// <summary>
        /// Updates an existing account's transaction history.
        /// </summary>
        /// <param name="account">The account with updated transaction history.</param>        
        public void UpdateAccount(AccountModel account)
        {
            _accountsDatabase.FirstOrDefault(u => u.CardNumber == account.CardNumber).TransactionHistory = account.TransactionHistory;
        }

        /// <summary>
        /// Retrieves an account by card number and PIN.
        /// </summary>
        /// <param name="cardNumber">The card number of the account to retrieve.</param>
        /// <param name="pin">The PIN associated with the account.</param>
        /// <returns>The account if found and PIN verification succeeds; otherwise, null.</returns>
        public AccountModel GetAccount(string cardNumber, string pin)
        {

            return _accountsDatabase.Find(u => u.CardNumber == cardNumber && _pinModule.VerifyPin(u, pin));
        }

        /// <summary>
        /// Retrieves an account by its unique identifier.
        /// </summary>
        /// <param name="Id">The unique identifier of the account to retrieve.</param>
        /// <returns>The account if found; otherwise, null.</returns>
        /// <inheritdoc/>
        public AccountModel GetAccountByID(int Id)
        {
            if (Id <= 0)
            {
                throw new ArgumentException("Invalid");
            }

            return _accountsDatabase.Find(a => a.Id == Id);
        }

        /// <summary>
        /// Checks if a card number already exists in the repository.
        /// </summary>
        /// <param name="cardNumber">The card number to check for existence.</param>
        /// <returns>True if the card number exists; otherwise, false.</returns>
        /// <inheritdoc/>
        public bool IsCardNumberExists(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                throw new ArgumentException("Card number cannot be null or empty.", nameof(cardNumber));
            }

            return _accountsDatabase.Exists(a => a.CardNumber == cardNumber);
        }

        /// <summary>
        /// Changes the PIN associated with an account.
        /// </summary>
        /// <param name="account">The account for which to change the PIN.</param>
        /// <param name="currentPin">The current PIN.</param>
        /// <param name="newPin">The new PIN.</param>
        /// <inheritdoc/>
        public void ChangePin(AccountModel account, string currentPin, string newPin)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");
            }

            if (string.IsNullOrEmpty(currentPin) || string.IsNullOrEmpty(newPin))
            {
                throw new ArgumentException("Current and new PIN cannot be null or empty.");
            }

            _pinModule.ChangePin(account, currentPin, newPin);
        }

        /// <summary>
        /// Updates the mobile number associated with an account.
        /// </summary>
        /// <param name="account">The account for which to update the mobile number.</param>
        /// <param name="newMobileNumber">The new mobile number.</param>
        /// <inheritdoc/>
        public void UpdateMobileNumber(AccountModel account, string newMobileNumber)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");
            }

            if (string.IsNullOrEmpty(newMobileNumber))
            {
                throw new ArgumentException("New mobile number cannot be null or empty.", nameof(newMobileNumber));
            }

            account.MobileNumber = newMobileNumber;
        }

        /// <summary>
        /// Retrieves all accounts stored in the repository.
        /// </summary>
        /// <returns>A list of all accounts.</returns>
        /// <inheritdoc/>
        public List<AccountModel> GetAllAccounts()
        {
            return _accountsDatabase;
        }

        #endregion

    }

}