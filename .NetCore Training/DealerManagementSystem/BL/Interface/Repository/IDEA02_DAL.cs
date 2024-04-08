using DealerManagementSystem.Models;
using System;
using System.Collections.Generic;

namespace DealerManagementSystem.DAL
{
    /// <summary>
    /// Interface for interacting with DEA02 data repository.
    /// </summary>
    public interface IDEA02_DAL
    {
        /// <summary>
        /// Retrieves a single DEA02 entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the DEA02 entity.</param>
        /// <returns>The DEA02 entity if found, otherwise null.</returns>
        DEA02 GetByID(int id);

        /// <summary>
        /// Retrieves all DEA02 entities.
        /// </summary>
        /// <returns>A collection of all DEA02 entities.</returns>
        List<DEA02> GetAll();

        /// <summary>
        /// Adds a new DEA02 entity to the repository.
        /// </summary>
        /// <param name="dea02">The DEA02 entity to add.</param>
        void Add(DEA02 dea02);

        /// <summary>
        /// Updates an existing DEA02 entity in the repository.
        /// </summary>
        /// <param name="dea02">The DEA02 entity to update.</param>
        void Update(DEA02 dea02);

        /// <summary>
        /// Deletes a DEA02 entity from the repository.
        /// </summary>
        /// <param name="dea02">The DEA02 entity to delete.</param>
        void Delete(int id);
    }
}
