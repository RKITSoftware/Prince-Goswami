using System.Web.Http;
using Data_serialization.Models;
using Newtonsoft.Json;

namespace Data_serialization.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : ApiController
    {
        /// <summary>
        /// Converts a Person object to JSON string.
        /// </summary>
        /// <returns>OK with the JSON string representation of the Person object.</returns>
        [HttpGet]
        [Route("JsonToString")]
        public IHttpActionResult JsonToString()
        {
            // JSON to String
            var person = new Person { Name = "Alice", Age = 28 };
            string jsonString = JsonConvert.SerializeObject(person);
            return Ok(jsonString);
        }

        /// <summary>
        /// Converts JSON string to a Person object.
        /// </summary>
        /// <param name="jsonInput">JSON string representation of the Person object.</param>
        /// <returns>OK with the deserialized Person object.</returns>
        [HttpPost]
        [Route("StringToJson")]
        public IHttpActionResult StringToJson([FromBody] string jsonInput)
        {
            // String to JSON
            var deserializedPerson = JsonConvert.DeserializeObject<Person>(jsonInput);
            return Ok(deserializedPerson);
        }
    }
}
