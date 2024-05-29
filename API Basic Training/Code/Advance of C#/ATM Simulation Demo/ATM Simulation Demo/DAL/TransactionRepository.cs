using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.BAL.Services;
using ATM_Simulation_Demo.Models.POCO;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;

namespace ATM_Simulation_Demo.DAL
{
    public class TransactionRepository : IBLTransactionRepository
    {
        private readonly string _connectionString = DAL_Helper.connectionString;
        private readonly IBLLimitService _limitService = new LimitService();

        /// <inheritdoc />
        public decimal AddTransaction(int id, TRN01 transaction)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                decimal newBalance = 0;
                connection.Open();
                MySqlTransaction transactionScope = connection.BeginTransaction();
                try
                {
                    // Insert transaction
                    _ = connection.Insert(transaction);

                    // Update account balance
                    if (transaction.N01F03 == 0)
                    {
                        transaction.N01F04 *= -1;
                    }

                    // Retrieve current balance
                    decimal currentBalance = connection.Single<ACC01>(q => q.C01F01 == id).C01F06;
                    newBalance = currentBalance + transaction.N01F04;

                    // Update account balance
                    _ = connection.Update<ACC01>(new { C01F06 = newBalance }, q => q.C01F01 == id);

                    bool withdrawalVerified = true;
                    // Update limit
                    if (transaction.N01F03 == 0)
                    {
                        withdrawalVerified = _limitService.UpdateDailyATMLimit(connection,id, transaction.N01F04);
                    }
                    if (!withdrawalVerified)
                    {
                        // If withdrawal is not verified, rollback transaction
                        transactionScope.Rollback();
                        Console.WriteLine("Withdrawal limit exceeded or could not be verified.");
                        return -1;
                    }

                    // Commit transaction if all operations are successful
                    transactionScope.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback transaction if there's an exception
                    transactionScope.Rollback();
                    // Handle exception
                    Console.WriteLine("Transaction failed: " + ex.Message);
                }
                return newBalance;
            }
            return -1;
        }

        /// <inheritdoc />
        public List<TRN01> ViewTransactionHistory(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Retrieve transaction history from the database
                List<TRN01> transactionHistory = new List<TRN01>();
                string query = @"SELECT 
                                    N01F01, 
                                    N01F02, 
                                    N01F03, 
                                    N01F04, 
                                    N01F05, 
                                    N01F06 
                                 FROM 
                                    TRN01 
                                 WHERE 
                                    N01F02 = @AccountId";
                using (MySqlCommand command = new MySqlCommand(
                  query , connection))
                {
                    command.Parameters.AddWithValue("@AccountId", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transactionHistory.Add(new TRN01
                            {
                                N01F01 = reader.GetInt32("N01F01"),
                                N01F02 = reader.GetInt32("N01F02"),
                                N01F03 = (TransactionType)Enum.Parse(typeof(TransactionType), reader.GetString("N01F03")),
                                N01F04 = reader.GetDecimal("N01F04"),
                                N01F05 = reader.GetDateTime("N01F05"),
                                N01F06 = reader.GetString("N01F06")
                            });
                        }
                    }
                }

                return transactionHistory;
            }
        }

        /// <inheritdoc />
        public List<TRN01> GetAllTransactions()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Retrieve transaction history from the database
                List<TRN01> transactionHistory = new List<TRN01>();

                using (MySqlCommand command = new MySqlCommand(
                    "SELECT * FROM TRN01 ", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transactionHistory.Add(new TRN01
                            {
                                N01F01 = reader.GetInt32("N01F01"),
                                N01F02 = reader.GetInt32("N01F02"),
                                N01F03 = (TransactionType)Enum.Parse(typeof(TransactionType), reader.GetString("N01F03")),
                                N01F04 = reader.GetDecimal("N01F04"),
                                N01F05 = reader.GetDateTime("N01F05"),
                                N01F06 = reader.GetString("N01F06")
                            });
                        }
                    }
                }

                return transactionHistory;
            }
        }

        public bool VerifyTransaction(int id, decimal amount)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                decimal balance = connection.Single<ACC01>(q => q.C01F01 == id).C01F06;
                return balance - amount > 10;
            }
        }
    }
}
