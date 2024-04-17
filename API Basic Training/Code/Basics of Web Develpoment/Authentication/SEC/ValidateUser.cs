using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication.SEC
{
    ///<summary>
    /// Provides methods for validating user credentials and performing user login operations.
    ///</summary>
    public class ValidateUser
    {
        ///<summary>
        /// Validates whether a user with the given username and password exists.
        ///</summary>
        ///<param name="username">The username of the user to validate.</param>
        ///<param name="password">The password of the user to validate.</param>
        ///<returns>True if a user with the specified username and password exists; otherwise, false.</returns>
        public static bool isUser(string username, string password)
        {
            if (Employees.EmployeesDetails().Any(emp => emp.Name.Equals(username) && emp.Password.Equals(password)))
            {
                return true;
            }
            return false;
        }

        ///<summary>
        /// Logs in a user with the given username and password.
        ///</summary>
        ///<param name="username">The username of the user to log in.</param>
        ///<param name="password">The password of the user to log in.</param>
        ///<returns>The Employee object representing the logged-in user, or null if the login fails.</returns>
        public static Employees Login(string username, string password)
        {
            return Employees.EmployeesDetails().FirstOrDefault(emp => emp.Name == username && emp.Password == password);
        }
    }
}
