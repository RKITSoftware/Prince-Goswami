using MySql.Data.MySqlClient;
using System.Data;

namespace FinalDemo.DAL
{
    public class DALHelper
    {
        public readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="CTG01_DAL"/> class.
        /// </summary>
        /// <param name="configuration">The IConfiguration implementation.</param>
        public DALHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        /// <summary>
        /// Maps a data reader to an object.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="reader">The data reader.</param>
        /// <returns>The mapped object.</returns>
        public T MapReaderToObject<T>(IDataReader reader) where T : class, new()
        {
            var obj = new T();
            foreach (var prop in typeof(T).GetProperties())
            {
                if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                {
                    prop.SetValue(obj, reader.GetValue(reader.GetOrdinal(prop.Name)));
                }
            }
            return obj;
        }


        /// <summary>
        /// Executes a query and returns a single result.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="query">The SQL query string.</param>
        /// <param name="parameters">The parameters for the query.</param>
        /// <returns>The result of the query.</returns>
        public T ExecuteSingleQuery<T>(string query, params MySqlParameter[] parameters) where T : class, new()
        {
            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddRange(parameters);
            connection.Open();
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapReaderToObject<T>(reader);
            }
            return null;
        }

        /// <summary>
        /// Executes a query and returns a list of results.
        /// </summary>
        /// <typeparam name="T">The type of the results.</typeparam>
        /// <param name="query">The SQL query string.</param>
        /// <param name="parameters">The parameters for the query.</param>
        /// <returns>The list of results.</returns>
        public List<T> ExecuteQuery<T>(string query, params MySqlParameter[] parameters) where T : class, new()
        {
            var results = new List<T>();
            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddRange(parameters);
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                results.Add(MapReaderToObject<T>(reader));
            }
            return results;
        }


        /// <summary>
        /// Executes a non-query command.
        /// </summary>
        /// <param name="query">The SQL query string.</param>
        /// <param name="parameters">The parameters for the query.</param>
        public void ExecuteNonQuery(string query, params MySqlParameter[] parameters)
        {
            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddRange(parameters);
            connection.Open();
            command.ExecuteNonQuery();
        }

    }
}
