using ATM_Simulation_Demo.BAL.Interface.V2;
using ATM_Simulation_Demo.Models.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM_Simulation_Demo.DAL.Transaction.V2
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
        /// Adds a transaction to the account's transaction history.
        /// </summary>
        /// <param name="account">The account to add the transaction for.</param>
        /// <param name="transaction">The transaction to add.</param>
        public BLAccountModel AddTransaction(BLAccountModel account, BLTransactionModel transaction)
        {
            if (VerifyTransaction(account.Balance, transaction.Type, transaction.Amount))
            {
                //update balance
                if(transaction.Type.ToString() == "Debit")
                {
                    transaction.Amount *= -1;
                }

                account.Balance += transaction.Amount;

                //add transaction
                account.TransactionHistory.Add(new BLTransactionModel
                {
                    TransactionId = _transactionDatabase.Count + 1, // Generating a unique transaction ID
                    Date = DateTime.Now,
                    Description = transaction.Description,
                    Amount = transaction.Amount
                });
            }
            return account;
        }

        /// <inheritdoc />
        /// <summary>
        /// View transaction history for a account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>List of transactions in the account's history.</returns>
        public List<BLTransactionModel> ViewTransactionHistory(BLAccountModel account)
        {
            return account.TransactionHistory;
        }


        private bool VerifyTransaction(decimal balance, TransactionType transactionType, decimal amount)
        {
            if (transactionType.ToString() == "Debit" && balance - amount >= 10)
            {
                return true;
            }
            return false;
        }
        #endregion
    }

}