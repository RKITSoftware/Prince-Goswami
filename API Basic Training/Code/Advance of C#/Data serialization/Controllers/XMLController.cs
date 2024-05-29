using Data_serialization.Models;
using System.IO;
using System.Web.Http;
using System.Xml.Serialization;

namespace Data_serialization.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class XMLController : ApiController
    {
        /// <summary>
        /// Converts a Person object to XML string.
        /// </summary>
        /// <returns>OK with the XML string representation of the Person object.</returns>
        [HttpGet]
        [Route("XmlToString")]
        public IHttpActionResult XmlToString()
        {
            // XML to String
            Person person = new Person { Name = "Charlie", Age = 22 };
            XmlSerializer serializer = new XmlSerializer(typeof(Person));
            using (StringWriter stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, person);
                string xmlString = stringWriter.ToString();
                return Ok(xmlString);
            }
        }

        /// <summary>
        /// Converts XML string to a Person object.
        /// </summary>
        /// <param name="xmlInput">XML string representation of the Person object.</param>
        /// <returns>OK with the deserialized Person object.</returns>
        [HttpPost]
        [Route("StringToXml")]
        public IHttpActionResult StringToXml([FromBody] string xmlInput)
        {
            // String to XML
            XmlSerializer serializer = new XmlSerializer(typeof(Person));
            using (StringReader stringReader = new StringReader(xmlInput))
            {
                Person deserializedPerson = (Person)serializer.Deserialize(stringReader);
                return Ok(deserializedPerson);
            }
        }
    }
}

