using ATM_Simulation_Demo.Models;
using System.Collections.Generic;

namespace ATM_Simulation_Demo.BAL.Interface
{
    /// <summary>
    /// Interface for managing ATMLimits in the database.
    /// </summary>
    public interface IBLLimitRepository
    {
        ///// <summary>
        ///// Gets the ATMLimit by its ID.
        ///// </summary>
        ///// <param name="limitID">The ID of the ATMLimit to retrieve.</param>
        ///// <returns>The ATMLimit with the specified ID.</returns>
        //LMT01 GetATMLimitById(int limitID);

        /// <summary>
        /// Gets the ATMLimit for a specific account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <returns>The ATMLimit for the specified account.</returns>
        LMT01 GetATMLimitByAccountID(int accountID);

        /// <summary>
        /// Updates the ATMLimit for a specific account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <param name="newLimit">The new ATMLimit values.</param>
        void UpdateATMLimit(int accountID, decimal updatedWithdrawlLimit);

        /// <summary>
        /// Resets of all ATMLimits in the system.
        /// </summary>
        /// <returns>A list of all ATMLimits.</returns>
        void ResetAllATMLimits();

        /// <summary>
        /// Adds a new ATMLimit for a specific account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <param name="limit">The ATMLimit to add.</param>
        void AddATMLimit(LMT01 limit);

        /// <summary>
        /// Updates the dailiy ATMLimit for a specific account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <param name="newLimit">The new ATMLimit values.</param>
        void UpdateDailyATMLimit(int accountID, LMT01 limit);

        ///// <summary>
        ///// Deletes the ATMLimit for a specific account.
        ///// </summary>
        ///// <param name="accountID">The ID of the account.</param>
        //void DeleteATMLimit(int accountID);
    }
}
