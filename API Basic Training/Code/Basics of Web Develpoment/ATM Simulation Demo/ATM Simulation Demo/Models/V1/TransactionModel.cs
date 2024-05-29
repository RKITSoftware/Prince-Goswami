using Microsoft.Web.Http;
using System;

namespace ATM_Simulation_Demo.Models.V1
{

    /// <summary>
    /// Represents a transaction in the system.
    /// </summary>
    public class TransactionModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the transaction.
        /// </summary>
        public int TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the transaction.
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the description of the transaction.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the amount involved in the transaction.
        /// </summary>
        public decimal Amount { get; set; }
    }

    #region RequestModel
    /// <summary>
    /// Represents a request model for adding a transaction to an account.
    /// </summary>
    public class AddTransactionRequest
    {
        /// <summary>
        /// The ID of the account to which the transaction will be added.
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// The transaction details to be added to the account.
        /// </summary>
        public TransactionModel Transaction { get; set; }
    }

    #endregion
}