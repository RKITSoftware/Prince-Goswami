using ATM_Simulation_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Simulation_Demo.BAL
{

    /// <summary>
    /// Interface for managing the user repository.
    /// </summary>
    public interface IBLUserRepository
    {
        /// <summary>
        /// Adds a new user to the repository.
        /// </summary>
        /// <param name="newUser">The new user to add.</param>
        void AddUser(BLUserModel newUser);

        /// <summary>
        /// Update current user with new user to the repository.
        /// </summary>
        /// <param name="newUser">The new user to add.</param>
        void UpdateUser(BLUserModel newUser);

        /// <summary>
        /// Retrieves a user based on card number and PIN.
        /// </summary>
        /// <param name="cardNumber">The card number of the user.</param>
        /// <param name="pin">The PIN of the user.</param>
        /// <returns>The user object if found, null otherwise.</returns>
        BLUserModel GetUser(string cardNumber, string pin);

        /// <summary>
        /// Checks if a user with the given card number exists in the repository.
        /// </summary>
        /// <param name="cardNumber">The card number to check.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        bool IsCardNumberExists(string cardNumber);

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
        /// Retrieves a list of all users in the repository.
        /// </summary>
        /// <returns>A list of all users.</returns>
        List<BLUserModel> GetAllUsers();
    }

}