using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication.Models
{
    ///<summary>
    /// Represents an employee entity with basic information.
    ///</summary>
    public class Employees
    {
        ///<summary>
        /// Gets or sets the unique identifier of the employee.
        ///</summary>
        public int Id { get; set; }

        ///<summary>
        /// Gets or sets the name of the employee.
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        /// Gets or sets the password of the employee.
        ///</summary>
        public string Password { get; set; }

        ///<summary>
        /// Gets or sets the email address of the employee.
        ///</summary>
        public string Email { get; set; }

        ///<summary>
        /// Gets or sets the phone number of the employee.
        ///</summary>
        public string Phone { get; set; }

        ///<summary>
        /// Gets or sets the role of the employee.
        ///</summary>
        public string role { get; set; }

        ///<summary>
        /// Gets or sets a value indicating whether the employee is active or not.
        ///</summary>
        public bool isActive { get; set; }

        ///<summary>
        /// Retrieves a list of sample employee details.
        ///</summary>
        ///<returns>A list of Employees containing sample data.</returns>
        public static List<Employees> EmployeesDetails()
        {
            return new List<Employees>
            {
                new Employees { Id = 1, Name = "usr1", Password = "pswd", Email = "prince@p.pp", Phone = "1234567890", role = "User", isActive = true},
                new Employees { Id = 2, Name = "usr2", Password = "pswd", Email = "prince@p.pp", Phone = "1234567890", role = "User", isActive = true},
                new Employees { Id = 3, Name = "Admin", Password = "pswd", Email = "prince@p.pp", Phone = "1234567890", role = "Admin", isActive = false},
                new Employees { Id = 4, Name = "Super", Password = "pswd", Email = "prince@p.pp", Phone = "1234567890", role = "SuperAdmin", isActive = true}
            };
        }
    }
}
