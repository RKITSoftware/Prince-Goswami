using System.Collections.Generic;

namespace Test_of_Basic_C__training
{
    /// <summary>
    /// Represents a user account in the banking system.
    /// </summary>
    public class UserModel
    {
        #region Private Fields
        /// <summary>
        /// Gets or sets the user's name associated with the account.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the card number linked to the user's account.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the Personal Identification Number (PIN) for account security.
        /// </summary>
        public string PIN { get; set; }

        /// <summary>
        /// Gets or sets the mobile number linked to the user's account for communication.
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the current balance in the user's account.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets a list of transactions representing the user's transaction history.
        /// </summary>
        public List<Transaction> TransactionHistory { get; set; }

        #endregion

        #region Constructor

        // Constructor
        public UserModel(string userName, string pin, string mobileNumber, List<Transaction> transactionHistory = null, decimal balance = 0)
        {
            if (transactionHistory == null) transactionHistory = new List<Transaction>();
            UserName = userName;
            CardNumber = CardNumberGenerator.GenerateCardNumber();
            PIN = pin;
            MobileNumber = mobileNumber;
            Balance = balance;
            TransactionHistory = transactionHistory;
        }

        #endregion

    }
}
