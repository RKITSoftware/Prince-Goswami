using ATM_Simulation_Demo.Models;
using ServiceStack.DataAnnotations;
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
    [Alias("USR01")]
    public class USR01
    {
        /// <summary>
        /// User ID (Primary Key, Auto Incremented)
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int R01F01 { get; set; }

        /// <summary>
        /// User Name (Not Null)
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Mobile Number (Not Null)
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// Date of Birth (Not Null)
        /// </summary>
        public DateTime R01F04 { get; set; }

        /// <summary>
        /// Role (Not Null)
        /// </summary>
        public UserRole R01F05 { get; set; }

        /// <summary>
        /// Password (Not Null)
        /// </summary>
        public string R01F06 { get; set; }


    }
}
