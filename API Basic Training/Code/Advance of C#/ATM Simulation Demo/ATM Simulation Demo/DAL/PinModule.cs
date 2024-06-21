using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.DAL;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.POCO;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ATM_Simulation_Demo.DAL
{
    /// <summary>
    /// Implementation of the PIN module interface.
    /// </summary>
    public class PinModule : IBLPinModule
    {
        #region Private Fields
        /// <summary>
        /// Represents the connection string used to connect to the database.
        /// </summary>
        private readonly string _connectionString = DateRepository.connectionString;
        #endregion

        #region Methods

        /// <inheritdoc />
        public void AssignPin(int id, string newPin)
        {
            if (IsPinValid(newPin))
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = string.Format(@"UPDATE ACC01 
                                                   SET C01F04 = @0
                                                   WHERE C01F01 = @1;", newPin, id);

                    using (MySqlCommand assignPinCommand = new MySqlCommand(
                        query, connection))
                    {
                        assignPinCommand.Parameters.AddWithValue("@NewPin", Convert.ToInt32(newPin));
                        assignPinCommand.Parameters.AddWithValue("@UserId", id);

                        assignPinCommand.ExecuteNonQuery();
                    }

                    Console.WriteLine("PIN assigned successfully.");
                }
            }
            else
            {
                Console.WriteLine("Invalid PIN. Please use a 4-digit numeric PIN.");
            }
        }

        /// <inheritdoc />
        public string ChangePin(int id, string currentPin, string newPin)
        {
            if (VerifyPin(id, currentPin))
            {
                if (IsPinValid(newPin))
                {
                    using (MySqlConnection connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();
                        string query = string.Format(@"UPDATE 
                                                    ACC01 
                                               SET  
                                                    C01F04 = @0 
                                               WHERE 
                                                    C01F01 = @1 
                                               AND 
                                                    C01F04 = @2;", newPin, id, currentPin);
                        int rowEffected = DALHelper.ExecuteNonQuery(query, connection);

                        if (rowEffected > 0)
                        {
                            return "PIN changed successfully.";
                        }
                        else
                        {
                            return "Current PIN verification failed. PIN not changed.";
                        }
                    }
                }
                else
                {
                    return "Invalid new PIN. Please use a 4-digit numeric PIN.";
                }
            }
            else
            {
                return "Current PIN verification failed. PIN not changed.";
            }
        }

        /// <inheritdoc />
        public bool VerifyPin(int Id, string enteredPin)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = string.Format(@"SELECT COUNT(*) 
                 FROM ACC01 
                 WHERE C01F01 = @0
                 AND C01F04 = @1;", Id, enteredPin);

                DataTable dt = DALHelper.ExecuteSelectQuery(query, connection);

                return dt.Rows.Count > 0;
            }
        }

        #endregion

        #region Helper Methods

        // Helper method to validate the PIN format
        private bool IsPinValid(string pin)
        {
            // PIN should be a 4-digit numeric value
            return !string.IsNullOrEmpty(pin) && pin.Length == 4 && int.TryParse(pin, out _);
        }

        #endregion
    }
    }
