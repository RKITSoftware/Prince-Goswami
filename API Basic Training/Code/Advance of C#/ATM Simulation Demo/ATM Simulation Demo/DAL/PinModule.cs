using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.DAL;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.POCO;
using MySql.Data.MySqlClient;
using System;

namespace ATM_Simulation_Demo.DAL
{
    /// <summary>
    /// Implementation of the PIN module interface.
    /// </summary>
    public class PinModule : IBLPinModule
    {
        private readonly string _connectionString = DAL_Helper.connectionString;

        /// <inheritdoc />
        public void AssignPin(int id, string newPin)
        {
            if (IsPinValid(newPin))
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (MySqlCommand assignPinCommand = new MySqlCommand(
                        "UPDATE ACC01 SET C01F04 = @NewPin WHERE C01F01 = @UserId;", connection))
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

                        using (MySqlCommand changePinCommand = new MySqlCommand(
                            "UPDATE ACC01 SET C01F04 = @NewPin WHERE C01F01 = @UserId AND C01F04 = @CurrentPin;", connection))
                        {
                            changePinCommand.Parameters.AddWithValue("@NewPin", Convert.ToInt32(newPin));
                            changePinCommand.Parameters.AddWithValue("@UserId", id);
                            changePinCommand.Parameters.AddWithValue("@CurrentPin", Convert.ToInt32(currentPin));

                            int rowsAffected = changePinCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                return "PIN changed successfully.";
                            }
                            else
                            {
                                return "Current PIN verification failed. PIN not changed.";
                            }
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
                return  "Current PIN verification failed. PIN not changed.";
            }
        }

        /// <inheritdoc />
        public bool VerifyPin(int Id, string enteredPin)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand verifyPinCommand = new MySqlCommand(
                    "SELECT COUNT(*) FROM ACC01 WHERE C01F01 = @UserId AND C01F04 = @EnteredPin;", connection))
                {
                    verifyPinCommand.Parameters.AddWithValue("@UserId", Id);
                    verifyPinCommand.Parameters.AddWithValue("@EnteredPin", enteredPin);

                    int count = Convert.ToInt32(verifyPinCommand.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        // Helper method to validate the PIN format
        private bool IsPinValid(string pin)
        {
            // PIN should be a 4-digit numeric value
            return !string.IsNullOrEmpty(pin) && pin.Length == 4 && int.TryParse(pin, out _);
        }
    }
}
