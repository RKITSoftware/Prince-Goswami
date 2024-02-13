using ORM_Tools.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ORM_Tools.BL
{
    /// <summary>
    /// Business logic class for managing BNK01 (Bank) data.
    /// </summary>
    public static class BLBank
    {
        // Retrieve IDbConnectionFactory from the application context
        private static readonly IDbConnectionFactory _dbFactory;

        static BLBank()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new Exception("IDbConnectionFactory not found");
            }
        }

        /// <summary>
        /// Retrieves all bank from the database.
        /// </summary>
        /// <returns>List of bank</returns>
        public static List<BNK01> GetAll()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var banks = db.Select<BNK01>();

                return banks;
            }
        }

        /// <summary>
        /// Retrieves an bank by ID from the database.
        /// </summary>
        /// <param name="id">Bank ID</param>
        /// <returns>Bank with the specified ID</returns>
        public static BNK01 Get(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var bank = db.SingleById<BNK01>(id);
                return bank;
            }
        }

        /// <summary>
        /// Adds a new bank to the database.
        /// </summary>
        /// <param name="bank">Bank data to be added</param>
        public static void Add(BNK01 bank)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.Insert(bank);
            }
        }

        /// <summary>
        /// Deletes an bank by ID from the database.
        /// </summary>
        /// <param name="id">Bank ID</param>
        public static void Delete(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<BNK01>(id);
            }
        }

        /// <summary>
        /// Updates an existing bank in the database.
        /// </summary>
        /// <param name="bank">Updated bank data</param>
        public static void Update(BNK01 bank)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {

                if (bank != null)
                {
                    db.Update(bank);
                }
            }
        }
    }
}