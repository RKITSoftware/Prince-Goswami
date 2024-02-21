using ATM_Simulation_Demo.Others.Security;
using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ATM_Simulation_Demo.Models
{

    /// <summary>
    /// Represents the model for a user in the system.
    /// </summary>
    public class ACC01
    {
        #region Properties

        /// <summary>
        /// Account ID (Primary Key, Auto Incremented)
        /// </summary>
        public int C01F01 { get; set; }

        /// <summary>
        /// Card Number (Unique, Not Null)
        /// </summary>
        public string C01F02 { get; set; }

        /// <summary>
        /// Name (Not Null)
        /// </summary>
        public string C01F03 { get; set; }

        /// <summary>
        /// PIN (Not Null)
        /// </summary>
        public string C01F04 { get; set; }

        /// <summary>
        /// Mobile Number
        /// </summary>
        public string C01F05 { get; set; }

        /// <summary>
        /// Balance (Default: 0)
        /// </summary>
        public decimal C01F06 { get; set; }

        #endregion

        #region Constructor
        public ACC01(string UserName, string PIN, string MobileNumber)
        {
            C01F02 = UserName;
            C01F03 = Generator.GenerateCardNumber();
            C01F04 = PIN;
            C01F05 = MobileNumber;
            C01F06 = 0;
        }

        public ACC01() { }
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

        //public static int GenerateId()
        //{
        //    return ++Id;
        //}
    }
    #endregion
}