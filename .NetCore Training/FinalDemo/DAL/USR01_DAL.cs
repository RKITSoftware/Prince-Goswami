using FinalDemo.BL.Interface.Repository;
using FinalDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Configuration;

namespace FinalDemo.DAL
{
    /// <summary>
    /// Repository for interacting with USR01 data.
    /// </summary>
    public class USR01_DAL : IUSR01_DAL
    {
        private readonly IDbConnectionFactory _dbFactory;
        private readonly string _connectionString;


        /// <summary>
        /// Initializes a new instance of the <see cref="USR01_DAL"/> class.
        /// </summary>
        /// <param name="dbFactory">The IDbConnectionFactory implementation.</param>
        public USR01_DAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }

        /// <summary>
        /// Retrieves a single User by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the User.</param>
        /// <returns>The User if found, otherwise null.</returns>
        public USR01 GetByID(int id)
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.SingleById<USR01>(id);
        }

        /// <summary>
        /// Retrieves all Users.
        /// </summary>
        /// <returns>A collection of all Users.</returns>
        public List<USR01> GetAll()
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.Select<USR01>();
        }

        /// <summary>
        /// Adds a new User to the repository.
        /// </summary>
        /// <param name="usr01">The User to add.</param>
        public void Add(USR01 usr01)
        {
            using var db = _dbFactory.OpenDbConnection();

            db.Insert(usr01);
        }

        /// <summary>
        /// Updates an existing User in the repository.
        /// </summary>
        /// <param name="usr01">The User to update.</param>
        public void Update(USR01 usr01)
        {
            using var db = _dbFactory.OpenDbConnection();
            db.Update(usr01);
        }

        /// <summary>
        /// Deletes a User from the repository.
        /// </summary>
        /// <param name="usr01">The User to delete.</param>
        public void Delete(int userId)
        {
            using var db = _dbFactory.OpenDbConnection();
            string query = "DELETE FROM USR01 WHERE H01F01 = " + userId;
            db.ExecuteNonQuery(query);
        }
    }
}
