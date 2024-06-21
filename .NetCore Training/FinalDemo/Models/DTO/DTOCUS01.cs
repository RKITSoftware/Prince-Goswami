using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.Models.DTO
{
    /// <summary>
    /// Represents customer profiles.
    /// </summary>
    public class DTOCUS01 // Customer Profiles
    {
        ///<summary>
        /// CustomerID - Unique identifier for each customer.
        ///</summary>
        [Required(ErrorMessage = "Customer ID is required.")]
        [Range(0,int.MaxValue)]
        [JsonProperty("S01101")]
        public int S01F01 { get; set; } // CustomerID

        ///<summary>
        /// FirstName - Customer's first name.
        ///</summary>
        [Required(ErrorMessage = "First name is required.")]
        [JsonProperty("S01102")]
        public string S01F02 { get; set; } // FirstName

        ///<summary>
        /// LastName - Customer's last name.
        ///</summary>
        [Required(ErrorMessage = "Last name is required.")]
        [JsonProperty("S01103")]
        public string S01F03 { get; set; } // LastName

        ///<summary>
        /// Email - Customer's email address.
        ///</summary>
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [JsonProperty("S01104")]
        public string S01F04 { get; set; } // Email

        ///<summary>
        /// Phone - Customer's phone number.
        ///</summary>
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [JsonProperty("S01105")]
        public string S01F05 { get; set; } // Phone

        ///<summary>
        /// Address - Customer's address.
        ///</summary>
        [Required(ErrorMessage = "Address is required.")]
        [JsonProperty("S01106")]
        public string S01F06 { get; set; } // Address
    }
}
