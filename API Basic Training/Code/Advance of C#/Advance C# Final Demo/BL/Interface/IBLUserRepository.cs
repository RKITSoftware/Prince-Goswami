using Advance_C__Final_Demo.Models;
using System.Collections.Generic;

namespace Advance_C__Final_Demo.BL.Interface
{
    /// <summary>
    /// Interface for managing users in the database.
    /// </summary>
    public interface IBLUserRepository
    {
        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        USR01 GetUserById(int userId);

        /// <summary>
        /// Gets a user by their card number.
        /// </summary>
        /// <param name="cardNumber">The card number of the user to retrieve.</param>
        /// <returns>The user with the specified card number.</returns>
        USR01 GetUserByCardNumber(string cardNumber);

        /// <summary>
        /// Updates the balance of a user.
        /// </summary>
        /// <param name="userId">The ID of the user to update.</param>
        /// <param name="newBalance">The new balance for the user.</param>
        void UpdateUserBalance(int userId, decimal newBalance);

        /// <summary>
        /// Gets a list of all users in the system.
        /// </summary>
        /// <returns>A list of all users.</returns>
        List<USR01> GetAllUsers();

        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <param name="user">The user to add.</param>
        void AddUser(USR01 user);

        /// <summary>
        /// Deletes a user from the system.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        void DeleteUser(int userId);


        List<TRN01> GetTransactionHistory(int userId);

        // Add other necessary methods for user-related operations
    }

}
