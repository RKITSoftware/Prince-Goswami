using System.Collections.Generic;
using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.DAL;
using DealerManagementSystem.Models.POCO;

namespace DealerManagementSystem.BL.Services
{
    public class BLCTG01 : IBLCTG01
    {
        private readonly ICTG01_DAL _categoryRepository;

         public BLCTG01(ICTG01_DAL categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        ///<inheritdoc/>
        public void AddCategory(CTG01 category)
        {
            _categoryRepository.Add(category);
        }

        ///<inheritdoc/>
        public void UpdateCategory(CTG01 category)
        {
            _categoryRepository.Update(category);
        }

        ///<inheritdoc/>
        public void RemoveCategory(int categoryId)
        {
            _categoryRepository.Delete(categoryId);
        }

        ///<inheritdoc/>
        public CTG01 GetCategoryById(int categoryId)
        {
            return _categoryRepository.GetByID(categoryId);
        }

        ///<inheritdoc/>
        public List<CTG01> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        ///<inheritdoc/>
        public bool CategoryExists(int categoryId)
        {
            if (_categoryRepository.GetByID(categoryId) != null) return true; 
            return false; 
        }
    }
}
