using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Swagger_Demo.Models;

namespace Swagger_Demo.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on products.
    /// </summary>
    public class ProductsController : ApiController
    {
        // In-memory list to store products
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Product A" },
            new Product { Id = 2, Name = "Product B" }
        };

        #region Actions

        /// <summary>
        /// Gets a list of products.
        /// </summary>
        /// <returns>An IEnumerable of Product representing products.</returns>
        // GET api/values
        public IHttpActionResult Get()
        {
            return Ok(_products);
        }

        /// <summary>
        /// Gets a specific product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>A Product representing the product with the specified ID.</returns>
        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to be created.</param>
        // POST api/values
        public IHttpActionResult Post([FromBody] Product product)
        {
            product.Id = _products.Count + 1; // Assuming simple incremental ID assignment
            _products.Add(product);

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        /// <summary>
        /// Updates a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product.</param>
        // PUT api/values/5
        public IHttpActionResult Put(int id, [FromBody] Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;

            return Ok(existingProduct);
        }

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _products.Remove(product);

            return Ok(product);
        }

        #endregion
    }
}
