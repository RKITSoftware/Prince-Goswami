using ATM_Simulation_Demo.BAL.Interface.V1;
using ATM_Simulation_Demo.Models.V1;
using System;
using System.Collections.Generic;

namespace ATM_Simulation_Demo.DAL.Account.V1
{
    public class AccountService : IBLAccountService
    {
        #region private fields
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
        /// <inheritdoc/>
        public AccountModel CreateAccount(string name, string mobileNumber, DateTime DOB)
        {
            try
            {
                string PIN = DOBToPin(DOB);
                AccountModel newAccount = new AccountModel(name, PIN, mobileNumber);
                _AccountRepository.AddAccount(newAccount);
                return newAccount;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        /// <inheritdoc/>
        public AccountModel GetAccount(string cardNumber, string pin)
        {
            try
            {
                if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(pin))
                {
                    throw new ArgumentException("Card number and PIN cannot be null or empty.");
                }

                return _AccountRepository.GetAccount(cardNumber, pin);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        /// <inheritdoc/>
        public AccountModel GetAccountByID(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    throw new ArgumentException("Invalid ID.");
                }
                return _AccountRepository.GetAccountByID(Id);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void UpdateMobileNumber(AccountModel Account, string newMobileNumber)
        {
            try
            {
                if (Account == null)
                {
                    throw new ArgumentNullException(nameof(Account), "Account cannot be null.");
                }

                if (!_AccountRepository.IsCardNumberExists(Account.CardNumber))
                {
                    throw new ArgumentException("Account can not be Found.", nameof(Account));
                }

                Account.MobileNumber = newMobileNumber;
                _AccountRepository.UpdateAccount(Account);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        /// <inheritdoc/>
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