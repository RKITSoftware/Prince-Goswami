using System.Collections.Generic;
using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.DAL;
using DealerManagementSystem.Models;
using ServiceStack.DataAnnotations;

namespace DealerManagementSystem.BL.Services
{
    public class BLDEA01 : IBLDEA01
    {
        private readonly IDEA01_DAL _dealerRepository;

        public BLDEA01(DEA01Repository dealerRepository)
        {
            _dealerRepository = dealerRepository;
        }

        public void AddDealer(DEA01 dealer)
        {
            _dealerRepository.Add(dealer);
        }

        public void UpdateDealer(DEA01 dealer)
        {
            _dealerRepository.Update(dealer);
        }

        public void RemoveDealer(int dealerId)
        {
            _dealerRepository.Delete(dealerId);
        }

        public DEA01 GetDealerById(int dealerId)
        {
            return _dealerRepository.GetByID(dealerId);
        }

        public List<DEA01> GetAllDealers()
        {
            return _dealerRepository.GetAll();
        }

        public bool DealerExists(int dealerId)
        {
            return _dealerRepository.GetByID(dealerId) != null;
        }
    }
}
