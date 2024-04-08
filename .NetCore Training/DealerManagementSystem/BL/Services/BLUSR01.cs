    using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.DAL;
using DealerManagementSystem.Filters;
using DealerManagementSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

namespace DealerManagementSystem.BL.Services
{
    public class BLUSR01 : IBLUSR01
    {
        private readonly IUSR01_DAL _userRepository;

        public BLUSR01(USR01Repository userRepository)
        {
            _userRepository = userRepository;
        }

        ///<inheritdoc/>
        public void AddUser(USR01 user)
        {
            _userRepository.Add(user);
        }

        ///<inheritdoc/>
        public void UpdateUser(USR01 user)
        {
            _userRepository.Update(user);
        }

        ///<inheritdoc/>>
        public  void RemoveUser(int userId)
        {
            _userRepository.Delete(userId);
        }

        ///<inheritdoc/>
        public  USR01 GetUserById(int userId)
        {
            return _userRepository.GetByID(userId);
        }

        ///<inheritdoc/>
        public List<USR01> GetAllUsers()
        {
             return _userRepository.GetAll();
        }

        /////<inheritdoc/>
        //public  List<USR01> SearchUsers(string searchCriteria)
        //{
        //    return  _userRepository.Search(searchCriteria);
        //}

        ///<inheritdoc/>
        public  bool UserExists(int userId)
        {
            if(userId < 0) return false;
            if(_userRepository.GetByID(userId) != null) return true;
            return true;
        }

        public USR01 AuthorizeUser(string userName, string password)
        {
            List<USR01> users = _userRepository.GetAll();
            USR01 verifiedUser = null;
            users.ForEach(user =>
            {
                if(user.R01F02 == userName && BLCrypto.Decrypt(user.R01F05 ) == password)
                {
                    verifiedUser = user;
                }
            });
            return verifiedUser;
        }
    }

}
