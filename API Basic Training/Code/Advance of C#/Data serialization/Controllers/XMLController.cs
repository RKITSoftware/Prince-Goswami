
using System.IO;
using System.Web.Http;
using System.Xml.Serialization;
using Data_serialization.Models;

namespace Data_serialization.Controllers
{

    [RoutePrefix("api/[controller]")]
    public class XMLController : ApiController
    {
        [HttpGet]
        public IHttpActionResult XmlToString()
        {
            // XML to String
            var person = new Person { Name = "Charlie", Age = 22 };
            var serializer = new XmlSerializer(typeof(Person));
            using (var stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, person);
                string xmlString = stringWriter.ToString();
                return Ok(xmlString);
            }
        }

        [HttpPost]
        public IHttpActionResult StringToXml([FromBody] string xmlInput)
        {
            // String to XML
            var serializer = new XmlSerializer(typeof(Person));
            using (var stringReader = new StringReader(xmlInput))
            {
                var deserializedPerson = (Person)serializer.Deserialize(stringReader);
                return Ok(deserializedPerson);
            }
        }
    }

}
