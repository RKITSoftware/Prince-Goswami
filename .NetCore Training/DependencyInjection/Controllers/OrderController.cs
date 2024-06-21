using DependencyInjection.BL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    /// <summary>
    /// Controller for managing order operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOrderProcessingService _orderProcessingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="shoppingCartService">The shopping cart service.</param>
        /// <param name="orderProcessingService">The order processing service.</param>
        public OrdersController(IShoppingCartService shoppingCartService, IOrderProcessingService orderProcessingService)
        {
            _shoppingCartService = shoppingCartService;
            _orderProcessingService = orderProcessingService;
        }

        /// <summary>
        /// Processes the order based on the items in the shopping cart.
        /// </summary>
        [HttpPost]
        public IActionResult ProcessOrder()
        {
            var cartItems = _shoppingCartService.GetCartItems();
            _orderProcessingService.ProcessOrder(cartItems);
            return Ok("Order processed successfully");
        }
    }
}
