using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LINQ
{
    /// <summary>
    /// Represents a program demonstrating LINQ queries with DataTable.
    /// </summary>
    class Program
    {
        static void Main()
        {
            // Create a DataTable with some sample data
            DataTable dataTable = new DataTable("Employees");
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Department", typeof(string));

            dataTable.Rows.Add(1, "Alice", "HR");
            dataTable.Rows.Add(2, "Bob", "IT");
            dataTable.Rows.Add(3, "Charlie", "Finance");

            // LINQ query to select employees from the IT department
            var itEmployees = from DataRow row in dataTable.Rows
                              where row.Field<string>("Department") == "IT"
                              select new
                              {
                                  ID = row.Field<int>("ID"),
                                  Name = row.Field<string>("Name"),
                                  Department = row.Field<string>("Department")
                              };
            printEmployee("IT", itEmployees);

            // LINQ query to select employees from the Finance department
            var financeEmployees = dataTable.AsEnumerable()
                                .Where(row => row.Field<string>("Department") == "Finance")
                                .Select(row => new
                                {
                                    ID = row.Field<int>("ID"),
                                    Name = row.Field<string>("Name"),
                                    Department = row.Field<string>("Department")
                                });
            printEmployee("Finance", financeEmployees);

            // LINQ query to sort employees by name
            var sortedEmployees = dataTable.AsEnumerable()
                                           .OrderBy(row => row.Field<string>("Name"))
                                           .Select(row => new
                                           {
                                               ID = row.Field<int>("ID"),
                                               Name = row.Field<string>("Name"),
                                               Department = row.Field<string>("Department")
                                           });
            printEmployee("Sorted", sortedEmployees);


            Console.ReadLine();
        }

        /// <summary>
        /// Prints the list of employees based on the specified type and employee list.
        /// </summary>
        /// <param name="employeeType">The type of employees being printed.</param>
        /// <param name="employeeList">The list of employees to print.</param>

        static void printEmployee(string employeeType, dynamic employeeList)
        {
            // Display the result
            Console.WriteLine(employeeType + " Employees:");
            foreach (var employee in employeeList)
            {
                Console.WriteLine($"{employee.ID} - {employee.Name} ({employee.Department})");
            }
        }
    }
}
