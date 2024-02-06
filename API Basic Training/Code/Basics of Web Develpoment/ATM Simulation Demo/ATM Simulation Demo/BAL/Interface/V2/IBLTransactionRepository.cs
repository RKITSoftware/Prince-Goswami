﻿using ATM_Simulation_Demo.Models.V2;
using System.Collections.Generic;


namespace ATM_Simulation_Demo.BAL.Interface.V2
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
        BLAccountModel AddTransaction(BLAccountModel account, BLTransactionModel transaction);

        /// <summary>
        /// View transaction history for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>List of transactions in the user's history.</returns>
        List<BLTransactionModel> ViewTransactionHistory(BLAccountModel account);
    }

}
