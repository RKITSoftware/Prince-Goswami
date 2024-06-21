using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalDemo.BL.Interface.Service
{

    public interface IBLVEH01 : IDataHandlerService<DTOVEH01>
    {
        

        ///<summary>
        ///Removes a vehicle from the inventory.
        ///</summary>
        ///<param name="vehicleId">The ID of the vehicle to be removed.</param>
        ///<returns>A boolean indicating whether the removal was successful or not.</returns>
        Response RemoveVehicle(int vehicleId);

        ///<summary>
        ///Retrieves details of a specific vehicle from the inventory.
        ///</summary>
        ///<param name="vehicleId">The ID of the vehicle to retrieve.</param>
        ///<returns>The vehicle object corresponding to the provided ID.</returns>
        Response GetVehicleById(int vehicleId);

        ///<summary>
        ///Retrieves all vehicles from the inventory.
        ///</summary>
        ///<returns>A list of all vehicles in the inventory.</returns>
        Response GetAllVehicles();

        ///<summary>
        ///Searches for vehicles in the inventory based on specified criteria.
        ///</summary>
        ///<param name="searchCriteria">The criteria for searching vehicles.</param>
        ///<returns>A list of vehicles matching the search criteria.</returns>
        //List<VEH01> SearchVehicles(string searchCriteria);

        ///<summary>
        ///Checks if a vehicle exists in the inventory.
        ///</summary>
        ///<param name="vehicleId">The ID of the vehicle to check.</param>
        ///<returns>A boolean indicating whether the vehicle exists or not.</returns>
        Response VehicleExists(int vehicleId);
    }

}
