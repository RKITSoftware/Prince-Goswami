using DB_CRUD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using DB_CRUD.Extension;
using System.Data.Odbc;

namespace DB_CRUD.BL
{
    /// <summary>
    /// Business logic class for handling CRUD operations on BNK01 table
    /// </summary>
    public class BLBank
    {
        #region private fields
        private readonly string _connectionString;
        private BNK01 _objBNK01;
        #endregion

        #region Public Fields
        public Response objResponse;
        public EnmOperation Operation;
        #endregion

        #region Constructor
        public BLBank()
        {
            _connectionString = "Server=127.0.0.1;Port=3306;Database=BankSimulator;User Id=Admin;Password=gs@123;";
            objResponse = new Response();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Retrieves all banks from the BNK01 table
        /// </summary>
        /// <returns>List of BNK01 objects representing banks</returns>
        public Response GetAll()
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
                        objResponse.Data = bankList;
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
            return objResponse;
        }

        /// <summary>
        /// Prepares the DTO object for saving by converting it to the corresponding model object.
        /// </summary>
        /// <param name="objDTOBNK01">The DTO object to be converted.</param>
        public void presave(DTOBNK01 objDTOBNK01)
        {
            // This method prepares the DTO object for saving by converting it to the corresponding model object.
            _objBNK01 = objDTOBNK01.Convert<BNK01>();
        }

        /// <summary>
        /// Validates the data before performing a specific Operation (Add, Edit).
        /// </summary>
        /// <param name="Operation">The Operation type (Add or Edit).</param>
        /// <returns>A response object indicating success or failure of the validation.</returns>
        public Response Validation()
        {
            // This method validates the data before performing a specific Operation (Add, Edit).
            Response objResponse = new Response();

            if (Operation == EnmOperation.A)
            {
                // Check if short name is unique for adding a new record.
                if (IsShortNameUnique(_objBNK01.K01F03))
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Bank Short Name must be unique.";
                    return objResponse;
                }
            }
            else if (Operation == EnmOperation.E)
            {
                // Check if ID exists for editing an existing record.
                if (!IsIdExists(_objBNK01.K01F01))
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Bank with the provided ID does not exist.";
                    return objResponse;
                }
                //// make it dynamic using opr flag
                // Check if short name is unique excluding the current bank for editing.
                if (IsShortNameUnique(_objBNK01.K01F03, _objBNK01.K01F01))
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Bank Short Name must be unique.";
                    return objResponse;
                }
            }
            else
            {
                // Invalid Operation type.
                objResponse.IsError = true;
                objResponse.Message = "Invalid Operation type.";
                return objResponse;
            }

            return objResponse;
        }

        /// <summary>
        /// Validates the data before deleting a record.
        /// </summary>
        /// <param name="id">The ID of the record to be deleted.</param>
        /// <returns>A response object indicating success or failure of the validation.</returns>
        public Response ValidationOnDelete(int id)
        {
            // This method validates the data before deleting a record.
            Response objResponse = new Response();

            // Check if ID exists before deleting.
            if (!IsIdExists(id))
            {
                objResponse.IsError = true;
                objResponse.Message = "Bank with the provided ID does not exist.";
            }
            return objResponse;
        }

        /// <summary>
        /// Saves the bank entity based on the specified Operation type (Add, Edit, Delete).
        /// </summary>
        /// <param name="Operation">The type of Operation to perform (Add, Edit, Delete).</param>
        /// <returns>A response object indicating the outcome of the save Operation.</returns>
        public Response Save()
        {
            // Initialize the response message.
            string message = "";
            MySqlConnection connection = new MySqlConnection(this._connectionString);

            // Perform the Operation based on the specified type.
            switch (Operation)
            {
                case EnmOperation.A:
                    // Add Operation: Insert the bank entity into the database.
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.Connection = connection;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "INSERT INTO BNK01 (K01F02, K01F03) VALUES (@Name, @ShortName)";

                            cmd.Parameters.AddWithValue("@Name", _objBNK01.K01F02);
                            cmd.Parameters.AddWithValue("@ShortName", _objBNK01.K01F03);

                            connection.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = ex.Message;
                        return objResponse;
                    }
                    finally { connection.Close(); }

                    message = "data inserted successfully";
                    break;

                case EnmOperation.E:
                    // Edit Operation: Update the existing bank entity in the database.
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
                            cmd.Parameters.AddWithValue("@Id", _objBNK01.K01F01);
                            cmd.Parameters.AddWithValue("@Name", _objBNK01.K01F02);
                            cmd.Parameters.AddWithValue("@ShortName", _objBNK01.K01F03);
                            connection.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = ex.Message;
                        return objResponse;
                    }
                    finally { connection.Close(); }

                    message = "data updated successfully";
                    break;


                default:
                    // Invalid Operation type.
                    return new Response { IsError = true, Message = "Invalid Operation type." };
            }

            // Update the response message and return the response object.
            objResponse.Message = message;
            return objResponse;
        }

        /// <summary>
        /// Adds a new bank to the BNK01 table
        /// </summary>
        /// <param name="bank">Bank data to be added</param>
        /// <returns>HTTP response</returns>
        public Response Add(BNK01 bank)
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
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
            finally { connection.Close(); }

            objResponse.Message = "Data added succesfully";
            return objResponse;
        }

        /// <summary>
        /// Deletes a bank from the BNK01 table by ID
        /// </summary>
        /// <param name="bankId">Bank ID to be deleted</param>
        /// <returns>HttpResponseMessage</returns>
        public Response Delete(int bankId)
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
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
            finally { connection.Close(); }
            objResponse.Message = "Data deleted succesfully";
            return objResponse;
        }

        #endregion

        #region Helper Methods
        // Method to check if the ID exists
        private bool IsIdExists(int id)
        {
            MySqlConnection connection = new MySqlConnection(this._connectionString);
            DataTable dt = new DataTable();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT K01F01 FROM BNK01 " +
                                    "WHERE K01F01 = @Id";

                cmd.Parameters.AddWithValue("@Id", id);


                connection.Open();
                cmd.ExecuteNonQuery();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
                connection.Close();
                return dt.Rows.Count > 0;
            }
        }

        // Method to check if the short name is unique
        private bool IsShortNameUnique(string shortName, int? currentId = null)
        {
            MySqlConnection connection = new MySqlConnection(this._connectionString);
            DataTable dt = new DataTable();

            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT K01F01 FROM BNK01 " +
                                    "WHERE K01F01 != @currentId " +
                                    "AND K01F03 = @shortName";

                cmd.Parameters.AddWithValue("@currentId", currentId);
                cmd.Parameters.AddWithValue("@shortName", shortName);



                connection.Open();
                cmd.ExecuteNonQuery();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
                connection.Close();
                return (dt.Rows.Count > 0);
            }
            #endregion
        } 
    }
}
