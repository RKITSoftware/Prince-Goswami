using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.DTO;
using ATM_Simulation_Demo.Models.POCO;
using System;
using System.Collections.Generic;

namespace ATM_Simulation_Demo.BAL.Interface 
{
    /// <summary>
    /// Interface for managing user-related operations in the business logic layer.
    /// </summary>
    public interface IBLUserService : IDataHandlerService<DTOUSR01>
    {
        Response PreValidation(DTOUSR01 objDTOACC01);

        /// <summary>
        /// Retrieves a user by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        Response GetUserByID(int userId);

        /// <summary>
        /// Retrieves a user by their username and password.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The user with the specified username and password.</returns>
        USR01 GetUserByCredentials(string userName, string password);

        
        /// <summary>
        /// Changes the password for a user.
        /// </summary>
        /// <param name="user">The user whose password will be changed.</param>
        /// <param name="currentPassword">The current password of the user.</param>
        /// <param name="newPassword">The new password for the user.</param>
        Response ChangePassword(int userId, string currentPassword, string newPassword);

        /// <summary>
        /// Updates the role for a user.
        /// </summary>
        /// <param name="user">The user whose role will be updated.</param>
        /// <param name="newRole">The new role for the user.</param>
        Response UpdateRole(int userId, enmUserRole newRole);

        /// <summary>
        /// Deletes a user by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        Response DeleteUser(int userId);

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        Response GetAllUsers();
    }
}
