using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using Newtonsoft.Json;

namespace Test_of_Basic_C__training
{
    public class DatabaseManagement
    {
        private List<User> usersDatabase;
        private PinModule pinModule;

        // File path to store user data
        private string filePath = "UserData.json";

        public DatabaseManagement()
        {
            usersDatabase = LoadUserData(); // Load user data from file during initialization
            pinModule = new PinModule();
        }

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


        public User GetUser(string cardNumber, string pin)
        {
            return usersDatabase.Find(u => u.CardNumber == cardNumber && pinModule.VerifyPin(u, pin));
        }

        public bool IsCardNumberExists(string cardNumber)
        {
            return usersDatabase.Exists(u => u.CardNumber == cardNumber);
        }

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

        public void UpdateMobileNumber(User user, string newMobileNumber)
        {
            user.MobileNumber = newMobileNumber;
            SaveUserData(); // Save user data to file after updating mobile number
            Console.WriteLine("Mobile number updated successfully.");
        }

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
