using Advance_C__Final_Demo.BL.Enum;
using Advance_C__Final_Demo.BL.Interface;
using Advance_C__Final_Demo.DAL.Transaction;
using Advance_C__Final_Demo.Models;
using System;
using System.Collections.Generic;

namespace Advance_C__Final_Demo.DAL.User
{
    public class UserService : IBLUserService
    {
        private readonly IBLUserRepository _userRepository = new UserRepository();
        private readonly IBLTransactionService _transactionService; 

        public USR01 GetUserDetails(int userId)
        {
            return _userRepository.GetUserById(userId);
        }
        
        public void AddUser(USR01 newUser)
        {
            _userRepository.AddUser(newUser);
        } 
        
        public void Delete(int userId)
        {
            _userRepository.DeleteUser(userId);
        }

        public USR01 GetUserDetailsByCardNumber(string cardNumber)
        {
            return _userRepository.GetUserByCardNumber(cardNumber);
        }

        public decimal Deposit(int userId, decimal amount)
        {
            USR01 user = _userRepository.GetUserById(userId);

            if (user != null)
            {
                // Update user balance
                decimal newBalance = user.R01F05 + amount;
                _userRepository.UpdateUserBalance(userId, newBalance);

                // Log the transaction
                LogTransaction(userId, TransactionType.Credit, amount);

                return newBalance;
            }

            return 0; // User not found
        }

        public decimal Withdraw(int userId, decimal amount)
        {
            USR01 user = _userRepository.GetUserById(userId);

            if (user != null && user.R01F05 >= amount)
            {
                // Update user balance
                decimal newBalance = user.R01F05 - amount;
                _userRepository.UpdateUserBalance(userId, newBalance);

                // Log the transaction
                LogTransaction(userId, TransactionType.Debit, amount);

                return newBalance;
            }

            return 0; // Insufficient funds or user not found
        }

        public List<TRN01> GetTransactionHistory(int userId)
        {
            // Fetch and return the transaction history from the repository
            // You may want to implement additional logic for transaction history retrieval
            return _userRepository.GetTransactionHistory(userId);
        }

        private void LogTransaction(int userId, TransactionType transactionType, decimal amount)
        {
            // Create a new transaction and add it to the Transaction table
            TRN01 transaction = new TRN01
            {
                N01F02 = userId,
                N01F03 = transactionType,
                N01F04 = amount,
                N01F05 = DateTime.Now
            };

            _transactionService.AddTransaction(transaction);
        }
    }
}
