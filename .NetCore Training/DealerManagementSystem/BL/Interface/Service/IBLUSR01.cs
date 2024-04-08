using DealerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DealerManagementSystem.BL.Interface.Service
{

    public interface IBLUSR01
    {
        ///<summary>
        ///Adds a new user to the inventory.
        ///</summary>
        ///<param name="user">The user object to be added.</param>
        void AddUser(USR01 user);

        ///<summary>
        ///Updates an existing user in the inventory.
        ///</summary>
        ///<param name="user">The updated user object.</param>
        ///<returns>A boolean indicating whether the update was successful or not.</returns>
        void UpdateUser(USR01 user);

        ///<summary>
        ///Removes a user from the inventory.
        ///</summary>
        ///<param name="userId">The ID of the user to be removed.</param>
        ///<returns>A boolean indicating whether the removal was successful or not.</returns>
        void RemoveUser(int userId);

        ///<summary>
        ///Retrieves details of a specific user from the inventory.
        ///</summary>
        ///<param name="userId">The ID of the user to retrieve.</param>
        ///<returns>The user object corresponding to the provided ID.</returns>
        USR01 GetUserById(int userId);

        ///<summary>
        ///Retrieves all users from the inventory.
        ///</summary>
        ///<returns>A list of all users in the inventory.</returns>
        List<USR01> GetAllUsers();

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
        bool UserExists(int userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        USR01 AuthorizeUser(string userName,string passwordHash);
    }

}
