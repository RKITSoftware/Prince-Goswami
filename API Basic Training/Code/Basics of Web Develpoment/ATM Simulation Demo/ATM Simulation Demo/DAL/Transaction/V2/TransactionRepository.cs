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

        /// <inheritdoc />
        /// <summary>
        /// Adds a transaction to the account's transaction history.
        /// </summary>
        /// <param name="account">The account to add the transaction for.</param>
        /// <param name="transaction">The transaction to add.</param>
        public AccountModel AddTransaction(AccountModel account, TransactionModel transaction)
        {
            if (VerifyTransaction(account.Balance, transaction.Type, transaction.Amount))
            {
                //update balance
                if (transaction.Type.ToString() == "D")
                {
                    transaction.Amount *= -1;
                }

                account.Balance += transaction.Amount;

                //add transaction
                account.TransactionHistory.Add(new TransactionModel
                {
                    TransactionId = _transactionDatabase.Count + 1, // Generating a unique transaction ID
                    Date = DateTime.Now,
                    Description = transaction.Description,
                    Amount = transaction.Amount,
                    Type = transaction.Type
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
        public List<TransactionModel> ViewTransactionHistory(AccountModel account)
        {
            return account.TransactionHistory;
        }


        /// <summary>
        /// Verifies if a transaction can be performed based on the current balance, transaction type, and amount.
        /// </summary>
        /// <param name="balance">The current balance in the account.</param>
        /// <param name="transactionType">The type of the transaction (e.g., Debit, Credit).</param>
        /// <param name="amount">The amount of the transaction.</param>
        /// <returns>True if the transaction can be performed, otherwise false.</returns>
        private bool VerifyTransaction(decimal balance, TransactionType transactionType, decimal amount)
        {
            // If the transaction type is Debit and the resulting balance after the transaction would be <= 10, return false
            if (transactionType == TransactionType.D && balance - amount <= 10)
            {
                return false;
            }
            return true;
        }

        #endregion
    }

}