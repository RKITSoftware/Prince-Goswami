using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalDemo.BL.Interface.Service
{

    public interface IBLUSR01 : IDataHandlerService<DTOUSR01>
    {
        /// <summary>
        /// Validates the data before delete.
        /// </summary>
        /// <returns>A response indicating whether the validation was successful.</returns>
        Response ValidationOnDelete(int R01F01);

          ///<summary>
        ///Removes a user from the inventory.
        ///</summary>
        ///<param name="userId">The ID of the user to be removed.</param>
        ///<returns>A boolean indicating whether the removal was successful or not.</returns>
        Response RemoveUser(int userId);

        ///<summary>
        ///Retrieves details of a specific user from the inventory.
        ///</summary>
        ///<param name="userId">The ID of the user to retrieve.</param>
        ///<returns>The user object corresponding to the provided ID.</returns>
        Response GetUserById(int userId);

        ///<summary>
        ///Retrieves all users from the inventory.
        ///</summary>
        ///<returns>A list of all users in the inventory.</returns>
        Response GetAllUsers();

        ///<summary>
        ///Searches for users in the inventory based on specified criteria.
        ///</summary>
        ///<param name="searchCriteria">The criteria for searching users.</param>
        ///<returns>A list of users matching the search criteria.</returns>
        //List<USR01> SearchUsers(string searchCriteria);

        ///<summary>
        ///Checks if a user exists in the inventory.
        ///</summary>
        ///<param name="userId">The ID of the user to check.</param>
        ///<returns>A boolean indicating whether the user exists or not.</returns>
        Response UserExists(int userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        Response AuthorizeUser(string userName, string passwordHash);
    }

}
