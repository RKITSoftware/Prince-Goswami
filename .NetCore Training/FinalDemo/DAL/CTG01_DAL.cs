using FinalDemo.BL.Interface.Repository;
using FinalDemo.Models.POCO;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace FinalDemo.DAL
{
    public class CTG01_DAL : ICTG01_DAL
    {
        private readonly DALHelper _dalHelper;

        public CTG01_DAL(DALHelper dalHelper)
        {
            _dalHelper = dalHelper;
        }

        public CTG01 GetByID(int id)
        {
            string query = "SELECT * FROM CTG01 WHERE Id = @Id";
            var parameter = new MySqlParameter("@Id", id);
            return _dalHelper.ExecuteSingleQuery<CTG01>(query, parameter);
        }

        public List<CTG01> GetAll()
        {
            string query = "SELECT * FROM CTG01";
            return _dalHelper.ExecuteQuery<CTG01>(query);
        }

        public void Add(CTG01 ctg01)
        {
            string query = "INSERT INTO CTG01 (/* columns */) VALUES (/* values */)";
            var parameters = new MySqlParameter[]
            {
                // Add parameters here
            };
            _dalHelper.ExecuteNonQuery(query, parameters);
        }

        public void Update(CTG01 ctg01)
        {
            string query = "UPDATE CTG01 SET /* columns = values */ WHERE Id = @Id";
            var parameters = new MySqlParameter[]
            {
                // Add parameters here
            };
            _dalHelper.ExecuteNonQuery(query, parameters);
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM CTG01 WHERE Id = @Id";
            var parameter = new MySqlParameter("@Id", id);
            _dalHelper.ExecuteNonQuery(query, parameter);
        }
    }
}
