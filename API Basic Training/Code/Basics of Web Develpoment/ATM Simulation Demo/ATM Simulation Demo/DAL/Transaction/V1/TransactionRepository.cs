using ATM_Simulation_Demo.BAL.Interface.V1;
using ATM_Simulation_Demo.Models.V1;
using System;
using System.Collections.Generic;

namespace ATM_Simulation_Demo.DAL.Transaction.V1
{
    public class TransactionRepository : IBLTransactionRepository
    {
        #region Fields

        private readonly List<TransactionModel> _transactionDatabase;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRepository"/> class.
        /// </summary>
        public TransactionRepository()
        {
            _transactionDatabase = new List<TransactionModel>();
        }

        #endregion

        #region Public Methods


        /// <summary>
        /// Adds a transaction to the user's transaction history.
        /// </summary>
        /// <param name="user">The user to add the transaction for.</param>
        /// <param name="transaction">The transaction to add.</param>
        public void AddTransaction(AccountModel user, TransactionModel transaction)
        {
            user.TransactionHistory.Add(new TransactionModel
            {
                TransactionId = _transactionDatabase.Count + 1, // Generating a unique transaction ID
                Date = DateTime.Now,
                Description = transaction.Description,
                Amount = transaction.Amount
            });
        }


        /// <summary>
        /// View transaction history for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>List of transactions in the user's history.</returns>
        public List<TransactionModel> ViewTransactionHistory(AccountModel user)
        {
            return user.TransactionHistory;
        }

        #endregion
    }

}