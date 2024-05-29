using System;
using System.Collections.Generic;
using System.Linq;


namespace LINQ
{

    /// <summary>
    /// Represents a program to manage a list of employees.
    /// </summary>
    class List
    {
        /// <summary>
        /// Runs the main functionality of the program.
        /// </summary>

        static void Run()
        {
            // Create a list of employees with some sample data
            List<Employee> employees = new List<Employee>
        {
            new Employee { ID = 1, Name = "Alice", Department = "HR", Salary = 50000 },
            new Employee { ID = 2, Name = "Bob", Department = "IT", Salary = 60000 },
            new Employee { ID = 3, Name = "Charlie", Department = "Finance", Salary = 55000 },
            new Employee { ID = 4, Name = "David", Department = "IT", Salary = 65000 },
            new Employee { ID = 5, Name = "Eva", Department = "HR", Salary = 48000 },
            new Employee { ID = 6, Name = "Frank", Department = "Finance", Salary = 70000 }
        };


            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Print Employees of specific department");
                Console.WriteLine("2. Print Sorted Employees by Name");
                Console.WriteLine("3. Print Highest Paid Employee");
                Console.WriteLine("4. Exit");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        string department = Console.ReadLine();
                        var Employees = employees.Where(emp => emp.Department == department);
                        PrintEmployees(department, Employees);
                        break;
                    case 2:
                        var sortedEmployees = employees.OrderBy(emp => emp.Name);
                        PrintEmployees("Sorted", sortedEmployees);
                        break;
                    case 3:
                        var highestPaidEmployee = employees.OrderByDescending(emp => emp.Salary).First();
                        Console.WriteLine($"Highest Paid Employee: {highestPaidEmployee.Name} ({highestPaidEmployee.Salary})");
                        break;
                    case 4:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                        break;
                }
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Prints a list of employees based on the specified department.
        /// </summary>
        /// <param name="employeeType">The type of employees to print.</param>
        /// <param name="employeeList">The list of employees to print.</param>

        static void PrintEmployees(string employeeType, IEnumerable<Employee> employeeList)
        {
            // Display the result
            Console.WriteLine($"{employeeType} Employees:");
            foreach (var employee in employeeList)
            {
                Console.WriteLine($"{employee.ID} - {employee.Name} ({employee.Department})");
            }
        }
    }

}