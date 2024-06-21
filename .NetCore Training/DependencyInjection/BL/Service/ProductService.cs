
using DependencyInjection.BL.Interface;
using DependencyInjection.Models;
using System.Collections.Generic;

namespace DependencyInjection.BL.Service
{
    //// regions
    /// <summary>
    /// Service for managing products.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        public ProductService()
        {
            // Initialize some sample products
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10.0m },
                new Product { Id = 2, Name = "Product 2", Price = 20.0m },
                new Product { Id = 3, Name = "Product 3", Price = 30.0m }
            };
        }

        /// <summary>
        /// Retrieves a collection of products.
        /// </summary>
        /// <returns>The collection of products.</returns>
        public IEnumerable<Product> GetProducts() => _products;
    }
}
