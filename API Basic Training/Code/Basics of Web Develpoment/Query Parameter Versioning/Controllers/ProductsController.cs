using System.Collections.Generic;
using System.Web.Http;
using Versioning.Models;

namespace Versioning.Controllers
{
    /// <summary>
    /// Controller for handling product-related operations with query parameter versioning.
    /// </summary>
    [RoutePrefix("api/demo/products")]
    public class ProductsController : ApiController
    {
        /// <summary>
        /// Gets products based on the specified version.
        /// </summary>
        /// <param name="version">The version number specified in the query string.</param>
        /// <returns>IHttpActionResult with the list of products based on the version.</returns>
        [HttpGet]
        [Route("GetProducts")]
        public IHttpActionResult GetProducts([FromUri] int version)
        {
            // Use the version parameter to determine the behavior
            switch (version)
            {
                case 1:
                    return GetProductsV1();
                case 2:
                    return GetProductsV2();
                default:
                    return BadRequest("Invalid version number");
            }
        }

        #region Private Methods

        /// <summary>
        /// Gets version 1 of the products.
        /// </summary>
        /// <returns>IHttpActionResult with the list of version 1 products.</returns>
        private IHttpActionResult GetProductsV1()
        {
            var productsV1 = new List<BLProductV1>
            {
                new BLProductV1 { Id = 1, Name = "Product 1" },
                new BLProductV1 { Id = 2, Name = "Product 2" }
            };

            return Ok(productsV1);
        }

        /// <summary>
        /// Gets version 2 of the products.
        /// </summary>
        /// <returns>IHttpActionResult with the list of version 2 products.</returns>
        private IHttpActionResult GetProductsV2()
        {
            var productsV2 = new List<BLProductV2>
            {
                new BLProductV2 { Id = 1, Name = "Product 1", Description = "Description 1" },
                new BLProductV2 { Id = 2, Name = "Product 2", Description = "Description 2" }
            };

            return Ok(productsV2);
        }

        #endregion
    }
}
