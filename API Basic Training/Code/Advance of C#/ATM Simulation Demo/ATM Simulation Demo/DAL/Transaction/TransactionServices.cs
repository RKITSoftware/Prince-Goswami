using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.BAL.Interface;

namespace ATM_Simulation_Demo.DAL.Transaction
{
    public class TransactionService : IBLTransactionService
    {
        private readonly IBLAccountRepository _accountRepository;
        private readonly IBLTransactionRepository _transactionRepository;

        public TransactionService(IBLAccountRepository accountRepository, IBLTransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        }

        /// <inheritdoc />
        public decimal AddTransaction(int accountId, decimal amount, TransactionType TransactionType, string Description)
        {
            try
            {
                if (accountId > 0)
                {
                    TRN01 transaction = new TRN01
                    {
                        N01F02 = accountId,
                        N01F03 = TransactionType,
                        N01F04 = amount,
                        N01F06 = Description
                    }; 
                    var account = _transactionRepository.AddTransaction(_accountRepository.GetAccountByID(accountId), transaction);
                    return account.C01F06; // Return updated balance from the account
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
                throw;
            }
        }

        /// <inheritdoc />
        public List<TRN01> ViewTransactionHistory(int accountId)
        {
            try
            {
                return _transactionRepository.ViewTransactionHistory(_accountRepository.GetAccountByID(accountId)) ?? new List<TRN01>();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        /// <inheritdoc />
        public List<TRN01> GetAllTransactions()
        {
            try
            {
                return _transactionRepository.GetAllTransactions() ?? new List<TRN01>();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }
 
    }
}
