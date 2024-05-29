using ATM_Simulation_Demo.BAL.Interface;
using System.Collections.Generic;
using System.Linq;

namespace ATM_Simulation_Demo.DAL.User
{
    /// <summary>
    /// Represents a repository for managing user data.
    /// </summary>
    public class UserRepository : IBLUserRepository
    {
        #region Fields

        // Database to store user data
        private readonly List<UserModel> _usersDatabase;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the UserRepository class.
        /// </summary>
        public UserRepository()
        {
            // Initialize or load user data
            _usersDatabase = new List<UserModel>
            {
                // Sample user data, can be replaced with actual data
                new UserModel { UserId = 1, UserName = "User1", Password = "password", Role = UserRole.User },
                new UserModel { UserId = 2, UserName = "User2", Password = "password", Role = UserRole.Admin },
                new UserModel { UserId = 3, UserName = "User3", Password = "password", Role = UserRole.DEO },
                new UserModel { UserId = 4, UserName = "User4", Password = "password", Role = UserRole.Customer },
                // Add more users as needed
            };
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new user and adds it to the database.
        /// </summary>
        /// <param name="newUser">The user to create.</param>
        /// <returns>The created user.</returns>
        /// <inheritdoc/>
        public UserModel CreateUser(UserModel newUser)
        {
            // Assign a unique UserId and add the user to the database
            newUser.UserId = _usersDatabase.Count + 1;
            _usersDatabase.Add(newUser);

            return newUser;
        }

        /// <summary>
        /// Retrieves a user by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user if found; otherwise, a new UserModel object.</returns>
        /// <inheritdoc/>
        public UserModel GetUser(int userId)
        {
            UserModel obj = new UserModel();

            // Retrieve user by userId if it's greater than 0
            if (userId > 0)
            {
                obj = _usersDatabase.FirstOrDefault(user => user.UserId == userId);
            }

            return obj;
        }

        /// <summary>
        /// Updates an existing user in the database.
        /// </summary>
        /// <param name="updatedUser">The updated user information.</param>
        /// <returns>The updated user.</returns>
        /// <inheritdoc/>
        public UserModel UpdateUser(UserModel updatedUser)
        {
            // Find the user by UserId and update the user information
            UserModel user = _usersDatabase.FirstOrDefault(u => u.UserId == updatedUser.UserId);
            user = updatedUser;
            return user;
        }

        /// <summary>
        /// Retrieves a user by their credentials (username and password).
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The user if found; otherwise, null.</returns>
        /// <inheritdoc/>
        public UserModel GetUserByCredentials(string userName, string password)
        {
            // Retrieve user by username and password
            return _usersDatabase.FirstOrDefault(user => user.UserName == userName && user.Password == password);
        }

        ///<inheritdoc/>
        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A list containing all users.</returns>
        public List<UserModel> GetAllUsers()
        {
            // Retrieve all users
            return _usersDatabase.ToList();
        }

        ///<inheritdoc/>
        /// <summary>
        /// Verifies the credentials of a user.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>True if the user credentials are valid; otherwise, false.</returns>
        public bool VerifyUserCredentials(string userName, string password)
        {
            // Verify user credentials
            return _usersDatabase.Any(user => user.UserName == userName && user.Password == password);
        }

        ///<inheritdoc/>
        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>True if the user was successfully deleted; otherwise, false.</returns>
        public bool DeleteUser(int userId)
        {
            // Find the index of the user with the given userId
            int index = _usersDatabase.FindIndex(user => user.UserId == userId);

            // Check if the user exists in the database
            if (index >= 0)
            {
                // Remove the user at the found index
                _usersDatabase.RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        /// <summary>
        /// Updates a single field of a user in the database.
        /// </summary>
        /// <param name="userId">The ID of the user to update.</param>
        /// <param name="field">The name of the field to update.</param>
        /// <param name="value">The new value of the field.</param>
        /// <returns>The updated user.</returns>
        public UserModel SingleFieldUpdate(int userId, string field, dynamic value)
        {
            int index = _usersDatabase.FindIndex(u => u.UserId == userId);

            if (index >= 0)
            {
                // Get the PropertyInfo for the specified field using reflection
                var property = typeof(UserModel).GetProperty(field);

                // Check if the property exists
                if (property != null)
                {
                    // Set the value of the specified property dynamically
                    property.SetValue(_usersDatabase[index], value);
                }
                else
                {
                    return null;
                }
            }
            return _usersDatabase[index];
        }
        #endregion
    }
}
