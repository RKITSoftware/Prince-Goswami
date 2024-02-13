using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ORM_Tools.Models
{
    [Alias("BNK01")]
    public class BNK01
    {

        /// <summary>
        /// BankID
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int K01F01 { get; set; }

        /// <summary>
        /// Bank Name
        /// </summary>
        public String K01F02 { get; set; }

        /// <summary>
        /// Bank Short Name
        /// </summary>
        public String K01F03 { get; set; }
    }
}