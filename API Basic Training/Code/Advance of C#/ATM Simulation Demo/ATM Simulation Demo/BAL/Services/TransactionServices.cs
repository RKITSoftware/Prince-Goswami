using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.DAL;

namespace ATM_Simulation_Demo.BAL.Services
{
    public class TransactionService : IBLTransactionService
    {
        private readonly IBLAccountRepository _accountRepository;
        private readonly IBLTransactionRepository _transactionRepository;
        private readonly IBLLimitService _limitService;

        public TransactionService(IBLAccountRepository accountRepository, IBLTransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _limitService = new LimitService();
        }

        /// <inheritdoc />
        public decimal AddTransaction(int accountId, decimal amount, TransactionType transactionType, string Description)
        {
            try
            {
                if (accountId > 0)
                {
                    if (transactionType == 0 && !_limitService.VerifyWithdrawal(accountId, amount))
                    {
                        throw new Exception("Daily Limit Exceeded!!!");
                    }
                    TRN01 transaction = new TRN01
                    {
                        N01F02 = accountId,
                        N01F03 = transactionType,
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
