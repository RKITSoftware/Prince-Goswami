using System;

namespace ATM_Simulation_Demo.Models
{

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
    }


}