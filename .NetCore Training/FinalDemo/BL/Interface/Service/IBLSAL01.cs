using System.Collections.Generic;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;

namespace FinalDemo.BL.Interface.Service
{
    public interface IBLSAL01 : IDataHandlerService<DTOSAL01>
    {
        

        ///<summary>
        ///Removes a sale deal.
        ///</summary>
        ///<param name="dealId">The ID of the sale deal to be removed.</param>
        ///<returns>A boolean indicating whether the removal was successful or not.</returns>
        Response RemoveSaleDeal(int dealId);

        ///<summary>
        ///Retrieves details of a specific sale deal.
        ///</summary>
        ///<param name="dealId">The ID of the sale deal to retrieve.</param>
        ///<returns>The sale deal object corresponding to the provided ID.</returns>
        Response GetSaleDealById(int dealId);

        ///<summary>
        ///Retrieves all sale deals.
        ///</summary>
        ///<returns>A list of all sale deals.</returns>
        Response GetAllSaleDeals();

        ///<summary>
        ///Checks if a sale deal exists.
        ///</summary>
        ///<param name="dealId">The ID of the sale deal to check.</param>
        ///<returns>A boolean indicating whether the sale deal exists or not.</returns>
         bool SaleDealExists(int dealId);
    }
}
