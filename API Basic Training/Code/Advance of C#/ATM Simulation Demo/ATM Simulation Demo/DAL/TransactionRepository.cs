using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ATM_Simulation_Demo.DAL
{
    public class TransactionRepository : IBLTransactionRepository
    {
        private readonly string _connectionString = DAL_Helper.connectionString;

        
        /// <inheritdoc />
        public ACC01 AddTransaction(ACC01 account, TRN01 transaction)
        {
            if (VerifyTransaction(account.C01F06, transaction.N01F03, transaction.N01F04))
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();


                    // Insert transaction into the database
                    using (MySqlCommand insertTransactionCommand = new MySqlCommand(
                        "INSERT INTO TRN01 (N01F02, N01F03, N01F04, N01F06) " +
                        "VALUES (@AccountId, @TransactionType, @Amount, @Description);", connection))
                    {
                        insertTransactionCommand.Parameters.AddWithValue("@AccountId", account.C01F01);
                        insertTransactionCommand.Parameters.AddWithValue("@TransactionType", Enum.GetName(typeof(TransactionType), transaction.N01F03));
                        insertTransactionCommand.Parameters.AddWithValue("@Amount", transaction.N01F04);
                        insertTransactionCommand.Parameters.AddWithValue("@Description", transaction.N01F06);

                        insertTransactionCommand.ExecuteNonQuery();
                    }

                    // Update balance in the database
                    if (transaction.N01F03 == 0) transaction.N01F04 *= -1;

                    using (MySqlCommand updateBalanceCommand = new MySqlCommand(
                        "UPDATE ACC01 SET C01F06 = C01F06 + @Amount WHERE C01F01 = @AccountId;", connection))
                    {
                        updateBalanceCommand.Parameters.AddWithValue("@Amount", transaction.N01F04);
                        updateBalanceCommand.Parameters.AddWithValue("@AccountId", account.C01F01);

                        updateBalanceCommand.ExecuteNonQuery();
                    }                
                }
            }
            return account;
        }

        /// <inheritdoc />
        public List<TRN01> ViewTransactionHistory(ACC01 account)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Retrieve transaction history from the database
                List<TRN01> transactionHistory = new List<TRN01>();

                using (MySqlCommand command = new MySqlCommand(
                    "SELECT * FROM TRN01 WHERE N01F02 = @AccountId;", connection))
                {
                    command.Parameters.AddWithValue("@AccountId", account.C01F01);

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

        private bool VerifyTransaction(decimal balance, TransactionType transactionType, decimal amount)
        {
            if (transactionType == TransactionType.Debit && balance - amount <= 10)
            {
                return false;
            }
            return true;
        }
    }
}
