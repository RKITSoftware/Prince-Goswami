using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM_Simulation_Demo.DAL.User
{
    using System;
    using System.Collections.Generic;
    using ATM_Simulation_Demo.BAL;
    using ATM_Simulation_Demo.BAL.Interface;
    using ATM_Simulation_Demo.Models;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class for managing the user repository.
    /// </summary>
    public class UserRepository : IBLUserRepository
    {
        private readonly List<BLUserModel> _usersDatabase;
        private readonly IBLPinModule _pinModule;
        private readonly ILogger<UserRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="pinModule">The PIN module.</param>
        /// <param name="logger">The logger.</param>
        public UserRepository(IBLPinModule pinModule, ILogger<UserRepository> logger)
        {
            _usersDatabase = new List<BLUserModel>();
            _pinModule = pinModule ?? throw new ArgumentNullException(nameof(pinModule));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public void AddUser(BLUserModel newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException(nameof(newUser), "User cannot be null.");
            }

            if (IsCardNumberExists(newUser.CardNumber))
            {
                throw new ArgumentException("User with the same card number already exists.", nameof(newUser));
            }

            _usersDatabase.Add(newUser);
            _logger.LogInformation("User added successfully.");
        }

        /// <inheritdoc/>
        public void UpdateUser(BLUserModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            if (!IsCardNumberExists(user.CardNumber))
            {
                throw new ArgumentException("User can not be Found.", nameof(user));
            }

            _usersDatabase.Find(u => u.CardNumber == user.CardNumber).TransactionHistory = user.TransactionHistory; 
            _logger.LogInformation("Transaction added successfully.");
        }

        /// <inheritdoc/>
        public BLUserModel GetUser(string cardNumber, string pin)
        {
            if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(pin))
            {
                throw new ArgumentException("Card number and PIN cannot be null or empty.");
            }

            return _usersDatabase.Find(u => u.CardNumber == cardNumber && _pinModule.VerifyPin(u, pin));
        }

        /// <inheritdoc/>
        public BLUserModel GetUserByID(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid");
            }

            return _usersDatabase.Find(u => u.id);
        }

        /// <inheritdoc/>
        public bool IsCardNumberExists(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                throw new ArgumentException("Card number cannot be null or empty.", nameof(cardNumber));
            }

            return _usersDatabase.Exists(u => u.CardNumber == cardNumber);
        }

        /// <inheritdoc/>
        public void ChangePin(BLUserModel user, string currentPin, string newPin)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            if (string.IsNullOrEmpty(currentPin) || string.IsNullOrEmpty(newPin))
            {
                throw new ArgumentException("Current and new PIN cannot be null or empty.");
            }

            _pinModule.ChangePin(user, currentPin, newPin);
        }

        /// <inheritdoc/>
        public void UpdateMobileNumber(BLUserModel user, string newMobileNumber)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            if (string.IsNullOrEmpty(newMobileNumber))
            {
                throw new ArgumentException("New mobile number cannot be null or empty.", nameof(newMobileNumber));
            }

            user.MobileNumber = newMobileNumber;
            _logger.LogInformation("Mobile number updated successfully.");
        }
     
        /// <inheritdoc/>
        public List<BLUserModel> GetAllUsers()
        {
            return _usersDatabase;
        }
    }

}