using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication.SEC
{
    public class ValidateUser
    {
        public static bool isUser(string username, string password)
        {
            if(Employees.EmployeesDetails().Any(emp => emp.Name.Equals(username) && emp.Password.Equals(password)))
            {
                return true;
            }
            return false;
        }

        public static Employees Login(string username, string password)
        {
            return Employees.EmployeesDetails().FirstOrDefault(emp => emp.Name == username && emp.Password == password);
        }
    }
}