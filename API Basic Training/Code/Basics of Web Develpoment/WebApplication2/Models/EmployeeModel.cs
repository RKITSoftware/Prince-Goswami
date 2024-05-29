using System;

namespace WebApplication2.Models
{
    /// <summary>
    /// Represents an employee entity.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the mobile number of the employee.
        /// </summary>
        public double MobileNo { get; set; }

        /// <summary>
        /// Gets or sets the city where the employee resides.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the employee is currently active.
        /// </summary>
        public bool isActive { get; set; }
    }
}
