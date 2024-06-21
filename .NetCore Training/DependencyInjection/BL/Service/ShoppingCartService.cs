using DependencyInjection.BL.Interface;
using DependencyInjection.Models;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjection.BL.Service
{
    /// <summary>
    /// Service for managing a shopping cart.
    /// </summary>
    public class ShoppingCartService : IShoppingCartService
    {
        private static List<CartItem> _cartItems = new();

        /// <summary>
        /// Adds a product to the shopping cart.
        /// </summary>
        /// <param name="product">The product to add to the cart.</param>
        public void AddToCart(Product? product)
        {
            var cartItem = _cartItems.FirstOrDefault(item => item.Product.Id == product.Id);
            if (cartItem == null)
            {
                _cartItems.Add(new CartItem { Product = product, Quantity = 1 });
            }
            else
            {
                cartItem.Quantity++;
            }
        }

        /// <summary>
        /// Retrieves the items currently in the shopping cart.
        /// </summary>
        /// <returns>The collection of cart items.</returns>
        public IEnumerable<CartItem> GetCartItems() => _cartItems;
    }
}
