using System;
using System.Collections.Generic;

// Abstract class that defines common methods for user-related operations
public abstract class UserBase
{
    #region Abstract Methods

    /// <summary>
    /// Updates the user's balance.
    /// </summary>
    /// <param name="amount">The amount to update the balance by.</param>
    public abstract void UpdateBalance(decimal amount);

    /// <summary>
    /// Adds a transaction to the user's transaction history.
    /// </summary>
    /// <param name="transaction">The transaction to add.</param>
    public abstract void AddTransaction(Transaction transaction);

    /// <summary>
    /// Displays the user's details.
    /// </summary>
    public abstract void DisplayUserDetails();

    #endregion
}

// Concrete implementation of the abstract class
public class User : UserBase
{
    #region Private Fields

    private string userName;
    private string cardNumber;
    private string pin;
    private string mobileNumber;
    private decimal balance;
    private List<Transaction> transactionHistory;

    #endregion

    #region Properties

    public string UserName
    {
        get { return userName; }
        set { userName = value; }
    }

    public string CardNumber
    {
        get { return cardNumber; }
        set { cardNumber = value; }
    }

    public string PIN
    {
        get { return pin; }
        set { pin = value; }
    }

    public string MobileNumber
    {
        get { return mobileNumber; }
        set { mobileNumber = value; }
    }

    public decimal Balance
    {
        get { return balance; }
        set { balance = value; }
    }

    public List<Transaction> TransactionHistory
    {
        get { return transactionHistory; }
        set { transactionHistory = value; }
    }

    #endregion

    #region Constructor

    // Constructor
    public User(string userName, string pin, string mobileNumber, decimal balance = 0)
    {
        UserName = userName;
        CardNumber = CardNumberGenerator.GenerateCardNumber();
        PIN = pin;
        MobileNumber = mobileNumber;
        Balance = balance;
        TransactionHistory = new List<Transaction>();
        Console.WriteLine("Your account has been created.\n" +
            "Your card number is " + CardNumber +
            "\nYour card PIN is " + pin);
    }

    #endregion

    #region Methods from the abstract class

    public override void UpdateBalance(decimal amount)
    {
        Balance += amount;
    }

    public override void AddTransaction(Transaction transaction)
    {
        TransactionHistory.Add(transaction);
    }

    public override void DisplayUserDetails()
    {
        Console.WriteLine($"Card Number: {CardNumber}");
        Console.WriteLine($"PIN: {PIN}");
        Console.WriteLine($"Mobile Number: {MobileNumber}");
        Console.WriteLine($"Balance: {Balance:C}");

        Console.WriteLine("Transaction History:");
        foreach (Transaction transaction in TransactionHistory)
        {
            Console.WriteLine($"{transaction.Date}: {transaction.Description}, Amount: {transaction.Amount:C}");
        }
    }

    #endregion
}

// Transaction class to store transaction details
public class Transaction
{
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }

    public Transaction(string description, decimal amount)
    {
        Date = DateTime.Now;
        Description = description;
        Amount = amount;
    }
}

// Class to generate a unique card number
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