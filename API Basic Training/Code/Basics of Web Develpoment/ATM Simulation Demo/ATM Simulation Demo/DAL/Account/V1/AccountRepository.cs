
using System;
using System.Collections.Generic;
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
        private static List<BLAccountModel> _accountsDatabase =  new List<BLAccountModel>();
        public readonly IBLPinModule _pinModule;
        //private readonly ILogger<AccountRepository> _logger;
        //private IBLPinModule pinModule;
        //private Action<string> writeLine;

        ///// <summary>
        ///// Initializes a new instance of the <see cref="AccountRepository"/> class.
        ///// </summary>
        ///// <param name="pinModule">The PIN module.</param>
        ///// <param name="logger">The logger.</param>
        //public AccountRepository(IBLPinModule pinModule, Logger<AccountRepository> logger)
        //{
        //    _accountsDatabase = new List<BLAccountModel>();
        //    _pinModule = pinModule ?? throw new ArgumentNullException(nameof(pinModule));
        //    _logger = new ILogger<>();
        //}

        public AccountRepository(IBLPinModule pinModule)
        {
            this._pinModule = pinModule;
            //_logger = new Logger<AccountRepository>();
        }

        /// <inheritdoc/>
        public void AddAccount(BLAccountModel newAccount)
        {
            if (newAccount == null)
            {
                throw new ArgumentNullException(nameof(newAccount), "Account cannot be null.");
            }

            //if (IsCardNumberExists(newAccount.CardNumber))
            //{
            //    throw new ArgumentException("Account with the same card number already exists.", nameof(newAccount));
            //}

            _accountsDatabase.Add(newAccount);
            //_logger.LogInformation("Account added successfully.");
        }

        /// <inheritdoc/>
        public void UpdateAccount(BLAccountModel account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");
            }

            if (!IsCardNumberExists(account.CardNumber))
            {
                throw new ArgumentException("Account can not be Found.", nameof(account));
            }

            _accountsDatabase.Find(u => u.CardNumber == account.CardNumber).TransactionHistory = account.TransactionHistory; 
            //_logger.LogInformation("Transaction added successfully.");
        }

        /// <inheritdoc/>
        public BLAccountModel GetAccount(string cardNumber, string pin)
        {
            if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(pin))
            {
                throw new ArgumentException("Card number and PIN cannot be null or empty.");
            }

            return _accountsDatabase.Find(u => u.CardNumber == cardNumber && _pinModule.VerifyPin(u, pin));
        }

        /// <inheritdoc/>
        public BLAccountModel GetAccountByID(int Id)
        {
            if (Id <= 0)
            {
                throw new ArgumentException("Invalid");
            }

            return _accountsDatabase.Find(a => a.Id == Id);
        }

        /// <inheritdoc/>
        public bool IsCardNumberExists(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                throw new ArgumentException("Card number cannot be null or empty.", nameof(cardNumber));
            }

            return _accountsDatabase.Exists(a => a.CardNumber == cardNumber);
        }

        /// <inheritdoc/>
        public void ChangePin(BLAccountModel account, string currentPin, string newPin)
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

        /// <inheritdoc/>
        public void UpdateMobileNumber(BLAccountModel account, string newMobileNumber)
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
            //_logger.LogInformation("Mobile number updated successfully.");
        }
     
        /// <inheritdoc/>
        public List<BLAccountModel> GetAllAccounts()
        {
            return _accountsDatabase;
        }
    }

}