using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.POCO;
using System.Collections.Generic;
using System.Data;
using System.Windows.Media.Animation;

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
        decimal AddTransaction(int id, TRN01 transaction);

        /// <summary>
        /// View transaction history for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>List of transactions in the user's history.</returns>
        DataTable ViewTransactionHistory(int id);

        /// <summary>
        /// Get all transaction history for backup.
        /// </summary>
        /// <returns>List of transactions.</returns>
        DataTable GetAllTransactions();

        bool VerifyTransaction(int id,decimal amount);

    }

}
