using System.Collections.Generic;
using ATM_Simulation_Demo.Models;

namespace ATM_Simulation_Demo.BAL.Interface
{
    /// <summary>
    /// Interface for managing ATMLimits.
    /// </summary>
    public interface IBLLimitService
    {
        /// <summary>
        /// Gets the ATMLimit for a specific account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <returns>The ATMLimit for the specified account.</returns>
        LMT01 GetATMLimitByAccountID(int accountID);

        /// <summary>
        /// Verifies if a withdrawal amount is within the ATMLimit for a specific account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <param name="amount">The withdrawal amount.</param>
        /// <returns>True if the withdrawal is within the ATMLimit, false otherwise.</returns>
        bool VerifyWithdrawal(int accountID, decimal amount);

        /// <summary>
        /// Updates the ATMLimit for a specific account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <param name="newLimit">The new ATMLimit values.</param>
        void UpdateATMLimit(int accountID, decimal newWithdrawlLimitAmount);

        /// <summary>
        /// Adds a new ATMLimit for a specific account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <param name="limit">The ATMLimit to add.</param>
        void AddATMLimit(int accountID);

        /// <summary>
        /// Deletes the ATMLimit for a specific account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        void DeleteATMLimit(int accountID);
    }
}
