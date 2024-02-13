using System;
using System.Collections.Generic;

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


/// <summary>
/// Concrete implementation of the abstract class
/// </summary>
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
    /// <summary>
    /// Gets or sets the user's name.
    /// </summary>
    public string UserName
    {
        get { return userName; }
        set { userName = value; }
    }

    /// <summary>
    /// Gets or sets the card number associated with the user.
    /// </summary>
    public string CardNumber
    {
        get { return cardNumber; }
        set { cardNumber = value; }
    }

    /// <summary>
    /// Gets or sets the Personal Identification Number (PIN) for the user.
    /// </summary>
    public string PIN
    {
        get { return pin; }
        set { pin = value; }
    }

    /// <summary>
    /// Gets or sets the mobile number of the user.
    /// </summary>
    public string MobileNumber
    {
        get { return mobileNumber; }
        set { mobileNumber = value; }
    }

    /// <summary>
    /// Gets or sets the current balance of the user.
    /// </summary>
    public decimal Balance
    {
        get { return balance; }
        set { balance = value; }
    }

    /// <summary>
    /// Gets or sets the transaction history associated with the user.
    /// </summary>
    public List<Transaction> TransactionHistory
    {
        get { return transactionHistory; }
        set { transactionHistory = value; }
    }


    #endregion

    #region Constructor

    // Constructor
    public User(string userName, string pin, string mobileNumber, List<Transaction> transactionHistory = null, decimal balance = 0)
    {
        if(transactionHistory == null) transactionHistory = new List<Transaction>();
        UserName = userName;
        CardNumber = CardNumberGenerator.GenerateCardNumber();
        PIN = pin;
        MobileNumber = mobileNumber;
        Balance = balance;
        TransactionHistory = transactionHistory;
        
    }

    #endregion

    #region Methods from the abstract class

    /// <inheritdoc/>
    public override void UpdateBalance(decimal amount)
    {
        Balance += amount;
    }

    /// <inheritdoc/>
    public override void AddTransaction(Transaction transaction)
    {
        TransactionHistory.Add(transaction);
    }

    /// <inheritdoc/>
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


/// <summary>
/// Transaction class to store transaction details
/// </summary>
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