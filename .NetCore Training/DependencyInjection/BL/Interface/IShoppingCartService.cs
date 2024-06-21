using DependencyInjection.Models;
using System.Collections.Generic;

namespace DependencyInjection.BL.Interface
{
    /// <summary>
    /// Represents a service for managing a shopping cart.
    /// </summary>
    public interface IShoppingCartService
    {
        /// <summary>
        /// Adds a product to the shopping cart.
        /// </summary>
        /// <param name="product">The product to add to the cart.</param>
        void AddToCart(Product? product);

        /// <summary>
        /// Retrieves the items currently in the shopping cart.
        /// </summary>
        /// <returns>The collection of cart items.</returns>
        IEnumerable<CartItem> GetCartItems();
    }
}
