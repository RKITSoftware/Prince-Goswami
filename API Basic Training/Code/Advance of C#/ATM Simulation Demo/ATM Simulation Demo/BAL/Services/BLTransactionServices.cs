using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Models.POCO;
using ATM_Simulation_Demo.Models.DTO;
using ATM_Simulation_Demo.Others.Security;
using static ATM_Simulation_Demo.BAL.BLHelper;
using ZstdSharp.Unsafe;
using ATM_Simulation_Demo.Extension;
using ATM_Simulation_Demo.Others.Auth;
using ServiceStack;
using System.Transactions;
using ServiceStack.Data;
using ATM_Simulation_Demo.DAL;
using System.Web;
using ServiceStack.OrmLite;
using System.Data;

namespace ATM_Simulation_Demo.BAL.Services
{
    public class TransactionService : IBLTransactionService
    {
        #region Private Properties
        private readonly IBLAccountRepository _accountRepository;
        private readonly IBLTransactionRepository _transactionRepository;
        private readonly IBLLimitService _limitService;
        private TRN01 _objTRN01;
        private readonly string _connectionString;
        private readonly IDbConnectionFactory dbFactory;
        #endregion

        #region Public Properties

        /// <summary>
        /// Specifies the operation to perform.
        /// </summary>
        public enmOperation Type { get; set; }

        #endregion Public Properties

        #region Constructor

        public TransactionService(IBLAccountRepository accountRepository, IBLTransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _limitService = new LimitService();
            _connectionString = DateRepository.connectionString;
            dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Checking the id exists or not for category.
        /// </summary>
        /// <param name="objDTOTRN01">DTO for TRN01 Model.</param>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response PreValidation(DTOTRN01 objDTOTRN01)
        {
            if (objDTOTRN01.N01F05 < DateTime.MinValue || objDTOTRN01.N01F05 > DateTime.MaxValue)
            {
                return PreConditionFailedResponse("Date limit exceeded");
            }
            // Update account balance
            if (objDTOTRN01.N01F03 == 0) objDTOTRN01.N01F04 *= -1;
            return OkResponse();
        }

        /// <summary>
        /// Prepares the objects for create or update operation.
        /// </summary>
        /// <param name="objTRN01DTO">The DTO object representing the customer.</param>
        public void PreSave(DTOTRN01 objTRN01DTO)
        {
            objTRN01DTO.N01F02 = TokenManager.AccountSId;
            _objTRN01 = objTRN01DTO.Convert<TRN01>();
        }

        /// <summary>
        /// Validates customer information.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Validation()
        {
            if (!_accountRepository.IsUserExists(_objTRN01.N01F02))
            {
                return PreConditionFailedResponse("Id invalid for operation");
            }
            if (_objTRN01.N01F03 == enmTransactionType.D)
            {
                if (!_limitService.VerifyWithdrawal(_objTRN01.N01F02, -_objTRN01.N01F04))
                    return PreConditionFailedResponse("daily limit exceeded!!!");

                if (!_transactionRepository.VerifyTransaction(TokenManager.AccountSId, -_objTRN01.N01F04))
                    return PreConditionFailedResponse("Transaction Failed");
            }
            else if (_objTRN01.N01F03 == enmTransactionType.C && _objTRN01.N01F04 < 0)
            {
                throw new Exception("Invalid Amount !!!");
            }
            return OkResponse();
        }

        /// <summary>
        /// Saves changes according to the operation.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Save()
        {
            decimal updatedBalance = _transactionRepository.AddTransaction(TokenManager.AccountSId, _objTRN01);
            if (updatedBalance >= 0)
            {
                return OkResponse("Transaction added successfully..!!", "Updated balance " + updatedBalance);
            }
            else
                return PreConditionFailedResponse("Transaction was reverted..!!");

        }

        /// <summary>
        /// Retrieves transaction history for a specific account.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <returns>A response indicating success with the transaction history or failure if no data is found.</returns>
        public Response ViewTransactionHistory(int accountId)
        {
            // Retrieve transaction history for the specified account
            DataTable dt = _transactionRepository.ViewTransactionHistory(accountId);

            if (dt.Rows.Count > 0)
            {
                return OkResponse("Record fetched", dt);
            }
            else
            {
                return PreConditionFailedResponse("No data found");
            }
        }

        /// <summary>
        /// Retrieves all transactions.
        /// </summary>
        /// <returns>A response indicating success with the list of transactions or failure if no data is found.</returns>
        public Response GetAllTransactions()
        {
            List<TRN01> transactionList;
            using (var db = dbFactory.Open())
            {
                // Retrieve all transactions
                transactionList = db.Select<TRN01>();
            }
            if (transactionList.Count > 0)
            {
                return OkResponse("Record fetched", transactionList);
            }
            else
            {
                return PreConditionFailedResponse("No data found");
            }
        }

        #endregion
    }
}
        