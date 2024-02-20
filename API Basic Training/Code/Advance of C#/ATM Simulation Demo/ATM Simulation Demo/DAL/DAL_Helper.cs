using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ATM_Simulation_Demo.DAL
{
    public class DAL_Helper
    {
        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
    }
}