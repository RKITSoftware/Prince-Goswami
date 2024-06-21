using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.Models.DTO
{
    /// <summary>
    /// Represents customer transactions.
    /// </summary>
    public class DTOCUS02 // Customer Transactions
    {
        ///<summary>
        /// Unique identifier for each transaction.
        ///</summary>
        [Required(ErrorMessage = "Transaction ID is required.")]
        [JsonProperty("S02101")]
        public int S02F01 { get; set; } // CustTransID

        ///<summary>
        /// Reference to the customer.
        ///</summary>
        [Required(ErrorMessage = "Customer ID is required.")]
        [JsonProperty("S02102")]
        public int S02F02 { get; set; } // CustomerID

        ///<summary>
        /// Reference to the vehicle involved.
        ///</summary>
        [Required(ErrorMessage = "Vehicle ID is required.")]
        [JsonProperty("S02103")]
        public int S02F03 { get; set; } // VehicleID

        ///<summary>
        /// Price at which the customer bought the vehicle.
        ///</summary>
        [Required(ErrorMessage = "Sale price is required.")]
        [JsonProperty("S02104")]
        public decimal S02F04 { get; set; } // SalePrice

        ///<summary>
        /// Applicable taxes on the transaction.
        ///</summary>
        [JsonProperty("S02105")]
        public decimal? S02F05 { get; set; } // TaxAmount

        ///<summary>
        /// Date and time of the transaction.
        ///</summary>
        [Required(ErrorMessage = "Transaction date is required.")]
        [JsonProperty("S02106")]
        public DateTime S02F06 { get; set; } // TransactionDate
    }
}
