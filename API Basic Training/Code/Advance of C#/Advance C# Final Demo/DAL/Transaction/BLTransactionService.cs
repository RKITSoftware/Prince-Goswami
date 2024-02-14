using Advance_C__Final_Demo.BL.Interface;

using Advance_C__Final_Demo.Models;
using System.Collections.Generic;

namespace Advance_C__Final_Demo.DAL.Transaction
{
    public class TransactionService : IBLTransactionService
    {
        private readonly IBLTransactionRepository _transactionRepository = new TransactionRepository();

        /// <inheritdoc />
        public TRN01 GetTransactionById(int transactionId)
        {
            return _transactionRepository.GetTransactionById(transactionId);
        }

        /// <inheritdoc />
        public List<TRN01> GetTransactionsByUserId(int userId)
        {
            return _transactionRepository.GetTransactionsByUserId(userId);
        }

        /// <inheritdoc />
        public void AddTransaction(TRN01 transaction)
        {
            _transactionRepository.AddTransaction(transaction);
        }

        /// <inheritdoc />
        public void DeleteTransaction(int transactionId)
        {
            _transactionRepository.DeleteTransaction(transactionId);
        }

        // Add other necessary methods for transaction-related services
    }
}
