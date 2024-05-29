using DealerManagementSystem.Models.POCO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DealerManagementSystem.BL.Interface.Service
{

    public interface IBLVEH01
    {
        ///<summary>
        ///Adds a new vehicle to the inventory.
        ///</summary>
        ///<param name="vehicle">The vehicle object to be added.</param>
        void AddVehicle(VEH01 vehicle);

        ///<summary>
        ///Updates an existing vehicle in the inventory.
        ///</summary>
        ///<param name="vehicle">The updated vehicle object.</param>
        ///<returns>A boolean indicating whether the update was successful or not.</returns>
        void UpdateVehicle(VEH01 vehicle);

        ///<summary>
        ///Removes a vehicle from the inventory.
        ///</summary>
        ///<param name="vehicleId">The ID of the vehicle to be removed.</param>
        ///<returns>A boolean indicating whether the removal was successful or not.</returns>
        void RemoveVehicle(int vehicleId);

        ///<summary>
        ///Retrieves details of a specific vehicle from the inventory.
        ///</summary>
        ///<param name="vehicleId">The ID of the vehicle to retrieve.</param>
        ///<returns>The vehicle object corresponding to the provided ID.</returns>
        VEH01 GetVehicleById(int vehicleId);

        ///<summary>
        ///Retrieves all vehicles from the inventory.
        ///</summary>
        ///<returns>A list of all vehicles in the inventory.</returns>
        List<VEH01> GetAllVehicles();

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
        bool VehicleExists(int vehicleId);
    }

}
