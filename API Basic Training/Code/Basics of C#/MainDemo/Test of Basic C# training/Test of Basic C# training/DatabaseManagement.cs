using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using Newtonsoft.Json;

namespace Test_of_Basic_C__training
{
    /// <summary>
    /// Manages the user database and related operations.
    /// </summary>
    public class DatabaseManagement
    {
        private List<User> usersDatabase;
        private PinModule pinModule;

        // File path to store user data
        private string filePath = "UserData.json";

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseManagement"/> class.
        /// </summary>
        public DatabaseManagement()
        {
            usersDatabase = LoadUserData(); // Load user data from file during initialization
            pinModule = new PinModule();
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="newUser">The new user to be added.</param>
        public void AddUser(User newUser)
        {
            if (!IsCardNumberExists(newUser.CardNumber))
            {
                usersDatabase.Add(newUser);
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
        public User GetUser(string cardNumber, string pin)
        {
            return usersDatabase.Find(u => u.CardNumber == cardNumber && pinModule.VerifyPin(u, pin));
        }

        /// <summary>
        /// Checks if a user with the given card number already exists.
        /// </summary>
        /// <param name="cardNumber">The card number to check.</param>
        /// <returns>True if the card number exists, otherwise false.</returns>
        public bool IsCardNumberExists(string cardNumber)
        {
            return usersDatabase.Exists(u => u.CardNumber == cardNumber);
        }

        /// <summary>
        /// Changes the PIN of a user.
        /// </summary>
        /// <param name="user">The user whose PIN needs to be changed.</param>
        /// <param name="currentPin">The current PIN of the user.</param>
        /// <param name="newPin">The new PIN to set.</param
        public void ChangePin(User user, string currentPin, string newPin)
        {
            if (pinModule.ChangePin(user, currentPin, newPin))
            {
                SaveUserData(); // Save user data to file after changing PIN only if the operation is successful
                Console.WriteLine("PIN changed successfully.");
            }
            else
            {
                Console.WriteLine("Failed to change PIN. Please check your current PIN.");
            }
        }
        /// <summary>
        /// Updates the mobile number of a user.
        /// </summary>
        /// <param name="user">The user whose mobile number needs to be updated.</param>
        /// <param name="newMobileNumber">The new mobile number to set.</param>
        public void UpdateMobileNumber(User user, string newMobileNumber)
        {
            user.MobileNumber = newMobileNumber;
            SaveUserData(); // Save user data to file after updating mobile number
            Console.WriteLine("Mobile number updated successfully.");
        }
        /// <summary>
        /// Displays information about all users in the database.
        /// </summary>
        public void DisplayAllUsers()
        {
            Console.WriteLine("===== All Users =====");
            foreach (User user in usersDatabase)
            {
                Console.WriteLine($"Card Number: {user.CardNumber}, PIN: {user.PIN}, Balance: {user.Balance}");
            }
        }

        // Save user data to file using JSON serialization
        private void SaveUserData()
        {
            string jsonData = JsonConvert.SerializeObject(usersDatabase, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }

        // Load user data from file using JSON deserialization
        private List<User> LoadUserData()
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                List<User> loadedData = JsonConvert.DeserializeObject<List<User>>(jsonData) ?? new List<User>();
                //Console.WriteLine("User data loaded successfully."); // You can comment out or remove this line
                return loadedData;
            }
            else
            {
                return new List<User>();
            }
        }

        /// <summary>
        /// Updates user data in the database.
        /// </summary>
        /// <param name="updatedUser">The updated user data.</param>
        public void UpdateUserData(User updatedUser)
        {
            int index = usersDatabase.FindIndex(u => u.CardNumber == updatedUser.CardNumber);

            if (index != -1)
            {
                usersDatabase[index] = updatedUser;
                SaveUserData(); // Save user data to file after updating user
                Console.WriteLine("User data updated successfully.");
            }
            else
            {
                Console.WriteLine("User not found. Unable to update user data.");
            }
        }
    }
}
