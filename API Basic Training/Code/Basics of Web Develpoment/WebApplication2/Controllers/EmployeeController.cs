using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApplication2.Models;
using System.Web.Http.Cors;

namespace WebApplication2.Controllers
{
    [EnableCors("*", "*", "*")]
    public class EmployeesController : ApiController
    {
        public static List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Aarti", MobileNo = 7894561230, City = "ThanGadh", isActive = true },
            new Employee { Id = 2, Name = "Jeet", MobileNo = 1234567890, City = "Tankara", isActive = true }
        };

        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns>List of employees.</returns>
        [DisableCors]
        [HttpGet]
        [Route("api/employees")]
        public IHttpActionResult Get()
        {
            return Ok(employees);
        }

        /// <summary>
        /// Gets an employee by ID.
        /// </summary>
        /// <param name="Id">The ID of the employee.</param>
        /// <returns>The employee with the specified ID.</returns>
        [HttpGet]
        [Route("api/employees/{Id}")]
        public IHttpActionResult GetById(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest("Invalid ID. ID must be greater than 0.");
            }

            return Ok(employees.FirstOrDefault(e => e.Id == Id));
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="employee">The employee object to create.</param>
        /// <returns>The newly created employee.</returns>
        [HttpPost]
        [Route("api/employees")]
        public IHttpActionResult Post(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Value cannot be null or empty.");
            }

            // Simulating data creation
            // You can process the received data and return a response
            employees.Add(employee);
            return CreatedAtRoute("DefaultApi", new { controller = "Employees", id = employee.Id }, employee);
        }

        /// <summary>
        /// Updates an employee.
        /// </summary>
        /// <param name="id">The ID of the employee to update.</param>
        /// <param name="employee">The updated employee object.</param>
        /// <returns>HTTP status code indicating the success of the operation.</returns>
        [HttpPut]
        [Route("api/employees/{id}")]
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

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>HTTP status code indicating the success of the operation.</returns>
        [HttpDelete]
        [Route("api/employees/{id}")]
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

        [Route("employeeFromBody/")]
        [HttpGet]
        public IHttpActionResult GetFromBody([FromBody]Employee employee)
        {
            return Ok(employee);
        }
    }
}
