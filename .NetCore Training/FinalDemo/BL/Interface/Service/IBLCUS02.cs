using System.Collections.Generic;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;

namespace FinalDemo.BL.Interface.Service
{
    public interface IBLCUS02 : IDataHandlerService<DTOCUS02>
    {
      
        ///<summary>
        ///Removes a customer transaction.
        ///</summary>
        ///<param name="transactionId">The ID of the customer transaction to be removed.</param>
        ///<returns>A boolean indicating whether the removal was successful or not.</returns>
        Response RemoveCustomerTransaction(int transactionId);

        ///<summary>
        ///Retrieves details of a specific customer transaction.
        ///</summary>
        ///<param name="transactionId">The ID of the customer transaction to retrieve.</param>
        ///<returns>The customer transaction object corresponding to the provided ID.</returns>
        Response GetCustomerTransactionById(int transactionId);

        ///<summary>
        ///Retrieves all customer transactions.
        ///</summary>
        ///<returns>A list of all customer transactions.</returns>
        Response GetAllCustomerTransactions();

        ///<summary>
        ///Checks if a customer transaction exists.
        ///</summary>
        ///<param name="transactionId">The ID of the customer transaction to check.</param>
        ///<returns>A boolean indicating whether the customer transaction exists or not.</returns>
        bool CustomerTransactionExists(int transactionId);
    }
}
