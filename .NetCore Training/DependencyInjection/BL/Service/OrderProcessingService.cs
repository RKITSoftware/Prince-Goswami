using DependencyInjection.BL.Interface;
using DependencyInjection.Models;
using System;
using System.Collections.Generic;

namespace DependencyInjection.BL.Service
{
    /// <summary>
    /// Service for processing orders.
    /// </summary>
    public class OrderProcessingService : IOrderProcessingService
    {
        /// <summary>
        /// Processes the specified cart items to complete an order.
        /// </summary>
        /// <param name="cartItems">The collection of cart items to process.</param>
        public void ProcessOrder(IEnumerable<CartItem> cartItems)
        {
            // Process order logic
            Console.WriteLine("Order processed:");
            foreach (var item in cartItems)
            {
                Console.WriteLine($"{item.Product.Name} - Quantity: {item.Quantity}");
            }
        }
    }
}
