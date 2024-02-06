using Microsoft.Web.Http;
using System;

namespace ATM_Simulation_Demo.Models.V2
{
    /// <summary>
    /// Enum representing transaction types.
    /// </summary>
    public enum TransactionType
    {
        Debit,
        Credit
        // Add more roles as needed
    }

    /// <summary>
    /// Represents a transaction in the system.
    /// </summary>

    public class BLTransactionModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the transaction.
        /// </summary>
        public int TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the transaction.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the description of the transaction.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the amount involved in the transaction.
        /// </summary>
        public decimal Amount { get; set; }


        /// <summary>
        /// Gets or sets the Transaction type in the transaction.
        /// </summary>
        public TransactionType Type{ get; set; }
    }


}