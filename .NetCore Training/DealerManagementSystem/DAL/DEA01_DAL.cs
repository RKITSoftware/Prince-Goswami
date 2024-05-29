using DealerManagementSystem.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Configuration;

namespace DealerManagementSystem.DAL
{
    /// <summary>
    /// Repository for interacting with DEA01 data.
    /// </summary>
    public class DEA01_DAL : IDEA01_DAL
    {
        private readonly IDbConnectionFactory _dbFactory;
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DEA01_DAL"/> class.
        /// </summary>
        /// <param name="dbFactory">The IDbConnectionFactory implementation.</param>
        public DEA01_DAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }

        /// <summary>
        /// Retrieves a single Vehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Vehicle.</param>
        /// <returns>The Vehicle if found, otherwise null.</returns>
        public DEA01 GetByID(int id)
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.SingleById<DEA01>(id);
        }

        /// <summary>
        /// Retrieves all Vehicles.
        /// </summary>
        /// <returns>A collection of all Vehicles.</returns>
        public List<DEA01> GetAll()
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.Select<DEA01>();
        }

        /// <summary>
        /// Adds a new Vehicle to the repository.
        /// </summary>
        /// <param name="dea01">The Vehicle to add.</param>
        public void Add(DEA01 dea01)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Insert(dea01);
        }

        /// <summary>
        /// Updates an existing Vehicle in the repository.
        /// </summary>
        /// <param name="dea01">The Vehicle to update.</param>
        public void Update(DEA01 dea01)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Update(dea01);
        }

        /// <summary>
        /// Deletes a Vehicle from the repository.
        /// </summary>
        /// <param name="dea01">The Vehicle to delete.</param>
        public void Delete(int id)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Delete(id);
        }
    }
}
