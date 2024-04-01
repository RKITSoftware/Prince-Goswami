
using Exception_Handling.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandling.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : Controller
{
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ILogger<CustomerController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("GetCustomers")]
    public IActionResult GetCustomers()
    {

        _logger.LogInformation("Getting customer details");

        var result = Customer.getCustomerDetails();
        if (result.Count == 0)
            throw new ApplicationException("Invalid Token");

        return Ok(result);

    }

    [HttpGet]
    [Route("Error")]
    public IActionResult Error()
    {
        int a = 10, b = 0;
        int c = a / b;
        return BadRequest("exception");
    }
}