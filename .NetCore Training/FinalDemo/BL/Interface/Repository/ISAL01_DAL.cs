using FinalDemo.Models.POCO;
using System;
using System.Collections.Generic;

namespace FinalDemo.BL.Interface.Repository
{
    /// <summary>
    /// Interface for interacting with SAL01 data repository.
    /// </summary>
    public interface ISAL01_DAL
    {
        /// <summary>
        /// Retrieves a single SAL01 entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the SAL01 entity.</param>
        /// <returns>The SAL01 entity if found, otherwise null.</returns>
        SAL01 GetByID(int id);

        /// <summary>
        /// Retrieves all SAL01 entities.
        /// </summary>
        /// <returns>A collection of all SAL01 entities.</returns>
        List<SAL01> GetAll();

        /// <summary>
        /// Adds a new SAL01 entity to the repository.
        /// </summary>
        /// <param name="sal01">The SAL01 entity to add.</param>
        void Add(SAL01 sal01);

        /// <summary>
        /// Updates an existing SAL01 entity in the repository.
        /// </summary>
        /// <param name="sal01">The SAL01 entity to update.</param>
        void Update(SAL01 sal01);

        /// <summary>
        /// Deletes a SAL01 entity from the repository.
        /// </summary>
        /// <param name="sal01">The SAL01 entity to delete.</param>
        void Delete(int id);
    }
}
