using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.BAL.Services;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.POCO;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;

namespace ATM_Simulation_Demo.DAL
{
    public class TransactionRepository : IBLTransactionRepository
    {
        #region Private Fields
        /// <summary>
        /// Represents the connection string used to connect to the database.
        /// </summary>
        private readonly string _connectionString = DateRepository.connectionString;

        /// <summary>
        /// Represents the service for managing limits.
        /// </summary>
        private readonly IBLLimitService _limitService = new LimitService();
        #endregion

        #region Public Methods

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



                    // Retrieve current balance
                    decimal currentBalance = connection.Single<ACC01>(q => q.C01F01 == id).C01F06;
                    newBalance = currentBalance + transaction.N01F04;

                    // Update account balance
                    _ = connection.Update<ACC01>(new { C01F06 = newBalance }, q => q.C01F01 == id);

                    bool withdrawalVerified = true;
                    // Update limit
                    if (transaction.N01F03 == 0)
                    {
                        withdrawalVerified = _limitService.UpdateDailyATMLimit(connection, id, transaction.N01F04);
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
        public DataTable ViewTransactionHistory(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Retrieve transaction history from the database
                string query = string.Format(@"SELECT 
                    C01F03
                    N01F03, 
                    N01F04, 
                    N01F05, 
                    N01F06,
                    CASE 
                        WHEN N01F03 = 0 THEN 'Debit'
                        ELSE 'Credit'
                    END AS TransactionType
                 FROM 
                    TRN01 
                 INNER JOIN ACC01
                    ACC01.C010F1 = TRN01.N010F2
                 WHERE 
                    N01F02 = @0
                 ORDER BY N01F05 DESC;", id);

                DataTable dt = DALHelper.ExecuteSelectQuery(query, connection);
                return dt;
            }
        }

        /// <inheritdoc />
        public DataTable GetAllTransactions()
        {
            DataTable dt;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Retrieve transaction history from the database
                List<TRN01> transactionHistory = new List<TRN01>();

                string query = @"SELECT 
                                    N01F01,
                                    C01F02,
                                    C01F03,
                                    N01F03,
                                    N01F04,
                                    N01F05,
                                    N01F06 
                                CASE 
                                    WHEN N01F03 = 0 THEN 'Debit'
                                    ELSE 'Credit'
                                END AS TransactionType
                                FROM TRN01 
                                INNER JOIN ACC01
                                    ACC01.C010F1 = TRN01.N010F2
                                ORDER BY N01F05 DESC,C01F02";
                dt = DALHelper.ExecuteSelectQuery(query, connection);
            }

            return dt;
        }


        /// <inheritdoc/>
        public bool VerifyTransaction(int id, decimal amount)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                decimal balance = connection.Single<ACC01>(q => q.C01F01 == id).C01F06;
                return balance - amount > 0;
            }
        }

        #endregion    }
    }
