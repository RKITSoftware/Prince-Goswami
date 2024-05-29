using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ORM_Tools.Models
{
    public class DTOBNK01
    {
        /// <summary>
        /// BankID
        /// </summary>
        [Required(ErrorMessage = "Bank Id is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Bank ID must be a positive number.")]
        [JsonProperty("K01101")]
        public int K01F01 { get; set; } 

        /// <summary>
        /// Bank Name
        /// </summary>
        [Required(ErrorMessage = "Bank Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Bank Name must be between 3 and 100 characters.")]
        [JsonProperty("K01102")]
        public String K01F02 { get; set; }

        /// <summary>
        /// Bank Short Name
        /// </summary>
        [Required(ErrorMessage = "Bank Short Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Bank Short Name must be between 2 and 50 characters.")]
        [JsonProperty("K01103")]
        public String K01F03 { get; set; }
    }
}
