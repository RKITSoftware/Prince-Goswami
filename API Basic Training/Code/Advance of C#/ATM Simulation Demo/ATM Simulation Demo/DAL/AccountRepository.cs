using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.DAL;
using ATM_Simulation_Demo.Others.Security;
using System.Security.Principal;
using ATM_Simulation_Demo.Models.POCO;
using System.Net.Http.Headers;
using ServiceStack.OrmLite;
using Org.BouncyCastle.Asn1.X509;

namespace ATM_Simulation_Demo.DAL
{
    public class AccountRepository : IBLAccountRepository
    {
        #region Private Fields
        /// <summary>
        /// Connection string to the database
        /// </summary>
        private readonly string _connectionString = DateRepository.connectionString;

        /// <summary>
        /// Interface instance for the PinModule business logic
        /// </summary>
        private readonly IBLPinModule _PinModule;

        /// <summary>
        /// Instance of the ACC01 class for user account operations
        /// </summary>
        private ACC01 _objACC01 = new ACC01();
        #endregion

        #region Constructor

        public AccountRepository(IBLPinModule _pinModule)
        {
            this._PinModule = _pinModule;
        }
        #endregion

        #region Public Methods
        /// <inheritdoc />
        public int AddAccount(ACC01 newAccount)
        {
            if (newAccount == null)
            {
                throw new ArgumentNullException(nameof(newAccount), "Account cannot be null.");
            }

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var TransactionScope = connection.BeginTransaction();
                try
                {
                    //Add Account
                    int accountId = (int)connection.Insert<ACC01>(newAccount);

                    if (accountId > 0)
                    {

                        // Add Limit
                        LMT01 limit = new LMT01(accountId);
                        connection.Insert<LMT01>(limit);
                        TransactionScope.Commit();

                        return accountId;
                    }
                    else
                    {
                        TransactionScope.Rollback();
                        return 0;
                    }

                }
                catch (Exception ex)
                {
                    TransactionScope.Rollback();
                    return 0;
                }
            }
        }

        /// <inheritdoc />
        public int UpdateAccount(ACC01 account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");
            }

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                int rowsAffected = connection.Update<ACC01>(account);
                return rowsAffected;
            }
        }

        /// <inheritdoc />
        public ACC01 GetAccount(string cardNumber, string pin)
        {

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = string.Format(@"SELECT 
                                    C01F01,
                                    C01F02,
                                    C01F03,
                                    C01F04,
                                    C01F05,
                                    C01F06,
                                 FROM 
                                    ACC01 
                                WHERE 
                                    C01F02 = @0 AND 
                                    C01F04 = @1;", cardNumber, pin);
                using (MySqlCommand command = new MySqlCommand(
                    query, connection))
                {
                    command.Parameters.AddWithValue("@CardNumber", cardNumber);
                    command.Parameters.AddWithValue("@PIN", BLCrypto.Encrypt(pin));

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _objACC01.C01F01 = reader.GetInt32("C01F01");
                            _objACC01.C01F02 = reader.GetString("C01F02");
                            _objACC01.C01F03 = reader.GetString("C01F03");
                            _objACC01.C01F04 = BLCrypto.Decrypt(reader.GetString("C01F04"));
                            _objACC01.C01F05 = reader.GetString("C01F05");
                            _objACC01.C01F06 = reader.GetDecimal("C01F06");
                        }
                    }
                }

                return _objACC01;
            }
        }

        /// <inheritdoc />
        public ACC01 GetAccountByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = string.Format(@"SELECT 
                                    C01F01,
                                    C01F02,
                                    C01F03,
                                    C01F04,
                                    C01F05,
                                    C01F06,
                                 FROM 
                                    ACC01 
                                WHERE 
                                    C01F01 = @0 ", id);
                using (MySqlCommand command = new MySqlCommand(query
                    , connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _objACC01.C01F01 = reader.GetInt32("C01F01");
                            _objACC01.C01F02 = reader.GetString("C01F02");
                            _objACC01.C01F03 = reader.GetString("C01F03");
                            _objACC01.C01F04 = BLCrypto.Decrypt(reader.GetString("C01F04"));
                            _objACC01.C01F05 = reader.GetString("C01F05");
                            _objACC01.C01F06 = reader.GetDecimal("C01F06");
                        }
                    }
                }

                return _objACC01;
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
                string query = string.Format(@"SELECT 
                                                     COUNT(C01F02) 
                                               FROM 
                                                     ACC01 
                                               WHERE C01F02 = @0;", cardNumber);
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        /// <inheritdoc />
        public bool UpdateMobileNumber(int accountId, string newMobileNumber)
        {
            if (accountId >= 0)
            {

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (MySqlCommand updateMobileNumberCommand = new MySqlCommand(
                        "UPDATE ACC01 SET C01F05 = @NewMobileNumber WHERE C01F01 = @AccountId;", connection))
                    {
                        updateMobileNumberCommand.Parameters.AddWithValue("@NewMobileNumber", newMobileNumber);
                        updateMobileNumberCommand.Parameters.AddWithValue("@AccountId", accountId);

                        updateMobileNumberCommand.ExecuteNonQuery();
                    }
                }
                return true;
            }
            else
                return false;
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
        public bool Delete(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(
                    $"DELETE FROM ACC01 WHERE C01F01 = {id});", connection))
                {
                    int rowsEffected = command.ExecuteNonQuery();

                    return rowsEffected > 0;
                }

            }
        }

        /// <inheritdoc />
        public bool IsUserExists(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid ID.", nameof(id));
            }
            //// string . format
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(
                    $"SELECT COUNT(*) FROM ACC01 WHERE C01F01 ={id};", connection))
                {

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        #endregion

    }
}



