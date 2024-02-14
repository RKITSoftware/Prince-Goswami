using Advance_C__Final_Demo.BL.Interface;
using Advance_C__Final_Demo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Advance_C__Final_Demo.DAL.Transaction
{
        public class TransactionRepository : IBLTransactionRepository
        {
            private readonly string _connectionString = "Server=127.0.0.1;Port=3306;Database=BankSimulator;User Id=Admin;Password=gs@123;";

            /// <inheritdoc />
            public TRN01 GetTransactionById(int transactionId)
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM TRN01 WHERE N01F01 = @TransactionId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TransactionId", transactionId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new TRN01
                                {
                                    N01F01 = Convert.ToInt32(reader["N01F01"]),
                                    N01F02 = Convert.ToInt32(reader["N01F02"]),
                                    N01F03 = reader["N01F03"].ToString(),
                                    N01F04 = Convert.ToDecimal(reader["N01F04"]),
                                    N01F05 = Convert.ToDateTime(reader["N01F05"])
                                };
                            }
                        }
                    }
                }

                return null; // Transaction not found
            }

            /// <inheritdoc />
            public List<TRN01> GetTransactionsByUserId(int userId)
            {
                List<TRN01> transactions = new List<TRN01>();

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM TRN01 WHERE N01F02 = @UserId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TRN01 transaction = new TRN01
                                {
                                    N01F01 = Convert.ToInt32(reader["N01F01"]),
                                    N01F02 = Convert.ToInt32(reader["N01F02"]),
                                    N01F03 = reader["N01F03"].ToString(),
                                    N01F04 = Convert.ToDecimal(reader["N01F04"]),
                                    N01F05 = Convert.ToDateTime(reader["N01F05"])
                                };

                                transactions.Add(transaction);
                            }
                        }
                    }
                }

                return transactions;
            }

            /// <inheritdoc />
            public void AddTransaction(TRN01 transaction)
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO TRN01 (N01F02, N01F03, N01F04, N01F05) VALUES (@UserId, @TransactionType, @Amount, @TransactionDate)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", transaction.N01F02);
                        command.Parameters.AddWithValue("@TransactionType", transaction.N01F03);
                        command.Parameters.AddWithValue("@Amount", transaction.N01F04);
                        command.Parameters.AddWithValue("@TransactionDate", transaction.N01F05);

                        command.ExecuteNonQuery();
                    }
                }
            }

            /// <inheritdoc />
            public void DeleteTransaction(int transactionId)
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM TRN01 WHERE N01F01 = @TransactionId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TransactionId", transactionId);

                        command.ExecuteNonQuery();
                    }
                }
            }

            // Add other necessary methods for transaction-related operations
        }
    }

