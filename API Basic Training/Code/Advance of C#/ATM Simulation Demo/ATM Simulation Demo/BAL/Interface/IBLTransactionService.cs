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
        public interface IBLTransactionService : IDataHandlerService<DTOTRN01>
        {

        Response PreValidation(DTOTRN01 objDTOTRN01);

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



