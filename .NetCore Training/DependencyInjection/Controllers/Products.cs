using DependencyInjection.BL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    /// <summary>
    /// Controller for managing product operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productCatalogService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productCatalogService">The product catalog service.</param>
        public ProductsController(IProductService productCatalogService)
        {
            _productCatalogService = productCatalogService;
        }

        /// <summary>
        /// Retrieves the list of products.
        /// </summary>
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productCatalogService.GetProducts();
            return Ok(products);
        }
    }
}
