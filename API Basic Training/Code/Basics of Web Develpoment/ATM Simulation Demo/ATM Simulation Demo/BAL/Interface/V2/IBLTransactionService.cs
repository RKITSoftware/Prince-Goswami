using ATM_Simulation_Demo.Models.V2;
using System.Collections.Generic;
using System.Windows.Media.Animation;

namespace ATM_Simulation_Demo.BAL.Interface.V2
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
            decimal AddTransaction(int  accountId, BLTransactionModel transaction);

            /// <summary>
            /// View transaction history for a account.
            /// </summary>
            /// <param name="account">The account.</param>
            /// <returns>List of transactions in the account's history.</returns>
            List<BLTransactionModel> ViewTransactionHistory(int accountId);
        }
    }



