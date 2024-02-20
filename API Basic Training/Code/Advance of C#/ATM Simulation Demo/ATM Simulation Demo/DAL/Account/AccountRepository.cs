using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.DAL;
using ATM_Simulation_Demo.BAL.Security;
using System.Security.Principal;

namespace ATM_Simulation_Demo.DAL.Account
{
    public class AccountRepository : IBLAccountRepository
    {
        private readonly string _connectionString = DAL_Helper.connectionString;
        private readonly IBLPinModule _PinModule;
        public AccountRepository(IBLPinModule _pinModule)
        {
            this._PinModule = _pinModule;
        }

        /// <inheritdoc />
        public void AddAccount(ACC01 newAccount)
        {
            if (newAccount == null)
            {
                throw new ArgumentNullException(nameof(newAccount), "Account cannot be null.");
            }

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand insertAccountCommand = new MySqlCommand(
                    "INSERT INTO ACC01 (C01F02, C01F03, C01F04, C01F05, C01F06) " +
                    "VALUES (@CardNumber, @Name, @PIN, @MobileNumber, @Balance);", connection))
                {
                    insertAccountCommand.Parameters.AddWithValue("@CardNumber", newAccount.C01F02);
                    insertAccountCommand.Parameters.AddWithValue("@Name", newAccount.C01F03);
                    insertAccountCommand.Parameters.AddWithValue("@PIN", BLCrypto.Encrypt(newAccount.C01F04));
                    insertAccountCommand.Parameters.AddWithValue("@MobileNumber", newAccount.C01F05);
                    insertAccountCommand.Parameters.AddWithValue("@Balance", newAccount.C01F06);

                    insertAccountCommand.ExecuteNonQuery();
                }
            }
        }

        /// <inheritdoc />
        public void UpdateAccount(ACC01 account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");
            }

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand updateAccountCommand = new MySqlCommand(
                    "UPDATE ACC01 SET C01F02 = @CardNumber, C01F03 = @Name, " +
                    "C01F04 = @PIN, C01F05 = @MobileNumber, C01F06 = @Balance " +
                    "WHERE C01F01 = @AccountId;", connection))
                {
                    updateAccountCommand.Parameters.AddWithValue("@CardNumber", account.C01F02);
                    updateAccountCommand.Parameters.AddWithValue("@Name", account.C01F03);
                    updateAccountCommand.Parameters.AddWithValue("@PIN",BLCrypto.Encrypt(account.C01F04));
                    updateAccountCommand.Parameters.AddWithValue("@MobileNumber", account.C01F05);
                    updateAccountCommand.Parameters.AddWithValue("@Balance", account.C01F06);
                    updateAccountCommand.Parameters.AddWithValue("@AccountId", account.C01F01);

                    updateAccountCommand.ExecuteNonQuery();
                }
            }
        }

        /// <inheritdoc />
        public ACC01 GetAccount(string cardNumber, string pin)
        {
            if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(pin))
            {
                throw new ArgumentException("Card number and PIN cannot be null or empty.");
            }

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(
                    "SELECT * FROM ACC01 WHERE C01F03 = @CardNumber AND C01F04 = @PIN;", connection))
                {
                    command.Parameters.AddWithValue("@CardNumber", cardNumber);
                    command.Parameters.AddWithValue("@PIN", BLCrypto.Encrypt(pin));

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ACC01
                            {
                                C01F01 = reader.GetInt32("C01F01"),
                                C01F02 = reader.GetString("C01F02"),
                                C01F03 = reader.GetString("C01F03"),
                                C01F04 = BLCrypto.Decrypt(reader.GetString("C01F04")),
                                C01F05 = reader.GetString("C01F05"),
                                C01F06 = reader.GetDecimal("C01F06")
                            };
                        }
                    }
                }

                return null;
            }
        }

        /// <inheritdoc />
        public ACC01 GetAccountByID(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid account ID");
            }

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(
                    "SELECT * FROM ACC01 WHERE C01F01 = @AccountId;", connection))
                {
                    command.Parameters.AddWithValue("@AccountId", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ACC01
                            {
                                C01F01 = reader.GetInt32("C01F01"),
                                C01F02 = reader.GetString("C01F02"),
                                C01F03 = reader.GetString("C01F03"),
                                C01F04 = BLCrypto.Decrypt(reader.GetString("C01F04")),
                                C01F05 = reader.GetString("C01F05"),
                                C01F06 = reader.GetDecimal("C01F06")
                            };
                        }
                    }
                }

                return null;
            }
        }

        /// <inheritdoc />
        public bool IsCardNumberExists(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                throw new ArgumentException("Card number cannot be null or empty.", nameof(cardNumber));
            }

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(
                    "SELECT COUNT(*) FROM ACC01 WHERE C01F02 = @CardNumber;", connection))
                {
                    command.Parameters.AddWithValue("@CardNumber", cardNumber);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        /// <inheritdoc />
        public void ChangePin(ACC01 account, string currentPin, string newPin)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");
            }

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand changePinCommand = new MySqlCommand(
                    "UPDATE ACC01 SET C01F04 = @NewPin WHERE C01F01 = @AccountId AND C01F04 = @CurrentPin;", connection))
                {
                    changePinCommand.Parameters.AddWithValue("@NewPin", BLCrypto.Encrypt(newPin));
                    changePinCommand.Parameters.AddWithValue("@AccountId", account.C01F01);
                    changePinCommand.Parameters.AddWithValue("@CurrentPin", BLCrypto.Encrypt(currentPin));

                    int rowsAffected = changePinCommand.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new InvalidOperationException("Invalid current PIN. Unable to change PIN.");
                    }
                }
            }
        }

        /// <inheritdoc />
        public void UpdateMobileNumber(ACC01 account, string newMobileNumber)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");
            }

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand updateMobileNumberCommand = new MySqlCommand(
                    "UPDATE ACC01 SET C01F05 = @NewMobileNumber WHERE C01F01 = @AccountId;", connection))
                {
                    updateMobileNumberCommand.Parameters.AddWithValue("@NewMobileNumber", newMobileNumber);
                    updateMobileNumberCommand.Parameters.AddWithValue("@AccountId", account.C01F01);

                    updateMobileNumberCommand.ExecuteNonQuery();
                }
            }
        }

        /// <inheritdoc />
        public List<ACC01> GetAllAccounts()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                List<ACC01> accounts = new List<ACC01>();

                using (MySqlCommand command = new MySqlCommand(
                    "SELECT * FROM ACC01;", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accounts.Add(new ACC01
                            {
                                C01F01 = reader.GetInt32("C01F01"),
                                C01F02 = reader.GetString("C01F02"),
                                C01F03 = reader.GetString("C01F03"),
                                C01F04 = BLCrypto.Decrypt(reader.GetString("C01F04")),
                                C01F05 = reader.GetString("C01F05"),
                                C01F06 = reader.GetDecimal("C01F06")
                            });
                        }
                    }
                }

                return accounts;
            }
        }

        /// <inheritdoc />
        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid account ID");
            }

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(
                    "DELETE FROM ACC01 WHERE C01F01 = @AccountId;", connection))
                {
                    command.Parameters.AddWithValue("@AccountId", id);
                    command.ExecuteNonQuery();
                }

            }
        }

    }
}



