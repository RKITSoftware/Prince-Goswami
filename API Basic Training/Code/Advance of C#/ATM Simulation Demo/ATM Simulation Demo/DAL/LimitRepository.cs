using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Models.POCO;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Linq;

namespace ATM_Simulation_Demo.DAL
{
    public class LimitRepository : IBLLimitRepository
    {
        #region Private Fields

        /// <summary>
        /// Connection string to the database
        /// </summary>
        private readonly string _connectionString;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for LimitRepository.
        /// </summary>
        public LimitRepository()
        {
            // Initialize the connection string using the helper class
            _connectionString = DateRepository.connectionString;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>

        public void AddATMLimit(LMT01 limit)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                _ = connection.Insert(limit);
            }
        }

        /// <inheritdoc/>

        public LMT01 GetATMLimitByAccountID(int accountID)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                LMT01 objLMT01 = connection.Single<LMT01>(x => x.T01F02 == accountID);

                return objLMT01;
            }
        }

        /// <inheritdoc/>


        public List<LMT01> GetAllATMLimit()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                List<LMT01> list = connection.Select<LMT01>();

                return list;
            }
        }


        /// <inheritdoc/>

        public void ResetAllATMLimits()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                List<LMT01> limits = connection.Select<LMT01>();

                if (limits.Any())
                {
                    foreach (LMT01 limit in limits)
                    {
                        limit.T01F03 = 10; // Reset DailyTransactionLimit to 0
                        limit.T01F04 = 0; // Reset MaxWithdrawalAmount to 0

                        _ = connection.Update(limit);
                    }
                }
            }
        }


        /// <inheritdoc/>

        public void UpdateATMLimit(int accountID, decimal updatedWithdrawlLimit)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                LMT01 existingLimit = connection.Single<LMT01>(x => x.T01F02 == accountID);

                if (existingLimit != null)
                {
                    existingLimit.T01F04 = updatedWithdrawlLimit;

                    _ = connection.Update(existingLimit);
                }
            }
        }

        /// <inheritdoc/>
        public bool UpdateDailyATMLimit(int accountID, decimal balance)
        {
            try
            {

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    LMT01 existingLimit = connection.Single<LMT01>(x => x.T01F02 == accountID);

                    if (existingLimit != null)
                    {
                        existingLimit.T01F03 = existingLimit.T01F03--;
                        existingLimit.T01F05 += balance;

                        _ = connection.Update(existingLimit);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion    }
    }
}