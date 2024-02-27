using System.Linq;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Models;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite.Dapper;
using ServiceStack.OrmLite;
using System;

namespace ATM_Simulation_Demo.DAL
{
    public class LimitService : IBLLimitService
    {
        private readonly IBLLimitRepository _limitRepository;

        public LimitService()
        {
             _limitRepository = new LimitRepository();
        }

        public void AddATMLimit(int accountID)
        {
            try
            {
                LMT01 limit = new LMT01(accountID);
                _limitRepository.AddATMLimit( limit);
            }
            catch {
                throw;
            }
        }

        public void DeleteATMLimit(int accountID)
        {
            throw new System.NotImplementedException();
        }

        public LMT01 GetATMLimitByAccountID(int accountID)
        {
            return _limitRepository.GetATMLimitByAccountID(accountID);
        }

        public void UpdateATMLimit(int accountID, decimal newWithdrawlLimitAmount)
        {
            _limitRepository.UpdateATMLimit(accountID, newWithdrawlLimitAmount);
        }

        public bool VerifyWithdrawal(int accountID, decimal amount)
        {
            DateTime? date = DAL_Helper.GetDate();
            if (date.Value.Date != DateTime.Today)
            {
                DAL_Helper.SetDate(DateTime.Now);
                _limitRepository.ResetAllATMLimits();
            }

            LMT01 limit = _limitRepository.GetATMLimitByAccountID(accountID) as LMT01;

            if(limit.T01F03 > 0 && limit.T01F05+amount < limit.T01F04 ) 
            {
                limit.T01F03--;
                limit.T01F05 += amount;
                _limitRepository.UpdateDailyATMLimit(accountID,limit);
                return true;
            }

            return false;
        }
    }
}
