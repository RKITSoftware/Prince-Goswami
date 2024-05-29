using System.Collections.Generic;
using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.DAL;
using DealerManagementSystem.Models.POCO;

namespace DealerManagementSystem.BL.Services
{
    public class BLSAL01 : IBLSAL01
    {
        private readonly ISAL01_DAL _saleDealRepository;

        public BLSAL01(ISAL01_DAL saleDealRepository)
        {
            _saleDealRepository = saleDealRepository;
        }

        public void AddSaleDeal(SAL01 deal)
        {
            _saleDealRepository.Add(deal);
        }

        public void UpdateSaleDeal(SAL01 deal)
        {
            _saleDealRepository.Update(deal);
        }

        public void RemoveSaleDeal(int dealId)
        {
            _saleDealRepository.Delete(dealId);
        }

        public SAL01 GetSaleDealById(int dealId)
        {
            return _saleDealRepository.GetByID(dealId);
        }

        public List<SAL01> GetAllSaleDeals()
        {
            return _saleDealRepository.GetAll();
        }

        public bool SaleDealExists(int dealId)
        {
            if (_saleDealRepository.GetByID(dealId) != null) return true;
            return false;
        }
    }
}
