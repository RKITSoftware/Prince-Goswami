using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.Models.DTO
{
    /// <summary>
    /// Represents vehicle inventory.
    /// </summary>
    public class DTOVEH01 // Vehicle Inventory
    {
        ///<summary>
        /// Unique identifier for each vehicle.
        ///</summary>
        [Required(ErrorMessage = "Vehicle ID is required.")]
        [JsonProperty("H01101")]
        public int H01F01 { get; set; } // VehicleID

        ///<summary>
        /// Name of the vehicle.
        ///</summary>
        [Required(ErrorMessage = "Name is required.")]
        [JsonProperty("H01102")]
        public string H01F02 { get; set; } // Name

        ///<summary>
        /// Description of the vehicle.
        ///</summary>
        [Required(ErrorMessage = "Description is required.")]
        [JsonProperty("H01103")]
        public string H01F03 { get; set; } // Description

        ///<summary>
        /// Price of the vehicle.
        ///</summary>
        [Required(ErrorMessage = "Price is required.")]
        [JsonProperty("H01104")]
        public decimal H01F04 { get; set; } // Price

        ///<summary>
        /// Quantity of the vehicle in stock.
        ///</summary>
        [Required(ErrorMessage = "Stock quantity is required.")]
        [JsonProperty("H01105")]
        public int H01F05 { get; set; } // StockQuantity

        ///<summary>
        /// Level at which to reorder the vehicle.
        ///</summary>
        [Required(ErrorMessage = "Reorder level is required.")]
        [JsonProperty("H01106")]
        public int H01F06 { get; set; } // ReorderLevel

        ///<summary>
        /// Category ID of the vehicle.
        ///</summary>
        [Required(ErrorMessage = "Category ID is required.")]
        [JsonProperty("H01107")]
        public int H01F07 { get; set; } // CategoryID
    }
}
