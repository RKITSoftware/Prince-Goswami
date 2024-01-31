using ATM_Simulation_Demo.Models;
using System.Collections.Generic;

namespace ATM_Simulation_Demo.BAL.Interface
{
    /// <summary>
    /// Interface for managing user-related operations in the business logic layer.
    /// </summary>
    public interface IBLUserService
    {
        /// <summary>
        /// Retrieves a user by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        BLUserModel GetUserByID(int userId);

        /// <summary>
        /// Retrieves a user by their card number and PIN.
        /// </summary>
        /// <param name="cardNumber">The card number of the user.</param>
        /// <param name="pin">The PIN of the user.</param>
        /// <returns>The user object if found, null otherwise.</returns>
        BLUserModel GetUser(string cardNumber, string pin);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="name">The name of the user.</param>
        /// <param name="mobileNumber">The mobile number of the user.</param>
        /// <param name="dob">The date of birth of the user.</param>
        /// <returns>The newly created user.</returns>
        BLUserModel CreateUser(string name, string mobileNumber, string dob);

        /// <summary>
        /// Changes the PIN for a user.
        /// </summary>
        /// <param name="user">The user to change the PIN for.</param>
        /// <param name="currentPin">The current PIN of the user.</param>
        /// <param name="newPin">The new PIN to set.</param>
        void ChangePin(BLUserModel user, string currentPin, string newPin);

        /// <summary>
        /// Updates the mobile number for a user.
        /// </summary>
        /// <param name="user">The user to update the mobile number for.</param>
        /// <param name="newMobileNumber">The new mobile number to set.</param>
        void UpdateMobileNumber(BLUserModel user, string newMobileNumber);

        /// <summary>
        /// Displays all users for testing purposes.
        /// </summary>
        /// <returns>A list of all users.</returns>
        List<BLUserModel> DisplayAllUsers();
    }
}
