using System.Collections.Generic;
using ATM_Simulation_Demo.Models;

namespace ATM_Simulation_Demo.BAL.Interface
{
    /// <summary>
    /// Interface for managing user-related operations in the business logic layer.
    /// </summary>
    public interface IBLUserRepository
    {
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="role">The role of the user.</param>
        /// <returns>The newly created user.</returns>
        BLUserModel CreateUser(string userName, string password, UserRole role);

        /// <summary>
        /// Retrieves a user by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        BLUserModel GetUser(int userId);

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        List<BLUserModel> GetAllUsers();

        /// <summary>
        /// Verifies the credentials of a user.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>True if the credentials are valid, otherwise false.</returns>
        bool VerifyUserCredentials(string userName, string password);

        /// <summary>
        /// Changes the role of a user.
        /// </summary>
        /// <param name="user">The user whose role will be changed.</param>
        /// <param name="newRole">The new role for the user.</param>
        void ChangeRole(BLUserModel user, UserRole newRole);

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="userName">The username of the user to retrieve.</param>
        /// <returns>The user with the specified username.</returns>
        BLUserModel GetUserByUserName(string userName);

        /// <summary>
        /// Changes the password of a user.
        /// </summary>
        /// <param name="user">The user whose password will be changed.</param>
        /// <param name="currentPassword">The current password of the user.</param>
        /// <param name="newPassword">The new password for the user.</param>
        /// <returns>True if the password change was successful, otherwise false.</returns>
        bool ChangePassword(BLUserModel user, string currentPassword, string newPassword);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        void DeleteUser(int userId);
    }
}
