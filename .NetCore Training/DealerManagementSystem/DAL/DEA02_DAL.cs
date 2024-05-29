using DealerManagementSystem.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Configuration;

namespace DealerManagementSystem.DAL
{
    /// <summary>
    /// Repository for interacting with DEA02 data.
    /// </summary>
    public class DEA02_DAL : IDEA02_DAL
    {
        private readonly IDbConnectionFactory _dbFactory;
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DEA02_DAL"/> class.
        /// </summary>
        /// <param name="dbFactory">The IDbConnectionFactory implementation.</param>
        public DEA02_DAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }

        /// <summary>
        /// Retrieves a single Vehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Vehicle.</param>
        /// <returns>The Vehicle if found, otherwise null.</returns>
        public DEA02 GetByID(int id)
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.SingleById<DEA02>(id);
        }

        /// <summary>
        /// Retrieves all Vehicles.
        /// </summary>
        /// <returns>A collection of all Vehicles.</returns>
        public List<DEA02> GetAll()
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.Select<DEA02>();
        }

        /// <summary>
        /// Adds a new Vehicle to the repository.
        /// </summary>
        /// <param name="dea02">The Vehicle to add.</param>
        public void Add(DEA02 dea02)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Insert(dea02);
        }

        /// <summary>
        /// Updates an existing Vehicle in the repository.
        /// </summary>
        /// <param name="dea02">The Vehicle to update.</param>
        public void Update(DEA02 dea02)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Update(dea02);
        }

        /// <summary>
        /// Deletes a Vehicle from the repository.
        /// </summary>
        /// <param name="dea02">The Vehicle to delete.</param>
        public void Delete(int id)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Delete(id);
        }
    }
}
