using ATM_Simulation_Demo.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections.Generic;
using System.Windows.Media.Animation;

namespace ATM_Simulation_Demo.BAL.Interface
{
        /// <summary>
        /// Interface for transaction-related operations.
        /// </summary>
        public interface IBLTransactionService
        {
        /// <summary>
        /// Adds a transaction to the account's transaction history.
        /// </summary>
        /// <param name="account">The account to add the transaction for.</param>
        /// <param name="transaction">The transaction to add.</param>
            decimal AddTransaction(int  accountId, decimal amount, TransactionType TransactionType, string Description);

        /// <summary>
        /// View transaction history for a account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>List of transactions in the account's history.</returns>
            List<TRN01> ViewTransactionHistory(int accountId);
        
        /// <summary>
        /// View transactions.
        /// </summary>
        /// <returns>List of transactions.</returns>
            List<TRN01> GetAllTransactions();
    }
}



