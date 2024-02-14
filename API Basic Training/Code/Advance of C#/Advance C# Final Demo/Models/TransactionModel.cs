using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Advance_C__Final_Demo.Models
{
    /// <summary>
    /// Represents a financial transaction in the system.
    /// </summary>
    public class TRN01
    {
        /// <summary>
        /// Gets or sets the unique identifier for the transaction.
        /// </summary>
        public int N01F01 { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the transaction.
        /// </summary>
        public int N01F02 { get; set; }

        /// <summary>
        /// Gets or sets the type of transaction (e.g., Deposit, Withdrawal).
        /// </summary>
        public string N01F03 { get; set; }

        /// <summary>
        /// Gets or sets the amount involved in the transaction.
        /// </summary>
        public decimal N01F04 { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the transaction occurred.
        /// </summary>
        public DateTime N01F05 { get; set; }
    }


    // Example request model for adding a new transaction
    public class TransactionRequest
    {
        public int UserId { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
    }
}
