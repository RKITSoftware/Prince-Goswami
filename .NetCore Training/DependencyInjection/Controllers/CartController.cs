using DependencyInjection.BL.Interface;
using DependencyInjection.Models;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    /// <summary>
    /// Controller for managing shopping cart operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartController"/> class.
        /// </summary>
        /// <param name="shoppingCartService">The shopping cart service.</param>
        public ShoppingCartController(IShoppingCartService shoppingCartService, IProductService productService)
        {
            _shoppingCartService = shoppingCartService;
            _productService = productService;
        }

        /// <summary>
        /// Adds a product to the shopping cart.
        /// </summary>
        /// <param name="productId">The ID of the product to add to the cart.</param>
        [HttpPost("{productId}")]
        public IActionResult AddToCart(int productId)
        {
            Product product = _productService.GetProducts().FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                _shoppingCartService.AddToCart(product);
                return Ok();
            }
            return NotFound();
        }

        /// <summary>
        /// Retrieves the items currently in the shopping cart.
        /// </summary>
        [HttpGet]
        public IActionResult GetCartItems()
        {
            IEnumerable<Models.CartItem> cartItems = _shoppingCartService.GetCartItems();
            return Ok(cartItems);
        }
    }
}
