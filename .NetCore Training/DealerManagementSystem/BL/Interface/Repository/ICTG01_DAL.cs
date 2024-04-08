using DealerManagementSystem.Models;
using System;
using System.Collections.Generic;

namespace DealerManagementSystem.DAL
{
    /// <summary>
    /// Interface for interacting with CTG01 data repository.
    /// </summary>
    public interface ICTG01_DAL
    {
        /// <summary>
        /// Retrieves a single CTG01 entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the CTG01 entity.</param>
        /// <returns>The CTG01 entity if found, otherwise null.</returns>
        CTG01 GetByID(int id);

        /// <summary>
        /// Retrieves all CTG01 entities.
        /// </summary>
        /// <returns>A collection of all CTG01 entities.</returns>
        List<CTG01> GetAll();

        /// <summary>
        /// Adds a new CTG01 entity to the repository.
        /// </summary>
        /// <param name="ctg01">The CTG01 entity to add.</param>
        void Add(CTG01 ctg01);

        /// <summary>
        /// Updates an existing CTG01 entity in the repository.
        /// </summary>
        /// <param name="ctg01">The CTG01 entity to update.</param>
        void Update(CTG01 ctg01);

        /// <summary>
        /// Deletes a CTG01 entity from the repository.
        /// </summary>
        /// <param name="ctg01">The CTG01 entity to delete.</param>
        void Delete(int categoryId);
    }
}
