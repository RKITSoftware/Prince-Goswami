using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.Models.V1;
using ATM_Simulation_Demo.BAL.Interface.V1;

namespace ATM_Simulation_Demo.DAL.Transaction.V1
{
    public class TransactionService : IBLTransactionService
    {
        private readonly IBLAccountRepository _userRepository;
        private readonly IBLTransactionRepository _transactionRepository;

        public TransactionService(IBLAccountRepository userRepository, IBLTransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// Adds a transaction to the user's transaction history.
        /// </summary>
        /// <param name="user">The user to add the transaction for.</param>
        /// <param name="transaction">The transaction to add.</param>
        public void AddTransaction(AccountModel user, decimal Amount, string Description)
        {
            try
            {
                if (user != null)
                {
                    TransactionModel transaction = new TransactionModel();
                    user.TransactionHistory.Add(transaction);
                    _userRepository.GetAccountByID(user.Id);
                }
                else
                {
                    // Handle the case where the user is null (not found)
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
        /// View transaction history for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>List of transactions in the user's history.</returns>
        public List<TransactionModel> ViewTransactionHistory(AccountModel user)
        {
            try
            {
                return user?.TransactionHistory ?? new List<TransactionModel>();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }


    }

}