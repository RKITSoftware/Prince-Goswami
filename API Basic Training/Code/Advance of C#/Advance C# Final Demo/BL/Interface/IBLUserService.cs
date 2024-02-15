

using Advance_C__Final_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Advance_C__Final_Demo.BL.Interface
{
    public interface IBLUserService
    {
        /// <summary>
        /// Get user details by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user details.</returns>
        USR01 GetUserDetails(int userId);

        /// <summary>
        /// Get user details by card number.
        /// </summary>
        /// <param name="cardNumber">The card number of the user to retrieve.</param>
        /// <returns>The user details.</returns>
        USR01 GetUserDetailsByCardNumber(string cardNumber);

        /// <summary>
        /// Perform a deposit transaction for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="amount">The amount to deposit.</param>
        /// <returns>The updated user balance after the deposit.</returns>
        decimal Deposit(int userId, decimal amount);

        /// <summary>
        /// Perform a withdrawal transaction for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="amount">The amount to withdraw.</param>
        /// <returns>The updated user balance after the withdrawal.</returns>
        decimal Withdraw(int userId, decimal amount);

        /// <summary>
        /// Get the transaction history for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The list of transactions for the user.</returns>
        List<TRN01> GetTransactionHistory(int userId);

        /// <summary>
        /// Get user details by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user details.</returns>
        void Delete(int userId);

        /// <summary>
        /// Get user details by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user details.</returns>
        void AddUser(USR01 newUser);

        // Add other necessary methods for user-related services
    }

}