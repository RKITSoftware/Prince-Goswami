using System;

namespace Test_of_Basic_C__training
{
    /// <summary>
    /// Abstract class that defines common methods for user-related operations
    /// </summary>
    public abstract class UserBase
    {
        #region Abstract Methods

        /// <summary>
        /// Updates the user's balance.
        /// </summary>
        /// <param name="amount">The amount to update the balance by.</param>
        public abstract void UpdateBalance(UserModel user, decimal amount);

        /// <summary>
        /// Adds a transaction to the user's transaction history.
        /// </summary>
        /// <param name="transaction">The transaction to add.</param>
        public abstract void AddTransaction(UserModel user, Transaction transaction);

        /// <summary>
        /// Displays the user's details.
        /// </summary>
        public abstract void DisplayUserDetails(UserModel user);

        #endregion
    }

    /// <summary>
    /// Concrete implementation of the abstract class
    /// </summary>
    public class UserManagement : UserBase
    {

        #region Methods from the abstract class

        /// <summary>
        /// Updates the user's balance.
        /// </summary>
        /// <param name="amount">The amount to update the balance by.</param>    
        public override void UpdateBalance(UserModel user, decimal amount)
        {
            user.Balance += amount;
        }

        /// <summary>
        /// Adds a transaction to the user's transaction history.
        /// </summary>
        /// <param name="transaction">The transaction to add.</param>
        public override void AddTransaction(UserModel user, Transaction transaction)
        {
            user.TransactionHistory.Add(transaction);
        }

        /// <summary>
        /// Displays the user's details.
        /// </summary>
        public override void DisplayUserDetails(UserModel user)
        {
            Console.WriteLine("");
            Console.WriteLine("--- User Details ---");
            Console.WriteLine($"Name: {user.UserName}");
            Console.WriteLine($"Card Number: {user.CardNumber}");
            Console.WriteLine($"PIN: {user.PIN}");
            Console.WriteLine($"Mobile Number: {user.MobileNumber}");
            Console.WriteLine($"Balance: {user.Balance:C}");
            Console.WriteLine("");

        }

        #endregion
    }

    /// <summary>
    /// Transaction class to store transaction details
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the date of the transaction.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the description of the transaction.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the amount of the transaction.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class with the specified description and amount.
        /// The date is automatically set to the current date and time.
        /// </summary>
        /// <param name="description">The description of the transaction.</param>
        /// <param name="amount">The amount of the transaction.</param>
        public Transaction(string description, decimal amount)
        {
            Date = DateTime.Now;
            Description = description;
            Amount = amount;
        }
    }


    #region helper Class
    /// <summary>
    /// Class to generate a unique card number
    /// </summary>
    public class CardNumberGenerator
    {
        private static Random random = new Random();

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
    }
    #endregion
}