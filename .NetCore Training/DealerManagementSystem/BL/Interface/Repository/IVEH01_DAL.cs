using DealerManagementSystem.Models.POCO;
using System;
using System.Collections.Generic;

namespace DealerManagementSystem.DAL
{
    /// <summary>
    /// Interface for interacting with VEH01 data repository.
    /// </summary>
    public interface IVEH01_DAL
    {
        /// <summary>
        /// Retrieves a single VEH01 entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the VEH01 entity.</param>
        /// <returns>The VEH01 entity if found, otherwise null.</returns>
        VEH01 GetByID(int id);

        /// <summary>
        /// Retrieves all VEH01 entities.
        /// </summary>
        /// <returns>A collection of all VEH01 entities.</returns>
        List<VEH01> GetAll();

        /// <summary>
        /// Adds a new VEH01 entity to the repository.
        /// </summary>
        /// <param name="veh01">The VEH01 entity to add.</param>
        void Add(VEH01 veh01);

        /// <summary>
        /// Updates an existing VEH01 entity in the repository.
        /// </summary>
        /// <param name="veh01">The VEH01 entity to update.</param>
        void Update(VEH01 veh01);

        /// <summary>
        /// Deletes a VEH01 entity from the repository.
        /// </summary>
        /// <param name="veh01">The VEH01 entity to delete.</param>
        void Delete(int vehicleId);
    }
}
