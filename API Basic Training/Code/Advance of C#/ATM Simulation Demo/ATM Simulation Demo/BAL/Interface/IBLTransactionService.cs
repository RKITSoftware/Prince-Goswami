using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.DTO;
using ATM_Simulation_Demo.Models.POCO;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections.Generic;
using System.Windows.Media.Animation;

namespace ATM_Simulation_Demo.BAL.Interface
{
        /// <summary>
        /// Interface for transaction-related operations.
        /// </summary>
        public interface IBLTransactionService
        {
           Response PreValidation(DTO_TRN01 objDTOTRN01);

        EnmOperation Operation { get; set; }

        Response Validation();
        void PreSave(DTO_TRN01 objTRN01DTO);
        Response Save();
        /// <summary>
        /// View transaction history for a account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>List of transactions in the account's history.</returns>
       Response ViewTransactionHistory(int accountId);
        
        /// <summary>
        /// View transactions.
        /// </summary>
        /// <returns>List of transactions.</returns>
         Response GetAllTransactions();
    }
}



