using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApplication2.Models;
using System.Web.Http.Cors;

[EnableCors("*","*","*")]
public class EmployeesController : ApiController
{
    public static List<Employee> employees = new List<Employee>
    {
        new Employee { Id = 1, Name = "Aarti", MobileNo = 7894561230, City = "ThanGadh", isActive = true },
        new Employee { Id = 2, Name = "Jeet", MobileNo = 1234567890, City = "Tankara", isActive = true }
    };

    [DisableCors]
    // GET: api/employees
    public IHttpActionResult Get()
    {
        return Ok(employees);
    }

    // GET: api/sample/{id}
    public IHttpActionResult GetById(int Id)
    {
        if (Id <= 0)
        {
            return BadRequest("Invalid ID. ID must be greater than 0.");
        }

        return Ok(employees.FirstOrDefault(e => e.Id == Id));
    }

    // POST: api/sample
    public IHttpActionResult Post(Employee employee)
    {
        if (employee == null)
        {
            return BadRequest("Value cannot be null or empty.");
        }

        // Simulating data creation
        // You can process the received data and return a response
        employees.Add(employee);
        return CreatedAtRoute("DefaultApi", new { controller = "Sample", id = 1 }, employee);
    }

    // PUT: api/sample/{id}
    public IHttpActionResult Put(int id, [FromBody] Employee employee)
    {
        Employee emp = employees.FirstOrDefault(e => e.Id == id);
        if (id >= 0 && employee != null)
        {
            emp.Name = employee.Name;
            emp.MobileNo = employee.MobileNo;
            emp.City = employee.City;
            emp.isActive = employee.isActive;
            return Ok();
        }
        else
        {
            return BadRequest("Invalid ID. ID must be greater than 0.");
        }
    }

    // DELETE: api/sample/{id}
    public IHttpActionResult Delete(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid ID. ID must be greater than 0.");
        }

        employees.Remove(employees.FirstOrDefault(e => e.Id == id));
        // Simulating data deletion
        // You can delete data with the provided ID
        return Ok();
    }
}


//new Employee { Id = 3, Name = "KB", MobileNo = 1431431430, City = "Rajkot", isActive = true },
//        new Employee { Id = 4, Name = "MB", MobileNo = 1831831830, City = "Dhrol", isActive = false },