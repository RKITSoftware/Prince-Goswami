using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.POCO;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ATM_Simulation_Demo.DAL
{
    public class DAL_Helper
    {
        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public static DateTime GetDate()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                return connection.Query<DateTime>("SELECT T01F02 FROM CDT01 WHERE T01F01 = 1").FirstOrDefault();
            }
        }

        public static void SetDate(DateTime day)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();


                var Date = connection.Query<CDT01>("SELECT * FROM CDT01").FirstOrDefault();

                if (Date == null)
                {
                    connection.Insert<CDT01>(new CDT01());
                }
                else
                {
                    Date.T01F02 = day;
                    connection.Update<CDT01>(Date);
                }
            }
        }
    }
}