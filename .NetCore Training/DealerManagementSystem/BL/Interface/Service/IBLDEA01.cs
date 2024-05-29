using System.Collections.Generic;
using DealerManagementSystem.Models.POCO;

namespace DealerManagementSystem.BL.Interface.Service
{
    public interface IBLDEA01
    {
        ///<summary>
        ///Adds a new dealer.
        ///</summary>
        ///<param name="dealer">The dealer object to be added.</param>
        void AddDealer(DEA01 dealer);

        ///<summary>
        ///Updates an existing dealer.
        ///</summary>
        ///<param name="dealer">The updated dealer object.</param>
        ///<returns>A boolean indicating whether the update was successful or not.</returns>
        void UpdateDealer(DEA01 dealer);

        ///<summary>
        ///Removes a dealer.
        ///</summary>
        ///<param name="dealerId">The ID of the dealer to be removed.</param>
        ///<returns>A boolean indicating whether the removal was successful or not.</returns>
        void RemoveDealer(int dealerId);

        ///<summary>
        ///Retrieves details of a specific dealer.
        ///</summary>
        ///<param name="dealerId">The ID of the dealer to retrieve.</param>
        ///<returns>The dealer object corresponding to the provided ID.</returns>
        DEA01 GetDealerById(int dealerId);

        ///<summary>
        ///Retrieves all dealers.
        ///</summary>
        ///<returns>A list of all dealers.</returns>
        List<DEA01> GetAllDealers();

        ///<summary>
        ///Checks if a dealer exists.
        ///</summary>
        ///<param name="dealerId">The ID of the dealer to check.</param>
        ///<returns>A boolean indicating whether the dealer exists or not.</returns>
        bool DealerExists(int dealerId);
    }
}
