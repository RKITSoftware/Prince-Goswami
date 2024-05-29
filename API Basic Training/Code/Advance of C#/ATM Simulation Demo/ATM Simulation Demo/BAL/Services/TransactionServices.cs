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
using ATM_Simulation_Demo.Others.Auth.Account;
using ServiceStack;
using System.Transactions;

namespace ATM_Simulation_Demo.BAL.Services
{
    public class TransactionService : IBLTransactionService
    {
        #region Private Properties
        private readonly IBLAccountRepository _accountRepository;
        private readonly IBLTransactionRepository _transactionRepository;
        private readonly IBLLimitService _limitService;
        private TRN01 _objTRN01;
        private ACC01 _objACC01;
        #endregion

        #region Public Properties

        /// <summary>
        /// Specifies the operation to perform.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion Public Properties



        public TransactionService(IBLAccountRepository accountRepository, IBLTransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _limitService = new LimitService();
        }


        /// <summary>
        /// Checking the id exists or not for category.
        /// </summary>
        /// <param name="objDTOTRN01">DTO for TRN01 Model.</param>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response PreValidation(DTO_TRN01 objDTOTRN01)
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
        public void PreSave(DTO_TRN01 objTRN01DTO)
        {
            objTRN01DTO.N01F02 = TokenManager.sessionId;
            _objTRN01 = objTRN01DTO.Convert<TRN01>();
            _objACC01 = _accountRepository.GetAccountByID(_objTRN01.N01F02);
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
            if (_objTRN01.N01F03 == TransactionType.D)
            {
                if (!_limitService.VerifyWithdrawal(_objTRN01.N01F02, -_objTRN01.N01F04))
                    return PreConditionFailedResponse("daily limit exceeded!!!");
                if (!_transactionRepository.VerifyTransaction(TokenManager.sessionId, -_objTRN01.N01F04))
                    return PreConditionFailedResponse("Transaction Failed");
            }
            else if (_objTRN01.N01F03 == TransactionType.C && _objTRN01.N01F04 < 0)
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
            decimal updatedBalance = _transactionRepository.AddTransaction(TokenManager.sessionId, _objTRN01);
            if (updatedBalance >= 0)
            {
                return OkResponse("Transaction added successfully..!!", "Updated balance " + updatedBalance);
            }
            else
                return PreConditionFailedResponse("Transaction was reverted..!!");

        }



        /// <inheritdoc />
        public Response ViewTransactionHistory(int accountId)
        {
            try
            {
                List<TRN01> lstTRN01 = _transactionRepository.ViewTransactionHistory(accountId);
                return OkResponse("Data fetched succesfully", lstTRN01);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        /// <inheritdoc />
        public Response GetAllTransactions()
        {
            try
            {
                List<TRN01> listTRN01 = _transactionRepository.GetAllTransactions() ?? new List<TRN01>();

                return OkResponse("Data fetcehd succesfully", listTRN01);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

    }
}
