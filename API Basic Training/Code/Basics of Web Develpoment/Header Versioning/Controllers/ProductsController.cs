using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Versioning.Models;

namespace Header_Versioning.Controllers
{

    /// <summary>
    /// Controller for handling product-related operations with header versioning.
    /// </summary>
    public class ProductsController : ApiController
    {
        /// <summary>
        /// Gets products based on the specified version.
        /// </summary>
        /// <returns>IHttpActionResult with the list of products based on the version.</returns>
        [HttpGet]
        [Route("Products")]
        public IHttpActionResult GetProducts()
        {
            // Retrieve products based on the requested version
            IEnumerable<object> products = null;

            // Check the API version specified in the request header
            var apiVersion = Request.Headers.GetValues("api-version")?.FirstOrDefault();

            switch (apiVersion)
            {
                case "1":
                    products = GetProductsV1();
                    break;

                case "2":
                    products = GetProductsV2();
                    break;

                default:
                    return BadRequest("Invalid API version.");
            }

            return Ok(products);
        }

        #region Private Methods

        /// <summary>
        /// Gets version 1 of the products.
        /// </summary>
        /// <returns>IHttpActionResult with the list of version 1 products.</returns>
        private IEnumerable<BLProductV1> GetProductsV1()
        {
            return new List<BLProductV1>
            {
                new BLProductV1 { Id = 1, Name = "Product 1" },
                new BLProductV1 { Id = 2, Name = "Product 2" }
            };
        }

        /// <summary>
        /// Gets version 2 of the products.
        /// </summary>
        /// <returns>IHttpActionResult with the list of version 2 products.</returns>
        private IEnumerable<BLProductV2> GetProductsV2()
        {
            return new List<BLProductV2>
            {
                new BLProductV2 { Id = 1, Name = "Product 1", Description = "Description 1" },
                new BLProductV2 { Id = 2, Name = "Product 2", Description = "Description 2" }
            };
        }

        #endregion
    }
}
