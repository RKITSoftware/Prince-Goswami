using DependencyInjection.Models;
using System.Collections.Generic;

namespace DependencyInjection.BL.Interface
{
    /// <summary>
    /// Represents a service for processing orders.
    /// </summary>
    public interface IOrderProcessingService
    {
        /// <summary>
        /// Processes the specified cart items to complete an order.
        /// </summary>
        /// <param name="cartItems">The collection of cart items to process.</param>
        void ProcessOrder(IEnumerable<CartItem> cartItems);
    }
}
