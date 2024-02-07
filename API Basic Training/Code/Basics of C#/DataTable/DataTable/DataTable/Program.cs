using System;
using System.Data;

/// <summary>
/// Namespace declaration for the DataTable project
/// <summary>
namespace Datatable
{
    /// <summary>
    /// Main class of the program
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the program
        /// </summary>
        static void Main()
        {
            // Display a title for the program
            Console.WriteLine("DataTable Program:");

            // Create a DataTable to store employee information
            DataTable employeeTable = new DataTable("Employee");

            // Create the primary key column
            DataColumn primaryKeyColumn = new DataColumn("EmployeeID", typeof(int));
            primaryKeyColumn.AutoIncrement = true;
            primaryKeyColumn.AllowDBNull = false;
            primaryKeyColumn.Unique = true;

            // Add columns to the DataTable
            employeeTable.Columns.Add(primaryKeyColumn);
            employeeTable.Columns.Add("FirstName", typeof(string));
            employeeTable.Columns.Add("LastName", typeof(string));
            employeeTable.Columns.Add("Position", typeof(string));

            // Set the primary key constraint
            employeeTable.PrimaryKey = new DataColumn[] { primaryKeyColumn };

            // Add sample data to the DataTable
            employeeTable.Rows.Add(1, "John", "Doe", "Developer");
            employeeTable.Rows.Add(2, "Jane", "Smith", "Manager");
            employeeTable.Rows.Add(3, "Bob", "Johnson", "Tester");

            // Display the contents of the DataTable
            DisplayDataTable(employeeTable);

            // Remove a row from the DataTable
            employeeTable.Rows.Remove(employeeTable.Rows.Find(3));

            // Display the updated contents of the DataTable
            DisplayDataTable(employeeTable);

            // Wait for user input before exiting
            Console.ReadLine();
        }

        /// <summary>
        /// Method to display the contents of a DataTable
        /// </summary>
        /// <param>
        /// DataTable to Print
        /// </param>
        static void DisplayDataTable(DataTable dataTable)
        {
            Console.WriteLine($"Table Name: {dataTable.TableName}");
            Console.WriteLine("--------------------------------------------------");

            // Display column names
            foreach (DataColumn column in dataTable.Columns)
            {
                Console.Write($"{column.ColumnName,-15}");
            }
            Console.WriteLine("\n--------------------------------------------------");

            // Display data rows
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write($"{item,-15}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("--------------------------------------------------");
        }
    }
}