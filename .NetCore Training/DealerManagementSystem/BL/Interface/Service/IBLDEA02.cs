using System.Collections.Generic;
using DealerManagementSystem.Models.POCO;

namespace DealerManagementSystem.BL.Interface.Service
{
    public interface IBLDEA02
    {
        ///<summary>
        ///Adds a new dealer transaction.
        ///</summary>
        ///<param name="transaction">The dealer transaction object to be added.</param>
        void AddDealerTransaction(DEA02 transaction);

        ///<summary>
        ///Updates an existing dealer transaction.
        ///</summary>
        ///<param name="transaction">The updated dealer transaction object.</param>
        ///<returns>A boolean indicating whether the update was successful or not.</returns>
        void UpdateDealerTransaction(DEA02 transaction);

        ///<summary>
        ///Removes a dealer transaction.
        ///</summary>
        ///<param name="transactionId">The ID of the dealer transaction to be removed.</param>
        ///<returns>A boolean indicating whether the removal was successful or not.</returns>
        void RemoveDealerTransaction(int transactionId);

        ///<summary>
        ///Retrieves details of a specific dealer transaction.
        ///</summary>
        ///<param name="transactionId">The ID of the dealer transaction to retrieve.</param>
        ///<returns>The dealer transaction object corresponding to the provided ID.</returns>
        DEA02 GetDealerTransactionById(int transactionId);

        ///<summary>
        ///Retrieves all dealer transactions.
        ///</summary>
        ///<returns>A list of all dealer transactions.</returns>
        List<DEA02> GetAllDealerTransactions();

        ///<summary>
        ///Checks if a dealer transaction exists.
        ///</summary>
        ///<param name="transactionId">The ID of the dealer transaction to check.</param>
        ///<returns>A boolean indicating whether the dealer transaction exists or not.</returns>
        bool DealerTransactionExists(int transactionId);
    }
}
