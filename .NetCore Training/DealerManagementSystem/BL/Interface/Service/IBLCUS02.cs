using System.Collections.Generic;
using DealerManagementSystem.Models;

namespace DealerManagementSystem.BL.Interface.Service
{
    public interface IBLCUS02
    {
        ///<summary>
        ///Adds a new customer transaction.
        ///</summary>
        ///<param name="transaction">The customer transaction object to be added.</param>
        void AddCustomerTransaction(CUS02 transaction);

        ///<summary>
        ///Updates an existing customer transaction.
        ///</summary>
        ///<param name="transaction">The updated customer transaction object.</param>
        ///<returns>A boolean indicating whether the update was successful or not.</returns>
        void UpdateCustomerTransaction(CUS02 transaction);

        ///<summary>
        ///Removes a customer transaction.
        ///</summary>
        ///<param name="transactionId">The ID of the customer transaction to be removed.</param>
        ///<returns>A boolean indicating whether the removal was successful or not.</returns>
        void RemoveCustomerTransaction(int transactionId);

        ///<summary>
        ///Retrieves details of a specific customer transaction.
        ///</summary>
        ///<param name="transactionId">The ID of the customer transaction to retrieve.</param>
        ///<returns>The customer transaction object corresponding to the provided ID.</returns>
        CUS02 GetCustomerTransactionById(int transactionId);

        ///<summary>
        ///Retrieves all customer transactions.
        ///</summary>
        ///<returns>A list of all customer transactions.</returns>
        List<CUS02> GetAllCustomerTransactions();

        ///<summary>
        ///Checks if a customer transaction exists.
        ///</summary>
        ///<param name="transactionId">The ID of the customer transaction to check.</param>
        ///<returns>A boolean indicating whether the customer transaction exists or not.</returns>
        bool CustomerTransactionExists(int transactionId);
    }
}
