using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.BAL.Interface;

namespace ATM_Simulation_Demo.BAL
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
        public BLUserModel GetUserByID(int userId)
        {
            return _userRepo.GetUser(userId);
        }

        /// <inheritdoc/>
        public BLUserModel GetUserByCredentials(string userName, string password)
        {
            return _userRepo.GetUserByCredentials(userName, password);
        }

        /// <inheritdoc/>
        public BLUserModel CreateUser(string userName, string mobileNumber,string password,DateTime DOB, UserRole role)
        {
            // Create a new user and add to the database
            var newUser = new BLUserModel
            {
                UserName = userName,
                MobileNumber = mobileNumber,
                Password = password,
                DateOfBirth = DOB,
                Role = role
            };

            return _userRepo.CreateUser(newUser);
        }

        /// <inheritdoc/>
        public void ChangePassword(BLUserModel user, string currentPassword, string newPassword)
        {
            if (user.Password == currentPassword) // Actual implementation may involve hashing and checking
            {
                user.Password = newPassword; // Update password (you may want to hash it)
                _userRepo.UpdateUser(user);
            }
            else
            {
                throw new InvalidOperationException("Invalid password. Unable to change password.");
            }
        }

        /// <inheritdoc/>
        public void UpdateRole(BLUserModel user, UserRole newRole)
        {
            user.Role = newRole;
            _userRepo.UpdateUser(user);
        }

        /// <inheritdoc/>
        public void DeleteUser(int id)
        {
            _userRepo.DeleteUser(id);
        }
        /// <inheritdoc/>
        public List<BLUserModel> GetAllUsers()
        {
            return _userRepo.GetAllUsers();
        }
    }
}
