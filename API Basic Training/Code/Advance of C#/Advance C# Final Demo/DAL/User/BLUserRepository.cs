using Advance_C__Final_Demo.BL.Enum;
using Advance_C__Final_Demo.BL.Interface;
using Advance_C__Final_Demo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Advance_C__Final_Demo.DAL.User
{
    public class UserRepository : IBLUserRepository
    {
        private readonly string _connectionString = "Server=127.0.0.1;Port=3306;Database=BankSimulator;User Id=Admin;Password=gs@123;";

        /// <inheritdoc />
        public USR01 GetUserById(int userId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM USR01 WHERE R01F01 = @UserId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new USR01
                            {
                                R01F01 = Convert.ToInt32(reader["R01F01"]),
                                R01F02 = reader["R01F02"].ToString(),
                                R01F03 = Convert.ToInt32(reader["R01F03"]),
                                R01F04 = reader["R01F04"].ToString(),
                                R01F05 = Convert.ToDecimal(reader["R01F04"])
                                // Map other properties
                            };
                        }
                    }
                }
            }

            return null; // User not found
        }

        /// <inheritdoc />
        public USR01 GetUserByCardNumber(string cardNumber)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM USR01 WHERE R01F02 = @CardNumber";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CardNumber", cardNumber);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new USR01
                            {
                                R01F01 = Convert.ToInt32(reader["R01F01"]),
                                R01F02 = reader["R01F02"].ToString(),
                                R01F03 = Convert.ToInt32(reader["R01F03"]),
                                R01F04 = reader["R01F04"].ToString(),
                                R01F05 = Convert.ToDecimal(reader["R01F04"])
                            };
                        }
                    }
                }
            }

            return null; // User not found
        }

        /// <inheritdoc />
        public void UpdateUserBalance(int userId, decimal newBalance)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE USR01 SET R01F05 = @NewBalance WHERE R01F01 = @UserId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewBalance", newBalance);
                    command.Parameters.AddWithValue("@UserId", userId);

                    command.ExecuteNonQuery();
                }
            }
        }

        /// <inheritdoc />
        public List<USR01> GetAllUsers()
        {
            List<USR01> users = new List<USR01>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM USR01";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            USR01 user = new USR01
                            {
                                R01F01 = Convert.ToInt32(reader["R01F01"]),
                                R01F02 = reader["R01F02"].ToString(),
                                R01F03 = Convert.ToInt32(reader["R01F03"]),
                                R01F04 = reader["R01F04"].ToString(),
                                R01F05 = Convert.ToDecimal(reader["R01F04"])
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        /// <inheritdoc />
        public void AddUser(USR01 user)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO USR01 (R01F02, R01F05) VALUES (@CardNumber, @Balance)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CardNumber", user.R01F02);
                    command.Parameters.AddWithValue("@Balance", user.R01F05);

                    command.ExecuteNonQuery();
                }
            }
        }

        /// <inheritdoc />
        public void DeleteUser(int userId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM USR01 WHERE R01F01 = @UserId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    command.ExecuteNonQuery();
                }
            }
        }

        /// <inheritdoc />
        public List<TRN01> GetTransactionHistory(int userId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM TRN01 WHERE N01F02 = @UserId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        List<TRN01> transactions = new List<TRN01>();

                        while (reader.Read())
                        {
                            TRN01 transaction = new TRN01
                            {
                                N01F01 = Convert.ToInt32(reader["N01F01"]),
                                N01F02 = Convert.ToInt32(reader["N01F02"]),
                                N01F03 = EnumConverter.ConvertToEnum<TransactionType>(reader["N01F03"].ToString()),
                                N01F04 = Convert.ToDecimal(reader["N01F04"]),
                                N01F05 = Convert.ToDateTime(reader["N01F05"])
                            };

                            transactions.Add(transaction);
                        }

                        return transactions;
                    }
                }
            }
        }
    }
}
