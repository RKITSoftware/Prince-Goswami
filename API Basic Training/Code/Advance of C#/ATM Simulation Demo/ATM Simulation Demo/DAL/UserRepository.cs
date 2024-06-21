using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Models.POCO;
using ATM_Simulation_Demo.Others.Security;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Web;

namespace ATM_Simulation_Demo.DAL
{
    /// <summary>
    /// Repository class for managing user-related database operations.
    /// </summary>
    public class UserRepository : IBLUserRepository
    {
        #region Private Fields
        /// <summary>
        /// Represents the connection string used to connect to the database.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Represents connection setting for orm.
        /// </summary>
        private readonly IDbConnectionFactory dbFactory;


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        public UserRepository()
        {
            _connectionString = DateRepository.connectionString;
            dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (dbFactory == null)
            {
                throw new Exception("IDbConnectionFactory not found");
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="newUser">The user object to create.</param>
        /// <returns>The newly created user object.</returns>
        public USR01 CreateUser(USR01 newUser)
        {
            using (var db = dbFactory.Open())
            {
                // Insert user into the database
                db.Insert(newUser);

                // Retrieve the inserted user
                return newUser;
            }
        }

        /// <summary>
        /// Retrieves a user by their ID from the database.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user object if found, otherwise null.</returns>
        public USR01 GetUser(int userId)
        {
            using (var db = dbFactory.Open())
            {
                // Retrieve user by userId
                return db.SingleById<USR01>(userId);
            }
        }

        /// <summary>
        /// Updates a user in the database.
        /// </summary>
        /// <param name="updatedUser">The updated user object.</param>
        /// <returns>The updated user object.</returns>
        public USR01 UpdateUser(USR01 updatedUser)
        {
            using (var db = dbFactory.Open())
            {
                // Update user in the database
                db.Update(updatedUser);
                return updatedUser;
            }
        }

        /// <summary>
        /// Retrieves a user by their credentials from the database.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The user object if found, otherwise null.</returns>
        public USR01 GetUserByCredentials(string userName, string password)
        {
            using (var db = dbFactory.Open())
            {
                // Retrieve user by credentials
                password = BLCrypto.Encrypt(password);
                USR01 user = db.Single<USR01>(x => x.R01F02 == userName && x.R01F06 == password);
                if (user == null)
                {
                    throw new Exception("User not Exist");
                }
                user.R01F06 = BLCrypto.Decrypt(user.R01F06);
                return user;
            }
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A list of all user objects.</returns>
        public List<USR01> GetAllUsers()
        {
            using (var db = dbFactory.Open())
            {
                // Retrieve all users
                List<USR01> userList = db.Select<USR01>();

                // Decrypt passwords
                userList.ForEach(user => user.R01F06 = BLCrypto.Decrypt(user.R01F06));

                return userList;
            }
        }

        /// <summary>
        /// Verifies user credentials in the database.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>True if the credentials are valid, otherwise false.</returns>
        public bool VerifyUserCredentials(string userName, string password)
        {
            using (var db = dbFactory.Open())
            {
                // Verify user credentials
                return db.Count<USR01>(x => x.R01F02 == userName && x.R01F06 == BLCrypto.Encrypt(password)) > 0;
            }
        }

        /// <summary>
        /// Deletes a user from the database by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        public void DeleteUser(int userId)
        {
            using (var db = dbFactory.Open())
            {
                // Delete user by userId
                db.DeleteById<USR01>(userId);
            }
        }

        /// <summary>
        /// Checks if a user exists in the database by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to check.</param>
        /// <returns>True if the user exists, otherwise false.</returns>
        public bool IsUserExists(int userId)
        {
            using (var db = dbFactory.Open())
            {
                return db.Exists<USR01>(userId);
            }
        }

        #endregion   
    }
}
