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
    public class EmployeesController : ApiController
    {
        // GET: api/AllEmployees
        [BasicAuthorization(Roles = "SuperAdmin")]
        [Route("emp")]
        [HttpGet]
        public List<Employees>  GetEmployees()
        {
            return Employees.EmployeesDetails();
        }

        // GET: api/AllEmployees
        [HttpGet]
        [Route("GetFewEmployees")]
        [BasicAuthorization(Roles = "SuperAdmin,Admin")]
        public HttpResponseMessage GetFewEmployees()
        {
            return Request.CreateResponse(Employees.EmployeesDetails().Where( emp => emp.Id < 3));
        }
        
        // GET: api/AllEmployees
        [Route("GetEmployee/{Id}")]
        [BasicAuthorization(Roles = "SuperAdmin,Admin,User")]
        public List<Employees> GetEmployee(int Id)
        {
            return Employees.EmployeesDetails();
        }
        
    }
}
