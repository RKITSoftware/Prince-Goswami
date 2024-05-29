using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.POCO;
using MySql.Data.MySqlClient;

namespace ATM_Simulation_Demo.BAL.Interface
{
    /// <summary>
    /// Interface for managing ATM limits.
    /// </summary>
    public interface IBLLimitService
    {
        /// <summary>
        /// Retrieves the ATM limit for a specific account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <returns>The ATM limit for the specified account.</returns>
        Response GetATMLimitByAccountID(int accountID);

        /// <summary>
        /// Retrieves the ATM limit for a specific account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <returns>The ATM limit for the specified account.</returns>
        Response GetAllATMLimit();

        /// <summary>
        /// Verifies if a withdrawal amount is within the ATM limit for a specific account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <param name="amount">The withdrawal amount.</param>
        /// <returns>True if the withdrawal is within the ATM limit, otherwise false.</returns>
        bool VerifyWithdrawal(int accountID, decimal amount);

        /// <summary>
        /// Updates the ATM limit for a specific account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <param name="newWithdrawlLimitAmount">The new ATM limit value.</param>
        Response UpdateATMLimit(int accountID, decimal newWithdrawlLimitAmount);

        /// <summary>
        /// Updates the daily ATM limit for a specific account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <param name="balance">The balance after the withdrawal.</param>
        /// <returns>True if the limit was updated successfully, otherwise false.</returns>
        bool UpdateDailyATMLimit(MySqlConnection connection, int accountID, decimal balance);

        /// <summary>
        /// Adds a new ATM limit for a specific account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        void AddATMLimit(int accountID);

        /// <summary>
        /// Deletes the ATM limit for a specific account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        void DeleteATMLimit(int accountID);
    }
}
