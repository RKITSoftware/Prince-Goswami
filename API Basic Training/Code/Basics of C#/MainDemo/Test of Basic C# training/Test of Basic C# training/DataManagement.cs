using System;
using System.Collections.Generic;

namespace Test_of_Basic_C__training
{
    public class DatabaseManagement
    {
        private List<User> usersDatabase;
        private PinModule pinModule;

        public DatabaseManagement()
        {
            usersDatabase = new List<User>();
            pinModule = new PinModule();
        }

        // Add a new user to the database
        public void AddUser(User newUser)
        {
            if (!IsCardNumberExists(newUser.CardNumber))
            {
                usersDatabase.Add(newUser);
                Console.WriteLine("User added successfully.");
            }
            else
            {
                Console.WriteLine("User with the same card number already exists.");
            }
        }

        // Retrieve a user based on card number and PIN
        public User GetUser(string cardNumber, string pin)
        {
            return usersDatabase.Find(u => u.CardNumber == cardNumber && pinModule.VerifyPin(u, pin));
        }

        // Check if a user with the given card number exists in the database
        public bool IsCardNumberExists(string cardNumber)
        {
            return usersDatabase.Exists(u => u.CardNumber == cardNumber);
        }

        // Change PIN for a user
        public void ChangePin(User user, string currentPin, string newPin)
        {
            pinModule.ChangePin(user, currentPin, newPin);
        }

        // Update mobile number for a user
        public void UpdateMobileNumber(User user, string newMobileNumber)
        {
            user.MobileNumber = newMobileNumber;
            Console.WriteLine("Mobile number updated successfully.");
        }

        // Display all users (for testing purposes)
        public void DisplayAllUsers()
        {
            Console.WriteLine("===== All Users =====");
            foreach (User user in usersDatabase)
            {
                Console.WriteLine($"Card Number: {user.CardNumber}, PIN: {user.PIN}, Balance: {user.Balance}");
            }
        }
    }
}