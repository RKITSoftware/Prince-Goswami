using Authentication.BAL;
using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Authentication.Controllers
{
    [BasicAuthentication]
    [RoutePrefix("api/Employees")]
    public class CLEmployeesController : ApiController
    {
        ///<summary>
        /// Retrieves a list of all employees.
        ///</summary>
        ///<returns>A list of Employees.</returns>
        [BasicAuthorization(Roles = "SuperAdmin")]
        [Route("GetEmployees")]
        [HttpGet]
        public List<Employees> GetEmployees()
        {
            return Employees.EmployeesDetails();
        }

        ///<summary>
        /// Retrieves a subset of employees based on specified criteria.
        ///</summary>
        ///<returns>An HttpResponseMessage containing the subset of employees.</returns>
        [HttpGet]
        [Route("GetFewEmployees")]
        [BasicAuthorization(Roles = "SuperAdmin,Admin")]
        public HttpResponseMessage GetFewEmployees()
        {
            return Request.CreateResponse(Employees.EmployeesDetails().Where(emp => emp.Id < 3));
        }

        ///<summary>
        /// Retrieves information about a specific employee.
        ///</summary>
        ///<param name="Id">The ID of the employee to retrieve.</param>
        ///<returns>A list containing the details of the specified employee.</returns>
        [Route("GetEmployee/{Id}")]
        [BasicAuthorization(Roles = "SuperAdmin,Admin,User")]
        public List<Employees> GetEmployee(int Id)
        {
            return Employees.EmployeesDetails();
        }

    }
}
