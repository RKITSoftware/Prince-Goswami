using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Models.V2;
using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.BAL.Interface.V2;

namespace ATM_Simulation_Demo.DAL.Account.V2
{
    /// <summary>
    /// Provides functionalities for managing accounts, such as creating accounts, retrieving account details,
    /// updating account information, and retrieving all accounts.
    /// </summary>
    public class AccountService : IBLAccountService
    {
        #region Private Fileds
        private readonly IBLAccountRepository _AccountRepository;
        private readonly IBLPinModule _pinModule;
        #endregion

        #region constructor
        public AccountService(IBLAccountRepository AccountRepository, IBLPinModule pinModule)
        {
            _AccountRepository = AccountRepository;
            _pinModule = pinModule;
        }
        #endregion

        #region methods
        /// <summary>
        /// Creates a new account with the specified name, mobile number, and date of birth.
        /// </summary>
        /// <param name="name">The name of the account holder.</param>
        /// <param name="mobileNumber">The mobile number of the account holder.</param>
        /// <param name="DOB">The date of birth of the account holder.</param>
        /// <returns>The newly created <see cref="AccountModel"/>.</returns>
        /// <exception cref="Exception">Thrown if an error occurs during the account creation process.</exception>
        public AccountModel CreateAccount(string name, string mobileNumber, DateTime DOB)
        {
            try
            {
                // Generate PIN from date of birth
                string PIN = DOBToPin(DOB);

                // Create a new account model
                AccountModel newAccount = new AccountModel(name, PIN, mobileNumber);

                // Add the new account to the repository
                _AccountRepository.AddAccount(newAccount);

                // Return the newly created account
                return newAccount;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }
      /// <summary>
        /// Retrieves the account associated with the specified card number and PIN.
        /// </summary>
        /// <param name="cardNumber">The card number associated with the account.</param>
        /// <param name="pin">The PIN associated with the account.</param>
        /// <returns>The <see cref="AccountModel"/> associated with the card number and PIN.</returns>
        /// <exception cref="Exception">Thrown if an error occurs during the retrieval process.</exception>
        public AccountModel GetAccount(string cardNumber, string pin)
        {
            try
            {
                return _AccountRepository.GetAccount(cardNumber, pin);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        /// <summary>
        /// Retrieves the account with the specified ID.
        /// </summary>
        /// <param name="Id">The ID of the account to retrieve.</param>
        /// <returns>The <see cref="AccountModel"/> with the specified ID.</returns>
        /// <exception cref="Exception">Thrown if an error occurs during the retrieval process.</exception>
        public AccountModel GetAccountByID(int Id)
        {
            try
            {
                return _AccountRepository.GetAccountByID(Id);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        /// <summary>
        /// Changes the PIN of the specified account.
        /// </summary>
        /// <param name="Account">The account whose PIN will be changed.</param>
        /// <param name="currentPin">The current PIN of the account.</param>
        /// <param name="newPin">The new PIN to be set.</param>
        /// <exception cref="Exception">Thrown if an error occurs during the PIN change process.</exception>
        public void ChangePin(AccountModel Account, string currentPin, string newPin)
        {
            try
            {
                _pinModule.ChangePin(Account, currentPin, newPin);
                _AccountRepository.UpdateAccount(Account);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        /// <summary>
        /// Updates the mobile number of the specified account.
        /// </summary>
        /// <param name="Account">The account whose mobile number will be updated.</param>
        /// <param name="newMobileNumber">The new mobile number to be set.</param>
        /// <exception cref="Exception">Thrown if an error occurs during the update process.</exception>
        public void UpdateMobileNumber(AccountModel Account, string newMobileNumber)
        {
            try
            {
                Account.MobileNumber = newMobileNumber;
                _AccountRepository.UpdateAccount(Account);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        /// <summary>
        /// Retrieves a list of all accounts.
        /// </summary>
        /// <returns>A list of <see cref="AccountModel"/> representing all accounts.</returns>
        /// <exception cref="Exception">Thrown if an error occurs during the retrieval process.</exception>
        public List<AccountModel> GetAllAccounts()
        {
            try
            {
                return _AccountRepository.GetAllAccounts();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }
        #endregion

        #region helper methods
        /// <summary>
        /// Generates 4 digit string from Date of Birth
        /// </summary>
        /// <param name="dateTime">Date of Birth in format of DateTime</param>
        /// <returns></returns>
        static string DOBToPin(DateTime dateTime)
        {
            // Get day and month as two-digit strings
            string day = dateTime.Day.ToString("D2");
            string month = dateTime.Month.ToString("D2");

            // Concatenate and return DDMM
            return day + month;
        }
        #endregion
    }


}