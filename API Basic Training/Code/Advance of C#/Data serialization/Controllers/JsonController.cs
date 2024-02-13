using System.Web.Http;
using System.Web.Http.Controllers;
using Data_serialization.Models;
using Newtonsoft.Json;

namespace Data_serialization.Controllers
{


    [Route("api/[controller]")]
    public class PersonController : ApiController
    {
        [HttpGet]
        public IHttpActionResult JsonToString()
        {
            // JSON to String
            var person = new Person { Name = "Alice", Age = 28 };
            string jsonString = JsonConvert.SerializeObject(person);
            return Ok(jsonString);
        }

        [HttpPost]
        public IHttpActionResult StringToJson([FromBody] string jsonInput)
        {
            // String to JSON
            var deserializedPerson = JsonConvert.DeserializeObject<Person>(jsonInput);
            return Ok(deserializedPerson);
        }
    }

}
