using ATM_Simulation_Demo.Models.POCO;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ATM_Simulation_Demo.Models.DTO
{
    /// <summary>
    /// Represents a transaction in the system.
    /// </summary>
    public class DTO_TRN01
    {
        /// <summary>
        /// Transaction ID (Primary Key, Auto Incremented)
        /// </summary>
        [JsonProperty("N01101")]
        public int N01F01 { get; set; }

        /// <summary>
        /// Account ID (Foreign Key)
        /// </summary>
        [JsonProperty("N01102")]
        public int N01F02 { get; set; }

        /// <summary>
        /// Transaction Type (Not Null, DEBIT or CREDIT)
        /// </summary>
        [JsonProperty("N01103")]
        [Required(ErrorMessage = "TransactionType is required.")]
        public TransactionType N01F03 { get; set; }

        /// <summary>
        /// Amount (Not Null)
        /// </summary>
        [JsonProperty("N01104")]
        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal N01F04 { get; set; }

        /// <summary>
        /// Transaction Date (Default: Current Timestamp)
        /// </summary>
        [JsonProperty("N01105")]
        [Required(ErrorMessage = "Transaction Date is required.")]
        public DateTime N01F05 { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("N01106")]
        public string N01F06 { get; set; }
    }
}
