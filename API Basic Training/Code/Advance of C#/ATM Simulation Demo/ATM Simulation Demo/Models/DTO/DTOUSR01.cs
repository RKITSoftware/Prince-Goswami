using ATM_Simulation_Demo.Models.POCO;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ATM_Simulation_Demo.Models.DTO
{
    /// <summary>
    /// DTO Model for requesting user details.
    /// </summary>
    public class DTO_USR01
    {
        /// <summary>
        /// User ID (Primary Key, Auto Incremented)
        /// </summary>
        [JsonProperty("R01101")]
        public int R01F01 { get; set; }

        /// <summary>
        /// User Name (Not Null)
        /// </summary>
        [Required(ErrorMessage = "User Name is required.")]
        [JsonProperty("R01102")]
        public string R01F02 { get; set; }

        /// <summary>
        /// Mobile Number (Not Null)
        /// </summary>
        [Required(ErrorMessage = "Mobile Number is required.")]
        [JsonProperty("R01103")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Mobile Number.")]
        public string R01F03 { get; set; }

        /// <summary>
        /// Date of Birth (Not Null)
        /// </summary>
        [Required(ErrorMessage = "Date of Birth is required.")]
        [JsonProperty("R01104")]
        public DateTime R01F04 { get; set; }

        /// <summary>
        /// Role (Not Null)
        /// </summary>
        [Required(ErrorMessage = "Role is required.")]
        [JsonProperty("R01105")]
        public UserRole R01F05 { get; set; }

        /// <summary>
        /// Password (Not Null)
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [JsonProperty("R01106")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string R01F06 { get; set; }
    }
}
