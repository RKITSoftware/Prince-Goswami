using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Others.Security;
using ATM_Simulation_Demo.Models;

namespace ATM_Simulation_Demo.BAL.Services
{
    /// <summary>
    /// Service class for managing user-related operations in the business logic layer.
    /// </summary>
    public class UserService : IBLUserService
    {
        private readonly IBLUserRepository _userRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepo">The user repository.</param>
        public UserService(IBLUserRepository userRepo)
        {
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }

        /// <inheritdoc/>
        public USR01 GetUserByID(int userId)
        {
            return _userRepo.GetUser(userId);
        }

        /// <inheritdoc/>
        public USR01 GetUserByCredentials(string userName, string password)
        {
            return _userRepo.GetUserByCredentials(userName, password);
        }

        /// <inheritdoc/>
        public USR01 CreateUser(string userName, string mobileNumber, string password, DateTime DOB, UserRole role)
        {
            // Create a new user and add to the database
            var newUser = new USR01
            {
                R01F02 = userName,
                R01F03 = mobileNumber,
                R01F06 = BLCrypto.Encrypt(password),
                R01F04 = DOB,
                R01F05 = role
            };

            return _userRepo.CreateUser(newUser);
        }

        /// <inheritdoc/>
        public void ChangePassword(USR01 user, string currentPassword, string newPassword)
        {
            if (BLCrypto.Decrypt(user.R01F06) == currentPassword) // Actual implementation may involve hashing and checking
            {
                user.R01F06 = BLCrypto.Encrypt(newPassword); // Update password (you may want to hash it)
                _userRepo.UpdateUser(user);
            }
            else
            {
                throw new InvalidOperationException("Invalid password. Unable to change password.");
            }
        }

        /// <inheritdoc/>
        public void UpdateRole(USR01 user, UserRole newRole)
        {
            user.R01F05 = newRole;
            _userRepo.UpdateUser(user);
        }

        /// <inheritdoc/>
        public void DeleteUser(int id)
        {
            _userRepo.DeleteUser(id);
        }

        /// <inheritdoc/>
        public List<USR01> GetAllUsers()
        {
            return _userRepo.GetAllUsers();
        }
    }
}
