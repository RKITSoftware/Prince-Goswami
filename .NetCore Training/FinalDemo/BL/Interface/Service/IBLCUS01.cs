using System.Collections.Generic;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;

namespace FinalDemo.BL.Interface.Service
{
    public interface IBLCUS01 : IDataHandlerService<DTOCUS01>
    {
        ///<summary>
        ///Removes a customer profile.
        ///</summary>
        ///<param name="customerId">The ID of the customer profile to be removed.</param>
        ///<returns>A boolean indicating whether the removal was successful or not.</returns>
        Response RemoveCustomer(int customerId);

        ///<summary>
        ///Retrieves details of a specific customer profile.
        ///</summary>
        ///<param name="customerId">The ID of the customer profile to retrieve.</param>
        ///<returns>The customer profile object corresponding to the provided ID.</returns>
        Response GetCustomerById(int customerId);
        
        
        ///<summary>
        ///Retrieves all customer profiles.
        ///</summary>
        ///<returns>A list of all customer profiles.</returns>
        Response GetAllCustomers();

        ///<summary>
        ///Checks if a customer profile exists.
        ///</summary>
        ///<param name="customerId">The ID of the customer profile to check.</param>
        ///<returns>A boolean indicating whether the customer profile exists or not.</returns>
        Response CustomerExists(int customerId);

      
        /// <summary>
        /// Validates if the CUS01 object exists in the database for deletion.
        /// </summary>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        Response ValidationOnDelete(int customerId);

         }
}
