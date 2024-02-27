using System.Linq;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Models;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite;

namespace ATM_Simulation_Demo.DAL
{
    public class LimitRepository : IBLLimitRepository
    {
        private readonly string _connectionString;

        public LimitRepository()
        {
            _connectionString = DAL_Helper.connectionString;
        }

        public void AddATMLimit( LMT01 limit)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                connection.Insert(limit);
            }
        }

        public LMT01 GetATMLimitByAccountID(int accountID)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var query = connection.Single<LMT01>(x => x.T01F02 == accountID);

                return query;
            }
        }

        public void ResetAllATMLimits()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var limits = connection.Select<LMT01>();

                if (limits.Any())
                {
                    foreach (var limit in limits)
                    {
                        limit.T01F03 = 0; // Reset DailyTransactionLimit to 0
                        limit.T01F04 = 0; // Reset MaxWithdrawalAmount to 0

                        connection.Update(limit);
                    }
                }
            }
        }

        public void UpdateATMLimit(int accountID, decimal updatedWithdrawlLimit)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var existingLimit = connection.Single<LMT01>(x => x.T01F02 == accountID);

                if (existingLimit != null)
                {
                    existingLimit.T01F04 = updatedWithdrawlLimit;

                    connection.Update(existingLimit);
                }
            }
        }

        public void UpdateDailyATMLimit(int accountID, LMT01 limit)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var existingLimit = connection.Single<LMT01>(x => x.T01F02 == accountID);

                if (existingLimit != null)
                {
                    existingLimit.T01F03 = limit.T01F03;
                    existingLimit.T01F05 = limit.T01F05;

                    connection.Update(existingLimit);
                }
            }
        }
    }
}
