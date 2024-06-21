using ATM_Simulation_Demo.Models.POCO;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using System;
using System.Configuration;
using System.Linq;

namespace ATM_Simulation_Demo.DAL
{
    /// <summary>
    /// Repository for handling date-related operations in the ATM simulation.
    /// </summary>
    public class DateRepository
    {
        /// <summary>
        /// Connection string for the database.
        /// </summary>
        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        /// <summary>
        /// Retrieves the current date from the database.
        /// </summary>
        /// <returns>The current date.</returns>
        public static DateTime GetDate()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                CDT01 objCDT01 = connection.Select<CDT01>().FirstOrDefault();

                return objCDT01.T01F02;
            }
        }

        /// <summary>
        /// Sets the current date in the database.
        /// </summary>
        /// <param name="day">The date to set.</param>
        public static void SetDate(DateTime day)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                CDT01 Date = connection.Select<CDT01>().FirstOrDefault();

                if (Date == null)
                {
                    _ = connection.Insert<CDT01>(new CDT01());
                }
                else
                {
                    Date.T01F02 = day;
                    _ = connection.Update<CDT01>(Date);
                }
            }
        }
    }
}
