using System.Collections.Generic;
using DealerManagementSystem.Models.POCO;

namespace DealerManagementSystem.BL.Interface.Service
{
    public interface IBLCUS01
    {
        ///<summary>
        ///Adds a new customer profile.
        ///</summary>
        ///<param name="customer">The customer profile object to be added.</param>
        void AddCustomer(CUS01 customer);

        ///<summary>
        ///Updates an existing customer profile.
        ///</summary>
        ///<param name="customer">The updated customer profile object.</param>
        ///<returns>A boolean indicating whether the update was successful or not.</returns>
        void UpdateCustomer(CUS01 customer);

        ///<summary>
        ///Removes a customer profile.
        ///</summary>
        ///<param name="customerId">The ID of the customer profile to be removed.</param>
        ///<returns>A boolean indicating whether the removal was successful or not.</returns>
        void RemoveCustomer(int customerId);

        ///<summary>
        ///Retrieves details of a specific customer profile.
        ///</summary>
        ///<param name="customerId">The ID of the customer profile to retrieve.</param>
        ///<returns>The customer profile object corresponding to the provided ID.</returns>
        CUS01 GetCustomerById(int customerId);

        ///<summary>
        ///Retrieves all customer profiles.
        ///</summary>
        ///<returns>A list of all customer profiles.</returns>
        List<CUS01> GetAllCustomers();

        ///<summary>
        ///Checks if a customer profile exists.
        ///</summary>
        ///<param name="customerId">The ID of the customer profile to check.</param>
        ///<returns>A boolean indicating whether the customer profile exists or not.</returns>
        bool CustomerExists(int customerId);
    }
}
