using DB_CRUD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;

namespace DB_CRUD.BL
{
    /// <summary>
    /// Business logic class for handling CRUD operations on BNK01 table
    /// </summary>
    public class BLBank
    {
        private readonly string _connectionString = "Server=127.0.0.1;Port=3306;Database=BankSimulator;User Id=Admin;Password=gs@123;";

        #region GetAll
        /// <summary>
        /// Retrieves all banks from the BNK01 table
        /// </summary>
        /// <returns>List of BNK01 objects representing banks</returns>

        public List<BNK01> GetAll()
        {
            MySqlConnection connection = new MySqlConnection(this._connectionString);
            List<BNK01> bankList = new List<BNK01>();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM BNK01;", connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bankList.Add(new BNK01()
                            {
                                K01F01 = (int)reader[0],
                                K01F02 = (string)reader[1],
                                K01F03 = (string)reader[2],
                            });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return bankList;
        }
        #endregion

        #region Add
        /// <summary>
        /// Adds a new bank to the BNK01 table
        /// </summary>
        /// <param name="bank">Bank data to be added</param>
        /// <returns>HTTP response</returns>

        public HttpResponseMessage Add(BNK01 bank)
        {
            MySqlConnection connection = new MySqlConnection(this._connectionString);
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO BNK01 (K01F02, K01F03) VALUES (@Name, @ShortName)";

                    cmd.Parameters.AddWithValue("@Name", bank.K01F02);
                    cmd.Parameters.AddWithValue("@ShortName", bank.K01F03);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
            }
            finally { connection.Close(); }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Data Added")
            };
        }
        #endregion

        #region Update

        /// <summary>
        /// Updates an existing bank in the BNK01 table
        /// </summary>
        /// <param name="Id">Bank ID to be updated</param>
        /// <param name="bank">Updated bank data</param>
        /// <returns>HTTP response</returns>
        public HttpResponseMessage Update(int Id, BNK01 bank)
        {
            MySqlConnection connection = new MySqlConnection(this._connectionString);
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE BNK01 " +
                                        "SET K01F02 = @Name, " +
                                        "K01F03 = @ShortName " +
                                        "WHERE K01F01 = @Id ";
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@Name", bank.K01F02);
                    cmd.Parameters.AddWithValue("@ShortName", bank.K01F03);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { connection.Close(); }

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Data Update")
            };
        }
        #endregion

        #region Delete

        /// <summary>
        /// Deletes a bank from the BNK01 table by ID
        /// </summary>
        /// <param name="bankId">Bank ID to be deleted</param>
        /// <returns>HttpResponseMessage</returns>
        public HttpResponseMessage Delete(int bankId)
        {
            MySqlConnection connection = new MySqlConnection(this._connectionString);
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM BNK01 " +
                                        "WHERE K01F01 = @Id";

                    cmd.Parameters.AddWithValue("@Id", bankId);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { connection.Close(); }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Data Delete")
            };
        }
        #endregion
    }
}
