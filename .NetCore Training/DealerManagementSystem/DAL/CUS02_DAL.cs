using DealerManagementSystem.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Configuration;

namespace DealerManagementSystem.DAL
{
    /// <summary>
    /// Repository for interacting with CUS02 data.
    /// </summary>
    public class CUS02_DAL : ICUS02_DAL
    {
        private readonly IDbConnectionFactory _dbFactory;
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="CUS02_DAL"/> class.
        /// </summary>
        /// <param name="dbFactory">The IDbConnectionFactory implementation.</param>
        public CUS02_DAL( IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }

        /// <summary>
        /// Retrieves a single Vehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Vehicle.</param>
        /// <returns>The Vehicle if found, otherwise null.</returns>
        public CUS02 GetByID(int id)
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.SingleById<CUS02>(id);
        }

        /// <summary>
        /// Retrieves all Vehicles.
        /// </summary>
        /// <returns>A collection of all Vehicles.</returns>
        public List<CUS02> GetAll()
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.Select<CUS02>();
        }

        /// <summary>
        /// Adds a new Vehicle to the repository.
        /// </summary>
        /// <param name="cus02">The Vehicle to add.</param>
        public void Add(CUS02 cus02)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Insert(cus02);
        }

        /// <summary>
        /// Updates an existing Vehicle in the repository.
        /// </summary>
        /// <param name="cus02">The Vehicle to update.</param>
        public void Update(CUS02 cus02)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Update(cus02);
        }

        /// <summary>
        /// Deletes a Vehicle from the repository.
        /// </summary>
        /// <param name="cus02">The Vehicle to delete.</param>
        public void Delete(int id)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Delete(id);
        }
    }
}
