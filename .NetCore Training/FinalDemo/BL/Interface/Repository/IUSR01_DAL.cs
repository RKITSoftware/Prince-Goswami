using FinalDemo.Models.POCO;
using System;
using System.Collections.Generic;

namespace FinalDemo.BL.Interface.Repository
{
    /// <summary>
    /// Interface for interacting with USR01 data repository.
    /// </summary>
    public interface IUSR01_DAL
    {
        /// <summary>
        /// Retrieves a single USR01 entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the USR01 entity.</param>
        /// <returns>The USR01 entity if found, otherwise null.</returns>
        USR01 GetByID(int id);

        /// <summary>
        /// Retrieves all USR01 entities.
        /// </summary>
        /// <returns>A collection of all USR01 entities.</returns>
        List<USR01> GetAll();

        /// <summary>
        /// Adds a new USR01 entity to the repository.
        /// </summary>
        /// <param name="veh01">The USR01 entity to add.</param>
        void Add(USR01 veh01);

        /// <summary>
        /// Updates an existing USR01 entity in the repository.
        /// </summary>
        /// <param name="veh01">The USR01 entity to update.</param>
        void Update(USR01 veh01);

        /// <summary>
        /// Deletes a USR01 entity from the repository.
        /// </summary>
        /// <param name="veh01">The USR01 entity to delete.</param>
        void Delete(int vehicleId);
    }
}
