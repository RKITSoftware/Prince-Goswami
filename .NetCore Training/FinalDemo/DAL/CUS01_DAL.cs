using FinalDemo.BL.Interface.Repository;
using FinalDemo.Models.POCO;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace FinalDemo.DAL
{
    /// <summary>
    /// Repository for interacting with CUS01 data.
    /// </summary>
    public class CUS01_DAL : ICUS01_DAL
    {
        private readonly DALHelper _dalHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CUS01_DAL"/> class.
        /// </summary>
        /// <param name="dalHelper">The DALHelper instance.</param>
        public CUS01_DAL(DALHelper dalHelper)
        {
            _dalHelper = dalHelper;
        }

        /// <summary>
        /// Retrieves a single Customer by its unique S01F01entifier.
        /// </summary>
        /// <param name="S01F01">The unique S01F01entifier of the Customer.</param>
        /// <returns>The Customer if found, otherwise null.</returns>
        public CUS01 GetByS01F01(int S01F01)
        {
            string query = "SELECT * FROM CUS01 WHERE S01F01 = @S01F01";
            var parameter = new MySqlParameter("@S01F01", S01F01);
            return _dalHelper.ExecuteSingleQuery<CUS01>(query, parameter);
        }

        /// <summary>
        /// Retrieves all Customers.
        /// </summary>
        /// <returns>A collection of all Customers.</returns>
        public List<CUS01> GetAll()
        {
            string query = "SELECT * FROM CUS01";
            return _dalHelper.ExecuteQuery<CUS01>(query);
        }

        /// <summary>
        /// Adds a new Customer to the repository.
        /// </summary>
        /// <param name="cus01">The Customer to add.</param>
        public voS01F01 Add(CUS01 cus01)
        {
            string query = "INSERT INTO CUS01 (S01F02, S01F03, S01F04) VALUES (@S01F02, @S01F03, @S01F04)";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@S01F02", cus01.S01F02),
                new MySqlParameter("@S01F03", cus01.S01F03),
                new MySqlParameter("@S01F04", cus01.S01F04)
            };
            _dalHelper.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Updates an existing Customer in the repository.
        /// </summary>
        /// <param name="cus01">The Customer to update.</param>
        public void Update(CUS01 cus01)
        {
            string query = "UPDATE CUS01 SET S01F02 = @S01F02, S01F03 = @S01F03, S01F04 = @S01F04 WHERE S01F01 = @S01F01";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@S01F02", cus01.S01F02),
                new MySqlParameter("@S01F03", cus01.S01F03),
                new MySqlParameter("@S01F04", cus01.S01F04),
                new MySqlParameter("@S01F01", cus01.S01F01)
            };
            _dalHelper.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Deletes a Customer from the repository.
        /// </summary>
        /// <param name="S01F01">The unique S01F01entifier of the Customer to delete.</param>
        public void Delete(int S01F01)
        {
            string query = "DELETE FROM CUS01 WHERE S01F01 = @S01F01";
            var parameter = new MySqlParameter("@S01F01", S01F01);
            _dalHelper.ExecuteNonQuery(query, parameter);
        }

    }
}
