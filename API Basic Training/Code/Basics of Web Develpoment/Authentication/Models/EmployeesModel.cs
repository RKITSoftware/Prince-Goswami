using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string role { get; set; }
        public bool isActive { get; set; }

        public static List<Employees> EmployeesDetails()
        {
            return new List<Employees>
            {
                new Employees { Id = 1, Name = "usr1", Password = "pswd", Email = "prince@p.pp", Phone = "1234567890",role = "User", isActive = true},
                new Employees { Id = 2, Name = "usr2", Password = "pswd", Email = "prince@p.pp", Phone = "1234567890",role = "User", isActive = true},
                new Employees { Id = 3, Name = "Adm", Password = "pswd", Email = "prince@p.pp", Phone = "1234567890",role = "Admin", isActive = false},
                new Employees { Id = 4, Name = "Super", Password = "pswd", Email = "prince@p.pp", Phone = "1234567890",role = "SuperAdmin", isActive = true}
            };
        }
    }
}