﻿using ATM_Simulation_Demo.BAL.Interface.V1;
using ATM_Simulation_Demo.Models.V1;
using System;
using System.Collections.Generic;

namespace ATM_Simulation_Demo.DAL.Transaction.V1
{
    public class TransactionRepository : IBLTransactionRepository
    {
        #region Fields

        private readonly List<BLTransactionModel> _transactionDatabase;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRepository"/> class.
        /// </summary>
        public TransactionRepository()
        {
            _transactionDatabase = new List<BLTransactionModel>();
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        /// <summary>
        /// Adds a transaction to the user's transaction history.
        /// </summary>
        /// <param name="user">The user to add the transaction for.</param>
        /// <param name="transaction">The transaction to add.</param>
        public void AddTransaction(BLAccountModel user, BLTransactionModel transaction)
        {
            user.TransactionHistory.Add(new BLTransactionModel
            {
                TransactionId = _transactionDatabase.Count + 1, // Generating a unique transaction ID
                Date = DateTime.Now,
                Description = transaction.Description,
                Amount = transaction.Amount
            });
        }

        /// <inheritdoc />
        /// <summary>
        /// View transaction history for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>List of transactions in the user's history.</returns>
        public List<BLTransactionModel> ViewTransactionHistory(BLAccountModel user)
        {
            return user.TransactionHistory;
        }

        #endregion
    }

}