using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM_Simulation_Demo.Models.V2
{

    /// <summary>
    /// Represents the model for a user in the system.
    /// </summary>
    public class AccountModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the user's Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the card number associated with the user.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the personal identification number (PIN) of the user.
        /// </summary>
        public string PIN { get; set; }

        /// <summary>
        /// Gets or sets the mobile number of the user.
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the balance in the user's account.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the transaction history of the user.
        /// </summary>
        public List<TransactionModel> TransactionHistory { get; set; }
        #endregion

        #region Constructor
        public  AccountModel(string UserName, string PIN, string MobileNumber)
        {
            Id = Generator.GenerateId();
            this.UserName = UserName;
            this.CardNumber = Generator.GenerateCardNumber();
            this.PIN = PIN;
            this.MobileNumber = MobileNumber;
            Balance = 0;
            TransactionHistory = new List<TransactionModel>();
        }
        #endregion

    }

    #region Helper Class
    /// <summary>
    /// Class to generate a unique card number
    /// </summary>
    public class Generator
    {
        private static Random random = new Random();
        private static int Id = 0;

        /// <summary>
        /// Generates a unique card number.
        /// </summary>
        /// <returns>The generated card number.</returns>
        public static string GenerateCardNumber()
        {
            // You can customize the prefix based on your needs
            string cardPrefix = "1234"; // Example prefix

            // Generate a unique identifier (you might want to use a more sophisticated approach)
            int uniqueIdentifier = random.Next(100000, 999999);

            // Concatenate the prefix and unique identifier to form the card number
            string cardNumber = $"{cardPrefix}-{uniqueIdentifier:D6}";
            return cardNumber;
        }

        public static int GenerateId()
        {
            return ++Id;
        }
    }
    #endregion
}