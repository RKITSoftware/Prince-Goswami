using DealerManagementSystem.Models.POCO;
using System;
using System.Collections.Generic;

namespace DealerManagementSystem.DAL
{
    /// <summary>
    /// Interface for interacting with CUS01 data repository.
    /// </summary>
    public interface ICUS01_DAL
    {
        /// <summary>
        /// Retrieves a single CUS01 entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the CUS01 entity.</param>
        /// <returns>The CUS01 entity if found, otherwise null.</returns>
        CUS01 GetByID(int id);

        /// <summary>
        /// Retrieves all CUS01 entities.
        /// </summary>
        /// <returns>A collection of all CUS01 entities.</returns>
        List<CUS01> GetAll();

        /// <summary>
        /// Adds a new CUS01 entity to the repository.
        /// </summary>
        /// <param name="cus01">The CUS01 entity to add.</param>
        void Add(CUS01 cus01);

        /// <summary>
        /// Updates an existing CUS01 entity in the repository.
        /// </summary>
        /// <param name="cus01">The CUS01 entity to update.</param>
        void Update(CUS01 cus01);

        /// <summary>
        /// Deletes a CUS01 entity from the repository.
        /// </summary>
        /// <param name="cus01">The CUS01 entity to delete.</param>
        void Delete(int id);
    }
}
