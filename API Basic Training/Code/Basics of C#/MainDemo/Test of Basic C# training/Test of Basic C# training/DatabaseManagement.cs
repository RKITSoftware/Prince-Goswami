using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Test_of_Basic_C__training
{
    /// <summary>
    /// Manages the user database and related operations.
    /// </summary>
    public class DatabaseManagement
    {
        #region Private Fields
        private List<UserModel> _usersDatabase;
        private PinModule _pinModule;

        /// <summary>
        /// File path to store user data
        /// </summary>
        private const string filePath = "UserData.json";
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseManagement"/> class.
        /// </summary>
        public DatabaseManagement()
        {
            _usersDatabase = LoadUserData(); // Load user data from file during initialization
            _pinModule = new PinModule();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="newUser">The new user to be added.</param>
        public void AddUser(UserModel newUser)
        {
            if (!IsCardNumberExists(newUser.CardNumber))
            {
                _usersDatabase.Add(newUser);
                SaveUserData(); // Save user data to file after adding a new user

                Console.WriteLine("Your account has been created.\n" +
            "Your card number is " + newUser.CardNumber +
            "\nYour card PIN is " + newUser.PIN);
                Console.WriteLine("User added successfully.");

            }
            else
            {
                Console.WriteLine("User with the same card number already exists.");
            }
        }

        /// <summary>
        /// Retrieves a user based on card number and PIN.
        /// </summary>
        /// <param name="cardNumber">The card number of the user.</param>
        /// <param name="pin">The PIN of the user.</param>
        /// <returns>The user object if found, otherwise null.</returns>
        public UserModel GetUser(string cardNumber, string pin)
        {
            return _usersDatabase.Find(u => u.CardNumber == cardNumber && _pinModule.VerifyPin(u, pin));
        }

        /// <summary>
        /// Checks if a user with the given card number already exists.
        /// </summary>
        /// <param name="cardNumber">The card number to check.</param>
        /// <returns>True if the card number exists, otherwise false.</returns>
        public bool IsCardNumberExists(string cardNumber)
        {
            return _usersDatabase.Exists(u => u.CardNumber == cardNumber);
        }

        /// <summary>
        /// Changes the PIN of a user.
        /// </summary>
        /// <param name="user">The user whose PIN needs to be changed.</param>
        /// <param name="currentPin">The current PIN of the user.</param>
        /// <param name="newPin">The new PIN to set.</param
        public void ChangePin(UserModel user, string currentPin, string newPin)
        {
            user = _pinModule.ChangePin(user, currentPin, newPin);
            UpdateUserData(user); // Save user data to file after changing PIN only if the operation is successful
            Console.WriteLine("Pin changed successfully");
        }
        /// <summary>
        /// Updates the mobile number of a user.
        /// </summary>
        /// <param name="user">The user whose mobile number needs to be updated.</param>
        /// <param name="newMobileNumber">The new mobile number to set.</param>
        public void UpdateMobileNumber(UserModel user, string newMobileNumber)
        {
            user.MobileNumber = newMobileNumber;
            UpdateUserData(user); // Update user data to file after updating mobile number
            Console.WriteLine("Mobile number updated successfully.");
        }
        /// <summary>
        /// Displays information about all users in the database.
        /// </summary>
        public void DisplayAllUsers()
        {
            Console.WriteLine("===== All Users =====");
            foreach (UserModel user in _usersDatabase)
            {
                Console.WriteLine($"Card Number: {user.CardNumber}, PIN: {user.PIN}, Balance: {user.Balance}");
            }
        }

        /// <summary>
        /// Updates user data in the database.
        /// </summary>
        /// <param name="updatedUser">The updated user data.</param>
        public void UpdateUserData(UserModel updatedUser)
        {
            int index = _usersDatabase.FindIndex(u => u.CardNumber == updatedUser.CardNumber);

            if (index != -1)
            {
                _usersDatabase[index] = updatedUser;
                SaveUserData(); // Save user data to file after updating user
                Console.WriteLine("User data updated successfully.");
            }
            else
            {
                Console.WriteLine("User not found. Unable to update user data.");
            }
        }

        #endregion

        #region Private Methods
        /// <summary> 
        /// Save user data to file using JSON serialization
        /// </summary>
        private void SaveUserData()
        {
            string jsonData = JsonConvert.SerializeObject(_usersDatabase, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }

        /// <summary>
        /// Load user data from file using JSON deserialization
        /// </summary>
        /// <returns>List of Users data from file. </returns>
        private List<UserModel> LoadUserData()
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                List<UserModel> loadedData = JsonConvert.DeserializeObject<List<UserModel>>(jsonData) ?? new List<UserModel>();
                //Console.WriteLine("User data loaded successfully."); // You can comment out or remove this line
                return loadedData;
            }
            else
            {
                return new List<UserModel>();
            }
        }
        #endregion

    }
}
