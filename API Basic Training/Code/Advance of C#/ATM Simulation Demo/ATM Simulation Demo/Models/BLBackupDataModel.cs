using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM_Simulation_Demo.Models
{
    public class BackupDataModel
    {
        public List<ACC01> Accounts { get; set; }
        public List<TRN01> AllTransactions { get; set; }
        public List<USR01> Users { get; set; }
    }
}