using System;
using System.Data;

class _DataTable
{
    static void Main()
    {
        Console.WriteLine("DataTable Program:");

        // Create a DataTable with columns
        DataTable employeeTable = new DataTable("Employee");


        // Create the primary key column
        DataColumn primaryKeyColumn = new DataColumn("EmployeeID", typeof(int));
        primaryKeyColumn.AutoIncrement = true;
        primaryKeyColumn.AllowDBNull = false;
        primaryKeyColumn.Unique = true;

        // Add the primary key column and other columns
        employeeTable.Columns.Add(primaryKeyColumn);
        employeeTable.Columns.Add("FirstName", typeof(string));
        employeeTable.Columns.Add("LastName", typeof(string));
        employeeTable.Columns.Add("Position", typeof(string));

        // Set the primary key constraint
        employeeTable.PrimaryKey = new DataColumn[] { primaryKeyColumn };


        // Add data to the DataTable
        employeeTable.Rows.Add(1, "John", "Doe", "Developer");
            employeeTable.Rows.Add(2, "Jane", "Smith", "Manager");
        employeeTable.Rows.Add(3, "Bob", "Johnson", "Tester");

        // Display the contents of the DataTable
        DisplayDataTable(employeeTable);

        // Remove data from DataTable
        employeeTable.Rows.Remove( employeeTable.Rows.Find(3));
        
        
        // Display the contents of the DataTable
        DisplayDataTable(employeeTable);
        Console.ReadLine();
    }

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
