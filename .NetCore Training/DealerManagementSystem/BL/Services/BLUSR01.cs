using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.DAL;
using DealerManagementSystem.Filters;
using DealerManagementSystem.Models.POCO;

namespace DealerManagementSystem.BL.Services
{
    public class BLUSR01 : IBLUSR01
    {
        private readonly IUSR01_DAL _userRepository;

        public BLUSR01(IUSR01_DAL userRepository)
        {
            _userRepository = userRepository;
        }

        ///<inheritdoc/>
        public void AddUser(USR01 user)
        {
            user.R01F03 = BLCrypto.Encrypt(user.R01F03);
            _userRepository.Add(user);
        }

        ///<inheritdoc/>
        public void UpdateUser(USR01 user)
        {
            _userRepository.Update(user);
        }

        ///<inheritdoc/>>
        public void RemoveUser(int userId)
        {
            _userRepository.Delete(userId);
        }

        ///<inheritdoc/>
        public USR01 GetUserById(int userId)
        {
            return _userRepository.GetByID(userId);
        }

        ///<inheritdoc/>
        public List<USR01> GetAllUsers()
        {
            var user = _userRepository.GetAll();
            user.ForEach(user => user.R01F03 = BLCrypto.Decrypt(user.R01F03));
            return user;
        }

        /////<inheritdoc/>
        //public  List<USR01> SearchUsers(string searchCriteria)
        //{
        //    return  _userRepository.Search(searchCriteria);
        //}

        ///<inheritdoc/>
        public bool UserExists(int userId)
        {
            if (userId < 0) return false;
            if (_userRepository.GetByID(userId) != null) return true;
            return true;
        }

        public USR01 AuthorizeUser(string userName, string password)
        {
            List<USR01> users = _userRepository.GetAll();
            string passwordHash = BLCrypto.Encrypt(password);
            USR01 verifiedUser = users.FirstOrDefault(usr => usr.R01F02 == userName && usr.R01F03 == passwordHash);
            
            return verifiedUser;
        }
    }

}
