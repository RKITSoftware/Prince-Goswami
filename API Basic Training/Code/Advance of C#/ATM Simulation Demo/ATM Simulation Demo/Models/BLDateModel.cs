using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM_Simulation_Demo.Models
{
    public class CDT01
    {
        /// <summary>
        /// Date Id
        /// </summary>
        public int T01F01{ get; set; }

        /// <summary>
        /// Date
        /// </summary>
        public DateTime T01F02 { get; set; } = DateTime.Now;
    }
}