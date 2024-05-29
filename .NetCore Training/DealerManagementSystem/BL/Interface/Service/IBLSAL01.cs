using System.Collections.Generic;
using DealerManagementSystem.Models.POCO;

namespace DealerManagementSystem.BL.Interface.Service
{
    public interface IBLSAL01
    {
        ///<summary>
        ///Adds a new sale deal.
        ///</summary>
        ///<param name="deal">The sale deal object to be added.</param>
        void AddSaleDeal(SAL01 deal);

        ///<summary>
        ///Updates an existing sale deal.
        ///</summary>
        ///<param name="deal">The updated sale deal object.</param>
        ///<returns>A boolean indicating whether the update was successful or not.</returns>
        void UpdateSaleDeal(SAL01 deal);

        ///<summary>
        ///Removes a sale deal.
        ///</summary>
        ///<param name="dealId">The ID of the sale deal to be removed.</param>
        ///<returns>A boolean indicating whether the removal was successful or not.</returns>
        void RemoveSaleDeal(int dealId);

        ///<summary>
        ///Retrieves details of a specific sale deal.
        ///</summary>
        ///<param name="dealId">The ID of the sale deal to retrieve.</param>
        ///<returns>The sale deal object corresponding to the provided ID.</returns>
        SAL01 GetSaleDealById(int dealId);

        ///<summary>
        ///Retrieves all sale deals.
        ///</summary>
        ///<returns>A list of all sale deals.</returns>
        List<SAL01> GetAllSaleDeals();

        ///<summary>
        ///Checks if a sale deal exists.
        ///</summary>
        ///<param name="dealId">The ID of the sale deal to check.</param>
        ///<returns>A boolean indicating whether the sale deal exists or not.</returns>
        bool SaleDealExists(int dealId);
    }
}
