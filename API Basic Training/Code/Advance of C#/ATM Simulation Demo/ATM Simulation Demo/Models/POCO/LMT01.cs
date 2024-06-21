

namespace ATM_Simulation_Demo.Models.POCO
{
    /// <summary>
    /// Model representing ATMLimits.
    /// </summary>
    public class LMT01
    {
        /// <summary>
        /// Gets or sets the LimitID (Auto-incremented primary key).
        /// </summary>
        public int T01F01 { get; set; }

        /// <summary>
        /// Gets or sets the AccountID (Foreign key referencing ACC01).
        /// </summary>
        public int T01F02 { get; set; }

        /// <summary>
        /// Gets or sets the DailyTransactionLimit (Default: 10).
        /// </summary>
        public int T01F03 { get; set; } 

        /// <summary>
        /// Gets or sets the MaxWithdrawalAmount (Default: 10000, Decimal(10, 2)).
        /// </summary>
        public decimal T01F04 { get; set; } 

        /// <summary>
        /// Gets or sets the UsedWithdrawalAmount (Default: 10000, Decimal(10, 2)).
        /// </summary>
        public decimal T01F05{ get; set; } 


        public LMT01(int accountId)
        {
            T01F02 = accountId;
            T01F03 = 3;
            T01F04 = 10000;
            T01F05 = 0;
        }
    }
}

