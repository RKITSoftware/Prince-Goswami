using ATM_Simulation_Demo.Models;
using System.Collections.Generic;


namespace ATM_Simulation_Demo.BAL.Interface
{
    /// <summary>
    /// Interface defining methods for transaction data access.
    /// </summary>
    public interface IBLTransactionRepository
    {
        /// <summary>
        /// Adds a transaction to the user's transaction history.
        /// </summary>
        /// <param name="user">The user to add the transaction for.</param>
        /// <param name="transaction">The transaction to add.</param>
        ACC01 AddTransaction(ACC01 account, TRN01 transaction);

        /// <summary>
        /// View transaction history for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>List of transactions in the user's history.</returns>
        List<TRN01> ViewTransactionHistory(ACC01 account);
        
        /// <summary>
        /// Get all transaction history for backup.
        /// </summary>
        /// <returns>List of transactions.</returns>
        List<TRN01> GetAllTransactions( );
    }

}
