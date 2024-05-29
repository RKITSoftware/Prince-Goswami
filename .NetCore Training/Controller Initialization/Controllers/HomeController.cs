using Controller_Initialization.BL;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Controller_Initialization.Controllers
{
    [ApiController]
    [Route("api/Home")]
    public class HomeController : ControllerBase
    {
        private readonly IBLDateTime _dateTime;

        public HomeController(IBLDateTime dateTime)
        {
            _dateTime = dateTime;
        }

        [HttpGet(Name = "")]
        public IActionResult Index()
        {
            Console.WriteLine(_dateTime.guid);
            var serverTime = _dateTime.GetCurrentDateTime();
            if (serverTime.Hour < 12)
            {
                Console.WriteLine("It's morning here - Good Morning!\t"+serverTime);
            }
            else if (serverTime.Hour < 17)
            {
                Console.WriteLine("It's afternoon here - Good Afternoon!\t"+serverTime);
            }
            else
            {
                Console.WriteLine("It's evening here - Good Evening!\t"+serverTime);
            }
            return Ok();
        }

    }
}