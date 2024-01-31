using System.Collections.Generic;
using System.Linq;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Models;

namespace ATM_Simulation_Demo.DAL.User
{
    public class UserRepository : IBLUserRepository
    {
        private readonly List<BLUserModel> _usersDatabase;

        public UserRepository()
        {
            // Initialize or load user data
            _usersDatabase = new List<BLUserModel>
            {
                new BLUserModel { UserId = 1, UserName = "User1", Password = "password1", Role = UserRole.User },
                new BLUserModel { UserId = 2, UserName = "User2", Password = "password2", Role = UserRole.Admin },
                // Add more users as needed
            };
        }

        public BLUserModel CreateUser(string userName, string password, UserRole role)
        {
            // Create a new user and add to the database
            var newUser = new BLUserModel
            {
                UserId = _usersDatabase.Count + 1,
                UserName = userName,
                Password = password,
                Role = role
            };

            _usersDatabase.Add(newUser);

            return newUser;
        }

        public BLUserModel GetUser(int userId)
        {
            // Retrieve user by userId
            return _usersDatabase.FirstOrDefault(user => user.UserId == userId);
        }

        public List<BLUserModel> GetAllUsers()
        {
            // Retrieve all users
            return _usersDatabase.ToList();
        }

        public bool VerifyUserCredentials(string userName, string password)
        {
            // Verify user credentials
            return _usersDatabase.Any(user => user.UserName == userName && user.Password == password);
        }

        public void ChangeRole(BLUserModel user, UserRole newRole)
        {
            // Change user role
            user.Role = newRole;
        }

        public BLUserModel GetUserByUserName(string userName)
        {
            // Retrieve user by userName
            return _usersDatabase.FirstOrDefault(user => user.UserName == userName);
        }

        public bool ChangePassword(BLUserModel user, string currentPassword, string newPassword)
        {
            // Change user password
            if (user.Password == currentPassword)
            {
                user.Password = newPassword;
                return true;
            }

            return false;
        }

        public void DeleteUser(int userId)
        {
            // Delete user by userId
            var user = _usersDatabase.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                _usersDatabase.Remove(user);
            }
        }
    }
}
