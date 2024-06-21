using MySql.Data.MySqlClient;
using System.Data;

public class DALHelper
{
    /// <summary>
    /// Executes a SELECT query on the database.
    /// </summary>
    /// <param name="query">The SELECT query to execute.</param>
    /// <param name="connection">The MySqlConnection to use for the query.</param>
    /// <returns>The result of the query as a dynamic object.</returns>
    public static DataTable ExecuteSelectQuery(string query, MySqlConnection connection)
    {
        DataTable dt = new DataTable();
        using (connection)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
        }
        return dt;
    }

    /// <summary>
    /// Executes a non-query (such as INSERT, UPDATE, DELETE) on the database.
    /// </summary>
    /// <param name="query">The non-query to execute.</param>
    /// <param name="connection">The MySqlConnection to use for the non-query.</param>
    /// <returns>The number of rows affected by the non-query.</returns>
    public static int ExecuteNonQuery(string query, MySqlConnection connection)
    {
        int rowsAffected;
        using (connection)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }
        return rowsAffected;
    }
}
