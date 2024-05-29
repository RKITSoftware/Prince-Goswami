

namespace ATM_Simulation_Demo.Models.DTO
{
    /// <summary>
    /// Model representing ATMLimits.
    /// </summary>
    public class DTO_LMT01
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


    }
}

