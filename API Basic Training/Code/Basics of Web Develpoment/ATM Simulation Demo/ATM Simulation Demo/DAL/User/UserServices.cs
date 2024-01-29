using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.BAL;
using ATM_Simulation_Demo.Models;
using System;
using System.Collections.Generic;

namespace ATM_Simulation_Demo.DAL.User
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IBLPinModule _pinModule;

        public UserService(UserRepository userRepository, IBLPinModule pinModule)
        {
            _userRepository = userRepository;
            _pinModule = pinModule;
        }

        public BLUserModel CreateUser(string name, string mobileNumber, DateTime DOB)
        {
            try
            {
                string PIN = DOBToPin(DOB);
                BLUserModel newUser = new BLUserModel(name, PIN , mobileNumber);
                _userRepository.AddUser(newUser);
                return newUser;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public BLUserModel GetUser(string cardNumber, string pin)
        {
            try
            {
                return _userRepository.GetUser(cardNumber, pin);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        } 
        
        public BLUserModel GetUserByID(int id)
        {
            try
            {
                return _userRepository.GetUser(cardNumber, pin);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public void ChangePin(BLUserModel user, string currentPin, string newPin)
        {
            try
            {
                _pinModule.ChangePin(user, currentPin, newPin);
                _userRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public void UpdateMobileNumber(BLUserModel user, string newMobileNumber)
        {
            try
            {
                user.MobileNumber = newMobileNumber;
                _userRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public List<BLUserModel> GetAllUsers()
        {
            try
            {
                return _userRepository.GetAllUsers();
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