
using Advance_C__Final_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advance_C__Final_Demo.BL.Interface
{
        /// <summary>
        /// Interface for managing transactions in the system.
        /// </summary>
        public interface IBLTransactionService
        {
            /// <summary>
            /// Gets a transaction by its ID.
            /// </summary>
            /// <param name="transactionId">The ID of the transaction to retrieve.</param>
            /// <returns>The transaction with the specified ID.</returns>
            TRN01 GetTransactionById(int transactionId);

            /// <summary>
            /// Gets a list of transactions for a specific user.
            /// </summary>
            /// <param name="userId">The ID of the user for whom transactions are retrieved.</param>
            /// <returns>A list of transactions for the specified user.</returns>
            List<TRN01> GetTransactionsByUserId(int userId);

            /// <summary>
            /// Adds a new transaction to the system.
            /// </summary>
            /// <param name="transaction">The transaction to add.</param>
            void AddTransaction(TRN01 transaction);

            /// <summary>
            /// Deletes a transaction from the system.
            /// </summary>
            /// <param name="transactionId">The ID of the transaction to delete.</param>
            void DeleteTransaction(int transactionId);

            // Add other necessary methods for transaction-related services
        }
    }


