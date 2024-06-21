using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.DAL;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.POCO;
using MySql.Data.MySqlClient;
using ServiceStack;
using ServiceStack.OrmLite;
using static ATM_Simulation_Demo.BAL.BLHelper;

namespace ATM_Simulation_Demo.BAL.Services
{
    /// <summary>
    /// Provides services related to ATM withdrawal limits.
    /// </summary>
    public class LimitService : IBLLimitService
    {
        #region Private Fields
        /// <summary>
        /// instance of the <see cref="LimitRepository"/> class.
        /// </summary>
        private readonly IBLLimitRepository _limitRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LimitService"/> class.
        /// </summary>
        public LimitService()
        {
            _limitRepository = new LimitRepository();
        }
        #endregion
    
        #region Methods

        /// <summary>
        /// Adds an ATM withdrawal limit for the specified account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        public void AddATMLimit(int accountID)
        {
            try
            {
                LMT01 limit = new LMT01(accountID);
                _limitRepository.AddATMLimit(limit);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Retrieves the ATM withdrawal limit for the specified account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <returns>The ATM withdrawal limit for the specified account.</returns>
        public Response GetATMLimitByAccountID(int accountID)
        {
            LMT01 limit = _limitRepository.GetATMLimitByAccountID(accountID);
            return OkResponse("data fetched", limit);
        }

        /// <summary>
        /// Retrieves the ATM withdrawal limit for the specified account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <returns>The ATM withdrawal limit for the specified account.</returns>
        public Response GetAllATMLimit()
        {
            List<LMT01> limit = _limitRepository.GetAllATMLimit();
            return OkResponse("data fetched", limit);
        }

        /// <summary>
        /// Updates the ATM withdrawal limit for the specified account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <param name="newWithdrawlLimitAmount">The new withdrawal limit amount.</param>
        public Response UpdateATMLimit(int accountID, decimal newWithdrawlLimitAmount)
        {
            if (accountID > 0 && newWithdrawlLimitAmount > 10)
            {
                _limitRepository.UpdateATMLimit(accountID, newWithdrawlLimitAmount);
                return OkResponse("Limit updated succesfully");
            }
            return PreConditionFailedResponse("Invalid parameters");
        }

        /// <summary>
        /// Verifies if a withdrawal of the specified amount is within the daily limit for the account.
        /// </summary>
        /// <param name="accountID">The ID of the account.</param>
        /// <param name="amount">The amount to be withdrawn.</param>
        /// <returns>True if the withdrawal is within the daily limit, otherwise false.</returns>
        public bool VerifyWithdrawal(int accountID, decimal amount)
        {
            DateTime? date = DateRepository.GetDate();
            if (date.Value.Date != DateTime.Today)
            {
                DateRepository.SetDate(DateTime.Now);
                _limitRepository.ResetAllATMLimits();
            }

            LMT01 limit = _limitRepository.GetATMLimitByAccountID(accountID) as LMT01;

            if (limit.T01F03 > 0 && limit.T01F05 + amount < limit.T01F04)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Updates the daily ATM withdrawal limit for the specified account.
        /// </summary>
        /// <param name="connection">The MySQL database connection.</param>
        /// <param name="id">The ID of the account.</param>
        /// <param name="balance">The balance after the withdrawal.</param>
        /// <returns>True if the limit was updated successfully, otherwise false.</returns>
        public bool UpdateDailyATMLimit(MySqlConnection connection, int id, decimal balance)
        {
            using (connection)
            {
                connection.Open();

                var existingLimit = connection.Single<LMT01>(x => x.T01F02 == id);

                if (existingLimit != null)
                {
                    existingLimit.T01F03 = existingLimit.T01F03--;
                    existingLimit.T01F05 = existingLimit.T01F05 + balance;

                    connection.Update(existingLimit);
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
