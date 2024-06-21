using MySql.Data.MySqlClient;
using System.Data;

namespace ATM_Simulation_Demo.DAL
{
    /// <summary>
    /// Represents a repository for retrieving backup data from the database.
    /// </summary>
    public class BackupRepository
    {
        #region Private Fields

        /// <summary>
        /// The connection string used to connect to the database.
        /// </summary>
        private readonly string _connectionString = DateRepository.connectionString;

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves backup data from the database and returns it as a DataSet.
        /// </summary>
        /// <returns>A DataSet containing backup data.</returns>
        public DataSet GetBackupData()
        {
            DataTable accounts = new DataTable("Accounts");
            DataTable transactions = new DataTable("Transactions");

            string fetchAccounts = $@"SELECT 
                                   C01F02 as CardNumber,
                                   C01F03 as Name,
                                   C01F05 as MobileNumber,
                                   C01F06 as Balance,
                                   T01F03 as Daily_Transaction_Limit,
                                   T01F04 as Max_Withdrawl_Limit,  
                                   T01F05 as Used_Withdrawl_Limit
                                FROM
                                    ACC01 C01
                                JOIN
                                    LMT01 T01
                                ON T01.T01F02 = C01.C01F01";

            string fetchTransactions = $@"SELECT 
                                   C01F02 AS CardNumber,
                                   C01F03 AS Name, 
                                   N01F03 AS TransactionType,
                                   N01F04 AS Amount,
                                   N01F05 AS TransactionDate,
                                   N01F06 AS Description
                                FROM
                                    TRN01 N01
                                INNER JOIN 
                                    ACC01 C01
                                ON 
                                    C01.C01F01 = N01.N01F02";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(fetchAccounts, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            accounts.Load(reader);
                        }
                    }
                }
                using (MySqlCommand command = new MySqlCommand(fetchTransactions, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            transactions.Load(reader);
                        }
                    }
                }
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(accounts);
            ds.Tables.Add(transactions);

            return ds;
        }

        #endregion
    }
}
