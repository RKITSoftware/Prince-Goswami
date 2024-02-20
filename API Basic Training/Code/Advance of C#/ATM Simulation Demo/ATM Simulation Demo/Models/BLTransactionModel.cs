using Microsoft.Web.Http;
using System;

namespace ATM_Simulation_Demo.Models
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

    public class TRN01
    {
        /// <summary>
        /// Transaction ID (Primary Key, Auto Incremented)
        /// </summary>
        public int N01F01 { get; set; }

        /// <summary>
        /// Account ID (Foreign Key)
        /// </summary>
        public int N01F02 { get; set; }

        /// <summary>
        /// Transaction Type (Not Null, DEBIT or CREDIT)
        /// </summary>
        public TransactionType N01F03 { get; set; }

        /// <summary>
        /// Amount (Not Null)
        /// </summary>
        public decimal N01F04 { get; set; }

        /// <summary>
        /// Transaction Date (Default: Current Timestamp)
        /// </summary>
        public DateTime N01F05 { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string N01F06 { get; set; }

    }
}