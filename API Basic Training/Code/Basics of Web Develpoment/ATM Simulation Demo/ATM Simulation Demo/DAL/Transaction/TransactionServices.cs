﻿using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.BAL;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.BAL.ATM_Simulation_Demo.BAL.Interface;

namespace ATM_Simulation_Demo.DAL.Transaction
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
        public void AddTransaction(BLAccountModel user, BLTransactionModel transaction)
        {
            try
            {
                if (user != null)
                {
                    user.TransactionHistory.Add(transaction);
                    _userRepository.UpdateAccount(user);
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
                throw;
            }
        }

        /// <summary>
        /// View transaction history for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>List of transactions in the user's history.</returns>
        public List<BLTransactionModel> ViewTransactionHistory(BLAccountModel user)
        {
            try
            {
                return user?.TransactionHistory ?? new List<BLTransactionModel>();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }


    }

}