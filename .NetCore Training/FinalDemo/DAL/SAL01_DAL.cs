using FinalDemo.BL.Interface.Repository;
using FinalDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Configuration;

namespace FinalDemo.DAL
{
    /// <summary>
    /// Repository for interacting with SAL01 data.
    /// </summary>
    public class SAL01_DAL : ISAL01_DAL
    {
        private readonly IDbConnectionFactory _dbFactory;
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="SAL01_DAL"/> class.
        /// </summary>
        /// <param name="dbFactory">The IDbConnectionFactory implementation.</param>
        public SAL01_DAL( IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }

        /// <summary>
        /// Retrieves a single Vehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Vehicle.</param>
        /// <returns>The Vehicle if found, otherwise null.</returns>
        public SAL01 GetByID(int id)
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.SingleById<SAL01>(id);
        }

        /// <summary>
        /// Retrieves all Vehicles.
        /// </summary>
        /// <returns>A collection of all Vehicles.</returns>
        public List<SAL01> GetAll()
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.Select<SAL01>();
        }

        /// <summary>
        /// Adds a new Vehicle to the repository.
        /// </summary>
        /// <param name="sal01">The Vehicle to add.</param>
        public void Add(SAL01 sal01)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Insert(sal01);
        }

        /// <summary>
        /// Updates an existing Vehicle in the repository.
        /// </summary>
        /// <param name="sal01">The Vehicle to update.</param>
        public void Update(SAL01 sal01)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Update(sal01);
        }

        /// <summary>
        /// Deletes a Vehicle from the repository.
        /// </summary>
        /// <param name="sal01">The Vehicle to delete.</param>
        public void Delete(int id)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Delete(id);
        }
    }
}
