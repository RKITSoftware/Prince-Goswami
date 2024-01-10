using System;
using System.Collections.Generic;

namespace Test_of_Basic_C__training
{
    /// <summary>
    /// Class for managing the user database.
    /// </summary>
    public class DatabaseManagement
    {
        #region Fields

        private List<User> usersDatabase; // List to store users
        private PinModule pinModule; // Instance of PinModule class

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseManagement"/> class.
        /// </summary>
        public DatabaseManagement()
        {
            usersDatabase = new List<User>(); // Initialize the usersDatabase list
            pinModule = new PinModule(); // Initialize the pinModule
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="newUser">The new user to add.</param>
        public void AddUser(User newUser)
        {
            if (!IsCardNumberExists(newUser.CardNumber)) // Check if user with the same card number already exists
            {
                usersDatabase.Add(newUser); // Add the new user to the usersDatabase list
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
        /// <returns>The user object if found, null otherwise.</returns>
        public User GetUser(string cardNumber, string pin)
        {
            return usersDatabase.Find(u => u.CardNumber == cardNumber && pinModule.VerifyPin(u, pin));
        }

        /// <summary>
        /// Checks if a user with the given card number exists in the database.
        /// </summary>
        /// <param name="cardNumber">The card number to check.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        public bool IsCardNumberExists(string cardNumber)
        {
            return usersDatabase.Exists(u => u.CardNumber == cardNumber);
        }

        /// <summary>
        /// Changes the PIN for a user.
        /// </summary>
        /// <param name="user">The user to change the PIN for.</param>
        /// <param name="currentPin">The current PIN of the user.</param>
        /// <param name="newPin">The new PIN to set.</param>
        public void ChangePin(User user, string currentPin, string newPin)
        {
            pinModule.ChangePin(user, currentPin, newPin);
        }

        /// <summary>
        /// Updates the mobile number for a user.
        /// </summary>
        /// <param name="user">The user to update the mobile number for.</param>
        /// <param name="newMobileNumber">The new mobile number to set.</param>
        public void UpdateMobileNumber(User user, string newMobileNumber)
        {
            user.MobileNumber = newMobileNumber; // Update the mobile number of the user
            Console.WriteLine("Mobile number updated successfully.");
        }

        /// <summary>
        /// Displays all users in the database (for testing purposes).
        /// </summary>
        public void DisplayAllUsers()
        {
            Console.WriteLine("===== All Users =====");
            foreach (User user in usersDatabase)
            {
                Console.WriteLine($"Card Number: {user.CardNumber}, PIN: {user.PIN}, Balance: {user.Balance}");
            }
        }

        #endregion
    }
}