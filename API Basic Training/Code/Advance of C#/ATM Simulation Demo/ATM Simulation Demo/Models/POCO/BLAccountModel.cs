using ATM_Simulation_Demo.Others.Security;
using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ATM_Simulation_Demo.Models.POCO
{

    /// <summary>
    /// Represents the model for a user in the system.
    /// </summary>
    public class ACC01
    {
        #region Properties

        /// <summary>
        /// Account ID (Primary Key, Auto Incremented)
        /// </summary>
        public int C01F01 { get; set; }

        /// <summary>
        /// Card Number (Unique, Not Null)
        /// </summary>
        public string C01F02 { get; set; }

        /// <summary>
        /// Name (Not Null)
        /// </summary>
        public string C01F03 { get; set; }

        /// <summary>
        /// PIN (Not Null)
        /// </summary>
        public string C01F04 { get; set; }

        /// <summary>
        /// Mobile Number
        /// </summary>
        public string C01F05 { get; set; }

        /// <summary>
        /// Balance (Default: 0)
        /// </summary>
        public decimal C01F06 { get; set; }

        #endregion

      
    }

   
}