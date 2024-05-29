using System.Collections.Generic;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.POCO;

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
        /// <param name="newUser">The user to be created.</param>
        /// <returns>The newly created user.</returns>
        USR01 CreateUser(USR01 newUser);

        /// <summary>
        /// Retrieves a user by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        USR01 GetUser(int userId);

        /// <summary>
        /// checks if user exists by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to check.</param>
        /// <returns>true if user exists.</returns>
        bool IsUserExists(int userId);


        /// <summary>
        /// Retrieves a user by their user ID.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>The updated user.</returns>
        USR01 UpdateUser(USR01 user);

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        List<USR01> GetAllUsers();

        /// <summary>
        /// Verifies the credentials of a user.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>True if the credentials are valid, otherwise false.</returns>
        bool VerifyUserCredentials(string userName, string password);


        /// <summary>
        /// Retrieves a user by their credentials.
        /// </summary>
        /// <param name="userName">The username of the user to retrieve.</param>
        /// <param name="password">The password of the user to retrieve.</param>
        /// <returns>The user with the specified username.</returns>
        USR01 GetUserByCredentials(string userName, string password);

        ///// <summary>
        ///// Changes the role of a user.
        ///// </summary>
        ///// <param name="user">The user whose role will be changed.</param>
        ///// <param name="newRole">The new role for the user.</param>
        //void ChangeRole(USR01 user, UserRole newRole);

        ///// <summary>
        ///// Changes the password of a user.
        ///// </summary>
        ///// <param name="user">The user whose password will be changed.</param>
        ///// <param name="currentPassword">The current password of the user.</param>
        ///// <param name="newPassword">The new password for the user.</param>
        ///// <returns>True if the password change was successful, otherwise false.</returns>
        //bool ChangePassword(USR01 user, string currentPassword, string newPassword);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        void DeleteUser(int userId);
    }
}
