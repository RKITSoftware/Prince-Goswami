using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DB_CRUD.Models
{
    public class DTOBNK01
    {
        /// <summary>
        /// BankID
        /// </summary>
        /// ///require range
        [JsonProperty("k01101")]
        public int K01F01 { get; set; }

        /// <summary>
        /// Bank Name
        /// </summary>
        [JsonProperty("k01102")]
        [Required(ErrorMessage = "Bank Name is required")]
        [StringLength(100, ErrorMessage = "Bank Name must be less than {1} characters")]
        public String K01F02 { get; set; }

        /// <summary>
        /// Bank Short Name
        /// </summary>
        [JsonProperty("k01103")]
        [Required(ErrorMessage = "Bank Short Name is required")]
        [StringLength(50, ErrorMessage = "Bank Short Name must be less than {1} characters")]
        public String K01F03 { get; set; }
    }
}
