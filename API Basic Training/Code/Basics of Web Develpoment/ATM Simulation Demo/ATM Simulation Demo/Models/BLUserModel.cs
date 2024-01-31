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
    public class BLUserModel
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
}
