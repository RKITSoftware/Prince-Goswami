using ATM_Simulation_Demo.Models;
using System;

namespace ATM_Simulation_Demo
{
    /// <summary>
    /// Enum representing user roles.
    /// </summary>
    public enum UserRole
    {
        User,
        Admin,
        DEO,
        Customer,
        // Add more roles as needed
    }

    /// <summary>
    /// Model representing user details.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Gets or sets the user's unique identifier.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user's mobile number.
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the user's date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the user's role (e.g., Admin, DEO).
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string Password { get; set; }

    }

    #region Request Models

    /// <summary>
    /// Represents a request model for changing a user's password.
    /// </summary>
    public class ChangePasswordRequest
    {
        /// <summary>
        /// The ID of the user whose password is being changed.
        /// </summary>
        public int userId { get; set; }

        /// <summary>
        /// The current password of the user.
        /// </summary>
        public string currentPassword { get; set; }

        /// <summary>
        /// The new password to be set for the user.
        /// </summary>
        public string newPassword { get; set; }
    }

    /// <summary>
    /// Represents a request model for creating a new user.
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// The username of the new user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The mobile number of the new user.
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// The password of the new user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The date of birth of the new user.
        /// </summary>
        public DateTime DOB { get; set; }

        /// <summary>
        /// The role of the new user.
        /// </summary>
        public UserRole Role { get; set; }
    }

    /// <summary>
    /// Represents a request model for updating a user's role.
    /// </summary>
    public class UpdateRoleRequest
    {
        /// <summary>
        /// The ID of the user whose role is being updated.
        /// </summary>
        public int userId { get; set; }

        /// <summary>
        /// The new role to be assigned to the user.
        /// </summary>
        public UserRole newRole { get; set; }
    }

    #endregion

}
