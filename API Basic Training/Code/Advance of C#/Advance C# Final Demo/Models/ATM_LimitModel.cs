
namespace Advance_C__Final_Demo.Models
{
    /// <summary>
    /// Represents ATM limits for users in the system.
    /// </summary>
    public class ATMLimit
    {
        /// <summary>
        /// Gets or sets the unique identifier for the ATM limit.
        /// </summary>
        public int LimitId { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the ATM limit.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the daily transaction limit for the user.
        /// </summary>
        public int DailyTransactionLimit { get; set; }

        /// <summary>
        /// Gets or sets the maximum withdrawal amount allowed for the user.
        /// </summary>
        public decimal MaxWithdrawalAmount { get; set; }
    }
}