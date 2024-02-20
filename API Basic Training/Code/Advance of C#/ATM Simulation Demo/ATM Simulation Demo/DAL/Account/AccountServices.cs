using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Models;
using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.BAL.Interface;

namespace ATM_Simulation_Demo.DAL.Account
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

        public void CreateAccount(string name, string mobileNumber, DateTime DOB)
        {
            try
            {
                string PIN = DOBToPin(DOB);
                ACC01 newAccount = new ACC01(name, PIN , mobileNumber);
                _AccountRepository.AddAccount(newAccount);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public ACC01 GetAccount(string cardNumber, string pin)
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
        
        public ACC01 GetAccountByID(int Id)
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

        public void ChangePin(ACC01 Account, string currentPin, string newPin)
        {
            try
            {
                _pinModule.ChangePin(Account, currentPin, newPin);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public void UpdateMobileNumber(ACC01 Account, string newMobileNumber)
        {
            try
            {
                Account.C01F05 = newMobileNumber;
                _AccountRepository.UpdateAccount(Account);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public List<ACC01> GetAllAccounts()
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


        public void Delete(int Id)
        {
            try
            {
                _AccountRepository.Delete(Id);
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