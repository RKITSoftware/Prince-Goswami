using DealerManagementSystem.Models.POCO;
using System;
using System.Collections.Generic;

namespace DealerManagementSystem.DAL
{
    /// <summary>
    /// Interface for interacting with DEA01 data repository.
    /// </summary>
    public interface IDEA01_DAL
    {
        /// <summary>
        /// Retrieves a single DEA01 entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the DEA01 entity.</param>
        /// <returns>The DEA01 entity if found, otherwise null.</returns>
        DEA01 GetByID(int id);

        /// <summary>
        /// Retrieves all DEA01 entities.
        /// </summary>
        /// <returns>A collection of all DEA01 entities.</returns>
        List<DEA01> GetAll();

        /// <summary>
        /// Adds a new DEA01 entity to the repository.
        /// </summary>
        /// <param name="dea01">The DEA01 entity to add.</param>
        void Add(DEA01 dea01);

        /// <summary>
        /// Updates an existing DEA01 entity in the repository.
        /// </summary>
        /// <param name="dea01">The DEA01 entity to update.</param>
        void Update(DEA01 dea01);

        /// <summary>
        /// Deletes a DEA01 entity from the repository.
        /// </summary>
        /// <param name="dea01">The DEA01 entity to delete.</param>
        void Delete(int id);
    }
}
