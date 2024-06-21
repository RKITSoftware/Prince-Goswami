using FinalDemo.Models.POCO;
using System;
using System.Collections.Generic;

namespace FinalDemo.BL.Interface.Repository
{
    /// <summary>
    /// Interface for interacting with CUS02 data repository.
    /// </summary>
    public interface ICUS02_DAL
    {
        /// <summary>
        /// Retrieves a single CUS02 entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the CUS02 entity.</param>
        /// <returns>The CUS02 entity if found, otherwise null.</returns>
        CUS02 GetByID(int id);

        /// <summary>
        /// Retrieves all CUS02 entities.
        /// </summary>
        /// <returns>A collection of all CUS02 entities.</returns>
        List<CUS02> GetAll();

        /// <summary>
        /// Adds a new CUS02 entity to the repository.
        /// </summary>
        /// <param name="cus02">The CUS02 entity to add.</param>
        void Add(CUS02 cus02);

        /// <summary>
        /// Updates an existing CUS02 entity in the repository.
        /// </summary>
        /// <param name="cus02">The CUS02 entity to update.</param>
        void Update(CUS02 cus02);

        /// <summary>
        /// Deletes a CUS02 entity from the repository.
        /// </summary>
        /// <param name="cus02">The CUS02 entity to delete.</param>
        void Delete(int id);
    }
}
