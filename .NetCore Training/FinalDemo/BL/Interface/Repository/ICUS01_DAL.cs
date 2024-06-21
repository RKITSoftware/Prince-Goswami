using FinalDemo.Models;
using FinalDemo.Models.POCO;
using System;
using System.Collections.Generic;

namespace FinalDemo.BL.Interface.Repository
{
    /// <summary>
    /// Interface for interacting with CUS01 data repository.
    /// </summary>
    public interface ICUS01_DAL
    {

        /// <summary>
        /// Retrieves all CUS01 entities.
        /// </summary>
        /// <returns>A collection of all CUS01 entities.</returns>
        Response GetAll();

        /// <summary>
        /// Validates if the CUS01 object exists in the database for deletion.
        /// </summary>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        Response ValidationOnDelete(int customerId);

        /// <summary>
        /// Removes a customer from the database by their ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer to remove.</param>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        Response RemoveCustomer(int customerId);

        /// <summary>
        /// Retrieves a customer from the database by their ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer to retrieve.</param>
        /// <returns>A Response object indicating success or failure with the retrieved customer data.</returns>
        Response GetCustomerById(int customerId);


    }
}
