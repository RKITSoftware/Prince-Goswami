using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.BAL;
using ATM_Simulation_Demo.DAL.User;
using ATM_Simulation_Demo.Models;

namespace ATM_Simulation_Demo.DAL.Transaction
{
    public class TransactionService
    {
        private readonly UserRepository _userRepository;

        public TransactionService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Adds a transaction to the user's transaction history.
        /// </summary>
        /// <param name="user">The user to add the transaction for.</param>
        /// <param name="transaction">The transaction to add.</param>
        public void AddTransaction(BLUserModel user, BLTransactionModel transaction)
        {
            try
            {
                if (user != null)
                {
                    user.TransactionHistory.Add(transaction);
                    _userRepository.UpdateUser(user);
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
        public List<BLTransactionModel> ViewTransactionHistory(BLUserModel user)
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