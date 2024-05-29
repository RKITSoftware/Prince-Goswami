using DealerManagementSystem.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace DealerManagementSystem.DAL
{
    /// <summary>
    /// Repository for interacting with CTG01 data.
    /// </summary>
    public class CTG01_DAL : ICTG01_DAL
    {
        private readonly IDbConnectionFactory _dbFactory;
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="CTG01_DAL"/> class.
        /// </summary>
        /// <param name="dbFactory">The IDbConnectionFactory implementation.</param>
        public CTG01_DAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }

        /// <summary>
        /// Retrieves a single Vehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Vehicle.</param>
        /// <returns>The Vehicle if found, otherwise null.</returns>
        public CTG01 GetByID(int id)
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.SingleById<CTG01>(id);
        }

        /// <summary>
        /// Retrieves all Vehicles.
        /// </summary>
        /// <returns>A collection of all Vehicles.</returns>
        public List<CTG01> GetAll()
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.Select<CTG01>();
        }

        /// <summary>
        /// Adds a new Vehicle to the repository.
        /// </summary>
        /// <param name="ctg01">The Vehicle to add.</param>
        public void Add(CTG01 ctg01)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Insert(ctg01);
        }

        /// <summary>
        /// Updates an existing Vehicle in the repository.
        /// </summary>
        /// <param name="ctg01">The Vehicle to update.</param>
        public void Update(CTG01 ctg01)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Update(ctg01);
        }

        /// <summary>
        /// Deletes a Vehicle from the repository.
        /// </summary>
        /// <param name="ctg01">The Vehicle to delete.</param>
        public void Delete(int categoryId)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Delete(categoryId);
        }
    }
}
