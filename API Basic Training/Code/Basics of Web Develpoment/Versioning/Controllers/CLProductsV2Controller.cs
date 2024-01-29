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
    [RoutePrefix("api/v2/demo")]
    public class CLProductsV2Controller : ApiController
    {
        /// <summary>
        /// Gets a list of products for version 1 of the API.
        /// </summary>
        /// <returns>IHttpActionResult with the list of products.</returns>
        [Route("Products")]
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            // mock data for V2
            var products = new List<BLProductV2>
            {
                new BLProductV2 { Id = 1, Name = "Product 1", Description = "Product V2" },
                new BLProductV2 { Id = 2, Name = "Product 2", Description = "Product V2" }
            };

            return Ok(products);
        }
    }
}
