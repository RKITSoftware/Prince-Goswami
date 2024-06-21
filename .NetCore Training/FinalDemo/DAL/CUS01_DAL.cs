using FinalDemo.BL.Interface.Repository;
using FinalDemo.Models;
using FinalDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Configuration;

namespace FinalDemo.DAL
{
    /// <summary>
    /// Repository for interacting with CUS01 data.
    /// </summary>

    public class CUS01_DAL : ICUS01_DAL
    {
        private readonly IDbConnectionFactory _dbFactory;
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="CUS01_DAL"/> class.
        /// </summary>
        /// <param name="dbFactory">The IDbConnectionFactory implementation.</param>
        public CUS01_DAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }

        /// <summary>
        /// Retrieves a single Vehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Vehicle.</param>
        /// <returns>The Vehicle if found, otherwise null.</returns>
        public CUS01 GetByID(int id)
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.SingleById<CUS01>(id);
        }

        /// <summary>
        /// Retrieves all Vehicles.
        /// </summary>
        /// <returns>A collection of all Vehicles.</returns>
        public List<CUS01> GetAll()
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.Select<CUS01>();
        }

        /// <summary>
        /// Adds a new Vehicle to the repository.
        /// </summary>
        /// <param name="cus01">The Vehicle to add.</param>
        public void Add(CUS01 cus01)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Insert(cus01);
        }

        /// <summary>
        /// Updates an existing Vehicle in the repository.
        /// </summary>
        /// <param name="cus01">The Vehicle to update.</param>
        public void Update(CUS01 cus01)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Update(cus01);
        }

        /// <summary>
        /// Deletes a Vehicle from the repository.
        /// </summary>
        /// <param name="cus01">The Vehicle to delete.</param>
        public void Re(int id)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Delete(id);
        }

        Response ICUS01_DAL.GetAll()
        {
            throw new NotImplementedException();
        }

        public Response ValidationOnDelete(int customerId)
        {
            throw new NotImplementedException();
        }

        public Response RemoveCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public Response GetCustomerById(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
