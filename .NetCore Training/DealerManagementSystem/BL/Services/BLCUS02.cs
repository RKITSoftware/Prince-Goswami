using System.Collections.Generic;
using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.DAL;
using DealerManagementSystem.Models.POCO;

namespace DealerManagementSystem.BL.Services
{
    public class BLCUS02 : IBLCUS02
    {
        private readonly ICUS02_DAL _customerTransactionRepository;

        public BLCUS02(ICUS02_DAL customerTransactionRepository)
        {
            _customerTransactionRepository = customerTransactionRepository;
        }

        public void AddCustomerTransaction(CUS02 transaction)
        {
            _customerTransactionRepository.Add(transaction);
        }

        public void UpdateCustomerTransaction(CUS02 transaction)
        {
            _customerTransactionRepository.Update(transaction);
        }

        public void RemoveCustomerTransaction(int transactionId)
        {
            _customerTransactionRepository.Delete(transactionId);
        }

        public CUS02 GetCustomerTransactionById(int transactionId)
        {
            return _customerTransactionRepository.GetByID(transactionId);
        }

        public List<CUS02> GetAllCustomerTransactions()
        {
            return _customerTransactionRepository.GetAll();
        }

        public bool CustomerTransactionExists(int transactionId)
        {
            return _customerTransactionRepository.GetByID(transactionId)!=null;
        }
    }
}
