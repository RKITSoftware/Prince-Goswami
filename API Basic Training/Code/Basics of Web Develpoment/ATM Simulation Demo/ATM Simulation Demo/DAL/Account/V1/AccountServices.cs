using ATM_Simulation_Demo.Models.V1;
using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.BAL.Interface.V1;
using ATM_Simulation_Demo.BAL.Interface;

namespace ATM_Simulation_Demo.DAL.Account.V1
{
    public class AccountService : IBLAccountService
    {
        private readonly IBLAccountRepository _AccountRepository;
        private readonly IBLPinModule _pinModule;

        public AccountService(IBLAccountRepository AccountRepository, IBLPinModule pinModule)
        {
            _AccountRepository = AccountRepository;
            _pinModule = pinModule;
        }

        public BLAccountModel CreateAccount(string name, string mobileNumber, DateTime DOB)
        {
            try
            {
                string PIN = DOBToPin(DOB);
                BLAccountModel newAccount = new BLAccountModel(name, PIN , mobileNumber);
                _AccountRepository.AddAccount(newAccount);
                return newAccount;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public BLAccountModel GetAccount(string cardNumber, string pin)
        {
            try
            {
                return _AccountRepository.GetAccount(cardNumber, pin);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        } 
        
        public BLAccountModel GetAccountByID(int Id)
        {
            try
            {
                return _AccountRepository.GetAccountByID(Id);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public void ChangePin(BLAccountModel Account, string currentPin, string newPin)
        {
            try
            {
                _pinModule.ChangePin(Account, currentPin, newPin);
                _AccountRepository.UpdateAccount(Account);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public void UpdateMobileNumber(BLAccountModel Account, string newMobileNumber)
        {
            try
            {
                Account.MobileNumber = newMobileNumber;
                _AccountRepository.UpdateAccount(Account);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public List<BLAccountModel> GetAllAccounts()
        {
            try
            {
                return _AccountRepository.GetAllAccounts();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

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