using ATM_Simulation_Demo.Models;
using System.Collections.Generic;


namespace ATM_Simulation_Demo.BAL
{
    namespace ATM_Simulation_Demo.BAL.Interface
    {
        /// <summary>
        /// Interface for transaction-related operations.
        /// </summary>
        public interface IBLTransactionService
        {
            /// <summary>
            /// Adds a transaction to the user's transaction history.
            /// </summary>
            /// <param name="user">The user to add the transaction for.</param>
            /// <param name="transaction">The transaction to add.</param>
            void AddTransaction(BLAccountModel user, BLTransactionModel transaction);

            /// <summary>
            /// View transaction history for a user.
            /// </summary>
            /// <param name="user">The user.</param>
            /// <returns>List of transactions in the user's history.</returns>
            List<BLTransactionModel> ViewTransactionHistory(BLAccountModel user);
        }
    }


}
