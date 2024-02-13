using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LINQ
{

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

            // Display the result
            Console.WriteLine("IT Employees:");
            foreach (var employee in itEmployees)
            {
                Console.WriteLine($"{employee.ID} - {employee.Name} ({employee.Department})");
            }

            Console.ReadLine();
        }
    }

}


//using System;
//using System.Collections.Generic;
//using System.Linq;

//class Program
//{
//    static void Main()
//    {
//        // Create a List of employees
//        List<Employee> employees = new List<Employee>
//        {
//            new Employee { ID = 1, Name = "Alice", Department = "HR" },
//            new Employee { ID = 2, Name = "Bob", Department = "IT" },
//            new Employee { ID = 3, Name = "Charlie", Department = "Finance" }
//        };

//        // LINQ query to select employees from the IT department
//        var itEmployees = from employee in employees
//                          where employee.Department == "IT"
//                          select employee;

//        // Display the result
//        Console.WriteLine("IT Employees:");
//        foreach (var employee in itEmployees)
//        {
//            Console.WriteLine($"{employee.ID} - {employee.Name} ({employee.Department})");
//        }

//        Console.ReadLine();
//    }
//}

//// Employee class for demonstration
//class Employee
//{
//    public int ID { get; set; }
//    public string Name { get; set; }
//    public string Department { get; set; }
//}
