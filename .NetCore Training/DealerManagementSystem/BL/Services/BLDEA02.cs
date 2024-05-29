using System.Collections.Generic;
using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.DAL;
using DealerManagementSystem.Models.POCO;

namespace DealerManagementSystem.BL.Services
{
    public class BLDEA02 : IBLDEA02
    {
        private readonly IDEA02_DAL _dealerTransactionRepository;

        public BLDEA02(IDEA02_DAL dealerTransactionRepository)
        {
            _dealerTransactionRepository = dealerTransactionRepository;
        }

        public void AddDealerTransaction(DEA02 transaction)
        {
            _dealerTransactionRepository.Add(transaction);
        }

        public void UpdateDealerTransaction(DEA02 transaction)
        {
            _dealerTransactionRepository.Update(transaction);
        }

        public void RemoveDealerTransaction(int transactionId)
        {
            _dealerTransactionRepository.Delete(transactionId);
        }

        public DEA02 GetDealerTransactionById(int transactionId)
        {
            return _dealerTransactionRepository.GetByID(transactionId);
        }

        public List<DEA02> GetAllDealerTransactions()
        {
            return _dealerTransactionRepository.GetAll();
        }

        public bool DealerTransactionExists(int transactionId)
        {
            if(_dealerTransactionRepository.GetByID(transactionId) != null) return true;
            return false;
        }
    }
}
