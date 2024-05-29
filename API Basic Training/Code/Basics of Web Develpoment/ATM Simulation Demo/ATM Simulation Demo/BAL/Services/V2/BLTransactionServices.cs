using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.Models.V2;
using ATM_Simulation_Demo.BAL.Interface.V2;

namespace ATM_Simulation_Demo.DAL.Transaction.V2
{
    public class TransactionService : IBLTransactionService
    {
        private readonly IBLAccountRepository _accountRepository;
        private readonly IBLTransactionRepository _transactionRepository;

        public TransactionService(IBLAccountRepository accountRepository, IBLTransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// Adds a transaction to the account's transaction history.
        /// </summary>
        /// <param name="account">The account to add the transaction for.</param>
        /// <param name="transaction">The transaction to add.</param>
        public decimal AddTransaction(int accountId, TransactionModel transaction)
        {
            try
            {
                if (accountId > 0)
                {
                    var account = _transactionRepository.AddTransaction(_accountRepository.GetAccountByID(accountId), transaction);
                    _accountRepository.UpdateAccount(account);
                    return account.Balance;
                }
                else
                {
                    // Handle the case where the account is null (not found)
                    throw new Exception("User not found");
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        /// <summary>
        /// View transaction history for a account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>List of transactions in the account's history.</returns>
        public List<TransactionModel> ViewTransactionHistory(int accountId)
        {
            try
            {
                return _accountRepository.GetAccountByID(accountId)?.TransactionHistory ?? new List<TransactionModel>();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

    }

}