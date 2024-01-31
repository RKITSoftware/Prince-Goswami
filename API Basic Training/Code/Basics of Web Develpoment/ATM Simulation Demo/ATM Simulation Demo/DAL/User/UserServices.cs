using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.DAL.Interface;
using ATM_Simulation_Demo.Models;

namespace ATM_Simulation_Demo.BAL
{
    /// <summary>
    /// Service class for managing user-related operations in the business logic layer.
    /// </summary>
    public class UserService : IBLUserService
    {
        private readonly IBLUserRepository _userRepo;
        private readonly IBLPinModule _pinModule;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepo">The user repository.</param>
        /// <param name="pinModule">The PIN module.</param>
        public UserService(IBLUserRepository userRepo, IBLPinModule pinModule)
        {
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _pinModule = pinModule ?? throw new ArgumentNullException(nameof(pinModule));
        }

        /// <inheritdoc/>
        public BLUserModel GetUserByID(int userId)
        {
            return _userRepo.GetUserByID(userId);
        }

        /// <inheritdoc/>
        public BLUserModel GetUser(string cardNumber, string pin)
        {
            return _userRepo.GetUser(cardNumber, pin);
        }

        /// <inheritdoc/>
        public BLUserModel CreateUser(string name, string mobileNumber, string dob)
        {
            // Logic to create a new user, generate PIN, etc.
            // You may use _pinModule to generate a PIN and then call _userRepo.CreateUser

            // For demonstration purposes, we'll just create a user with a fixed PIN.
            string pin = "1234"; // Replace this with actual PIN generation logic

            BLUserModel newUser = new BLUserModel
            {
                Name = name,
                MobileNumber = mobileNumber,
                DateOfBirth = dob,
                PIN = pin,
                // Set other properties as needed
            };

            _userRepo.CreateUser(newUser);
            return newUser;
        }

        /// <inheritdoc/>
        public void ChangePin(BLUserModel user, string currentPin, string newPin)
        {
            if (_pinModule.VerifyPin(user, currentPin))
            {
                _pinModule.ChangePin(user, currentPin, newPin);
                _userRepo.UpdateUser(user);
            }
            else
            {
                throw new InvalidOperationException("Invalid PIN. Unable to change PIN.");
            }
        }

        /// <inheritdoc/>
        public void UpdateMobileNumber(BLUserModel user, string newMobileNumber)
        {
            user.MobileNumber = newMobileNumber;
            _userRepo.UpdateUser(user);
        }

        /// <inheritdoc/>
        public List<BLUserModel> DisplayAllUsers()
        {
            return _userRepo.GetAllUsers();
        }
    }
}
.0