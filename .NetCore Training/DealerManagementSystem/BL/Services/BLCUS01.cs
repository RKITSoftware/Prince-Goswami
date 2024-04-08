using System.Collections.Generic;
using DealerManagementSystem.DAL;
using DealerManagementSystem.Models;
using ServiceStack.Logging;

namespace DealerManagementSystem.BL.Services
{
    public class BLCUS01 : IBLCUS01
    {
        private readonly ICUS01_DAL _customerRepository;

        public BLCUS01(CUS01Repository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void AddCustomer(CUS01 customer)
        {
            _customerRepository.Add(customer);
        }

        public void UpdateCustomer(CUS01 customer)
        {
            _customerRepository.Update(customer);
        }

        public void RemoveCustomer(int customerId)
        {
            _customerRepository.Delete(customerId);
        }

        public CUS01 GetCustomerById(int customerId)
        {
            return _customerRepository.GetByID(customerId);
        }

        public List<CUS01> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        public bool CustomerExists(int customerId)
        {
            if(_customerRepository.GetByID(customerId) != null) return true;
            return false;
        }
    }
}
