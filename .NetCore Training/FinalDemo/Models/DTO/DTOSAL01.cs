using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.Models.DTO
{
    /// <summary>
    /// Represents sales and deal structuring.
    /// </summary>
    public class DTOSAL01 // Sales and Deal Structuring
    {
        ///<summary>
        /// Unique identifier for each deal.
        ///</summary>
        [Required(ErrorMessage = "Deal ID is required.")]
        [JsonProperty("L01101")]
        public int L01F01 { get; set; } // DealID

        ///<summary>
        /// Reference to the customer.
        ///</summary>
        [Required(ErrorMessage = "Customer ID is required.")]
        [JsonProperty("L01102")]
        public int L01F02 { get; set; } // CustomerID

        ///<summary>
        /// Reference to the vehicle being sold.
        ///</summary>
        [Required(ErrorMessage = "Vehicle ID is required.")]
        [JsonProperty("L01103")]
        public int L01F03 { get; set; } // VehicleID

        ///<summary>
        /// Final sale price of the vehicle.
        ///</summary>
        [Required(ErrorMessage = "Sale price is required.")]
        [JsonProperty("L01104")]
        public decimal L01F04 { get; set; } // SalePrice

        ///<summary>
        /// Date of the deal.
        ///</summary>
        [Required(ErrorMessage = "Date is required.")]
        [JsonProperty("L01105")]
        public DateTime L01F05 { get; set; } // Date

        ///<summary>
        /// Whether credit was approved.
        ///</summary>
        [JsonProperty("L01106")]
        public bool L01F06 { get; set; } // CreditApproved

        ///<summary>
        /// Whether the contract was signed.
        ///</summary>
        [JsonProperty("L01107")]
        public bool L01F07 { get; set; } // ContractSigned
    }
}
