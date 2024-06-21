using ATM_Simulation_Demo.Others.Security;
using Microsoft.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ATM_Simulation_Demo.Models.DTO
{
    /// <summary>
    /// Represents the model for a user in the system.
    /// </summary>
    public class DTOACC01
    {
        #region Properties
        //// validation
        /// <summary>
        /// Account ID (Primary Key, Auto Incremented)
        /// </summary>
        [Required(ErrorMessage = "Account id is required.")]
        [Range(0,int.MaxValue)]
        [JsonProperty("C01101")]
        public int C01F01 { get; set; }

        //// validation 
        /// <summary>
        /// Card Number (Unique, Not Null)
        /// </summary>
        [Required(ErrorMessage = "Card Number is required.")]
        [JsonProperty("C01102")]
        public string C01F02 { get; set; }

        /// <summary>
        /// Name (Not Null)
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [JsonProperty("C01103")]
        public string C01F03 { get; set; }

        /// <summary>
        /// PIN (Not Null)
        /// </summary>
        [Required(ErrorMessage = "PIN is required.")]
        [JsonProperty("C01104")]
        public string C01F04 { get; set; }

        /// <summary>
        /// Mobile Number
        /// </summary>
        [JsonProperty("C01105")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Mobile Number.")]
        public string C01F05 { get; set; }

        /// <summary>
        /// Balance (Default: 0)
        /// </summary>
        [JsonProperty("C01106")]
        public decimal? C01F06 { get; set; }

        /// <summary>
        /// Date Of Birth
        /// </summary>
        [JsonProperty("C01107")]
        public DateTime C01F07 { get; set; } = DateTime.MinValue;
        #endregion
    }
}
