using System.Collections.Generic;
namespace Exception_Handling.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }

        public static List<Customer> getCustomerDetails()
        {
            List<Customer> customers = new List<Customer>
        {
            //new Customer {Id = 1, Name = "jeet",MobileNumber = "1234567890" },
            //new Customer {Id = 2, Name = "dev",MobileNumber = "1234567890" },
            //new Customer {Id = 3, Name = "raj",MobileNumber = "1234567890" },
            //new Customer {Id = 4, Name = "deep",MobileNumber = "1234567890" },
            //new Customer {Id = 5, Name = "yash",MobileNumber = "1234567890" },
        };

            return customers;
        }
    }

}
