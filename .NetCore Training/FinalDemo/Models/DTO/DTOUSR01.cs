using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.Models.DTO
{

    /// <summary>
    /// Represents user management.
    /// </summary>
    public class DTOUSR01 // User Management
    {
        ///<summary>
        /// Unique identifier for each user.
        ///</summary>
        [Required(ErrorMessage = "User ID is required.")]
        [JsonProperty("R01101")]
        public int R01F01 { get; set; } // UserID

        ///<summary>
        /// User's login name.
        ///</summary>
        [Required(ErrorMessage = "Username is required.")]
        [JsonProperty("R01102")]
        public string R01F02 { get; set; } // Username

        ///<summary>
        /// Hash of the user's password.
        ///</summary>
        [Required(ErrorMessage = "Password hash is required.")]
        [JsonProperty("R01103")]
        public string R01F03 { get; set; } // PasswordHash

        ///<summary>
        /// Role of the user.
        ///</summary>
        [Required(ErrorMessage = "Role is required.")]
        [JsonProperty("R01104")]
        public enmUserRole R01F04 { get; set; } // Role

        ///<summary>
        /// User's email address.
        ///</summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [JsonProperty("R01105")]
        public string R01F05 { get; set; } // Email

        ///<summary>
        /// Account creation date and time.
        ///</summary>
        [Required(ErrorMessage = "Creation date is required.")]
        [JsonProperty("R01106")]
        public DateTime R01F06 { get; set; } // CreatedAt
    }
}
