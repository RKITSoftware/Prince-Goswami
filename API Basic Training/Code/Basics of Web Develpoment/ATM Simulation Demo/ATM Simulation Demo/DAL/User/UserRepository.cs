using System.Collections.Generic;
using System.Linq;
using System.Windows;
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
                new BLUserModel { UserId = 1, UserName = "User1", Password = "password", Role = UserRole.User },
                new BLUserModel { UserId = 2, UserName = "User2", Password = "password", Role = UserRole.Admin },
                new BLUserModel { UserId = 3, UserName = "User3", Password = "password", Role = UserRole.DEO},
                new BLUserModel { UserId = 4, UserName = "User4", Password = "password", Role = UserRole.Customer},
                // Add more users as needed
            };
        }

        public BLUserModel CreateUser(BLUserModel newUser)
        {
            newUser.UserId = _usersDatabase.Count + 1;
            _usersDatabase.Add(newUser);

            return newUser;
        }

        public BLUserModel GetUser(int userId)
        {
            // Retrieve user by userId
            return _usersDatabase.FirstOrDefault(user => user.UserId == userId);
        }

        public BLUserModel UpdateUser(BLUserModel updatedUser)
        {
            BLUserModel user = _usersDatabase.FirstOrDefault(u => u.UserId == updatedUser.UserId);
            user = updatedUser;
            return user;
        }

        public BLUserModel GetUserByCredentials(string userName, string password)
        {
            // Retrieve user by userId
            return _usersDatabase.FirstOrDefault(user => user.UserName == userName && user.Password == password);
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
