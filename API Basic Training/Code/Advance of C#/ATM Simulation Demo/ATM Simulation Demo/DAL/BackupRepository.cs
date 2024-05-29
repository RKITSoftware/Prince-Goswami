using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Web;

namespace ATM_Simulation_Demo.DAL
{
    public class BackupRepository
    {
        #region privateFields
        private readonly string _connectionString = DAL_Helper.connectionString;
        #endregion

        #region Methods

        public string GetBackupData()
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

            string JSONobject = null;
            JSONobject = JsonConvert.SerializeObject(ds);

            return JSONobject;
        }

        #endregion
    }
}