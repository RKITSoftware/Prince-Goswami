using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Versioning.Models;

namespace Versioning.Controllers
{
    /// <summary>
    /// Controller for handling version 1 of the demo API.
    /// </summary>
    [RoutePrefix("api/v1/demo")]
    public class CLProductsV1Controller : ApiController
    {
        /// <summary>
        /// Gets a list of products for version 1 of the API.
        /// </summary>
        /// <returns>IHttpActionResult with the list of products.</returns>
        [Route("Products")]
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            // Mock data for version 1
            var products = new List<BLProductV1>
            {
                new BLProductV1 { Id = 1, Name = "Product 1" },
                new BLProductV1 { Id = 2, Name = "Product 2" }
            };

            return Ok(products);
        }
    }
}
