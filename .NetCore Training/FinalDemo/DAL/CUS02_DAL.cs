using FinalDemo.BL.Interface.Repository;
using FinalDemo.Models.POCO;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace FinalDemo.DAL
{
    /// <summary>
    /// Repository for interacting with CUS02 data.
    /// </summary>
    public class CUS02_DAL : ICUS02_DAL
    {
        private readonly DALHelper _dalHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CUS02_DAL"/> class.
        /// </summary>
        /// <param name="dalHelper">The DALHelper instance.</param>
        public CUS02_DAL(DALHelper dalHelper)
        {
            _dalHelper = dalHelper;
        }

        /// <summary>
        /// Retrieves a single Customer by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Customer.</param>
        /// <returns>The Customer if found, otherwise null.</returns>
        public CUS02 GetByID(int id)
        {
            string query = "SELECT * FROM CUS02 WHERE Id = @Id";
            var parameter = new MySqlParameter("@Id", id);
            return _dalHelper.ExecuteSingleQuery<CUS02>(query, parameter);
        }

        /// <summary>
        /// Retrieves all Customers.
        /// </summary>
        /// <returns>A collection of all Customers.</returns>
        public List<CUS02> GetAll()
        {
            string query = "SELECT * FROM CUS02";
            return _dalHelper.ExecuteQuery<CUS02>(query);
        }

        /// <summary>
        /// Adds a new Customer to the repository.
        /// </summary>
        /// <param name="cus02">The Customer to add.</param>
        public void Add(CUS02 cus02)
        {
            string query = "INSERT INTO CUS02 (FirstName, LastName, Email) VALUES (@FirstName, @LastName, @Email)";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@FirstName", cus02.FirstName),
                new MySqlParameter("@LastName", cus02.LastName),
                new MySqlParameter("@Email", cus02.Email)
            };
            _dalHelper.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Updates an existing Customer in the repository.
        /// </summary>
        /// <param name="cus02">The Customer to update.</param>
        public void Update(CUS02 cus02)
        {
            string query = "UPDATE CUS02 SET FirstName = @FirstName, LastName = @LastName, Email = @Email WHERE Id = @Id";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@FirstName", cus02.FirstName),
                new MySqlParameter("@LastName", cus02.LastName),
                new MySqlParameter("@Email", cus02.Email),
                new MySqlParameter("@Id", cus02.Id)
            };
            _dalHelper.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Deletes a Customer from the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the Customer to delete.</param>
        public void Delete(int id)
        {
            string query = "DELETE FROM CUS02 WHERE Id = @Id";
            var parameter = new MySqlParameter("@Id", id);
            _dalHelper.ExecuteNonQuery(query, parameter);
        }
    }
}
