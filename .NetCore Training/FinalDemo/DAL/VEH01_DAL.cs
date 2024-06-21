using FinalDemo.BL.Interface.Repository;
using FinalDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Configuration;

namespace FinalDemo.DAL
{
    /// <summary>
    /// Repository for interacting with VEH01 data.
    /// </summary>
    public class VEH01_DAL : IVEH01_DAL
    {
        private readonly IDbConnectionFactory _dbFactory;
        private readonly string _connectionString;


        /// <summary>
        /// Initializes a new instance of the <see cref="VEH01_DAL"/> class.
        /// </summary>
        /// <param name="dbFactory">The IDbConnectionFactory implementation.</param>
        public VEH01_DAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }

        /// <summary>
        /// Retrieves a single Vehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Vehicle.</param>
        /// <returns>The Vehicle if found, otherwise null.</returns>
        public VEH01 GetByID(int id)
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.SingleById<VEH01>(id);
        }

        /// <summary>
        /// Retrieves all Vehicles.
        /// </summary>
        /// <returns>A collection of all Vehicles.</returns>
        public List<VEH01> GetAll()
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.Select<VEH01>();
        }

        /// <summary>
        /// Adds a new Vehicle to the repository.
        /// </summary>
        /// <param name="veh01">The Vehicle to add.</param>
        public void Add(VEH01 veh01)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Insert(veh01);
        }

        /// <summary>
        /// Updates an existing Vehicle in the repository.
        /// </summary>
        /// <param name="veh01">The Vehicle to update.</param>
        public void Update(VEH01 veh01)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Update(veh01);
        }

        /// <summary>
        /// Deletes a Vehicle from the repository.
        /// </summary>
        /// <param name="veh01">The Vehicle to delete.</param>
        public void Delete(int vehicleId)
        {
            using var db = _dbFactory.OpenDbConnection();
            string query = "DELETE FROM VEH01 WHERE H01F01 = " + vehicleId;
            db.ExecuteNonQuery(query);
        }
    }
}
