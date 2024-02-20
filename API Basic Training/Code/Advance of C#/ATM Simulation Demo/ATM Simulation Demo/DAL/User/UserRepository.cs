using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.MySql;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Models;
using ServiceStack.Data;
using System.Data.Odbc;
using System.Web;
using ATM_Simulation_Demo.BAL.Security;

namespace ATM_Simulation_Demo.DAL.User
{
    public class UserRepository : IBLUserRepository
    {
        private readonly string _connectionString ;
        private readonly IDbConnectionFactory dbFactory;


        public UserRepository()
        {
            _connectionString = DAL_Helper.connectionString;
            dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (dbFactory == null)
            {
                throw new Exception("IDbConnectionFactory not found");
            }
        }

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

        public USR01 GetUser(int userId)
        {
            using (var db = dbFactory.Open())
            {
                // Retrieve user by userId
                return db.SingleById<USR01>(userId);
            }
        }

        public USR01 UpdateUser(USR01 updatedUser)
        {
            using (var db = dbFactory.Open())
            {
                // Update user in the database
                db.Update(updatedUser);
                return updatedUser;
            }
        }

        public USR01 GetUserByCredentials(string userName, string password)
        {
            using (var db = dbFactory.Open())
            {
                // Retrieve user by credentials
                USR01 user = db.Single<USR01>(x => x.R01F02 == userName && x.R01F06 == BLCrypto.Encrypt(password));
                user.R01F06 = BLCrypto.Decrypt(user.R01F06);
                return user;
            }
        }

        public List<USR01> GetAllUsers()
        {
            using (var db = dbFactory.Open())
            {
                // Retrieve all users
                List<USR01> userList =  db.Select<USR01>();
                
                // Decrypt passwords
                userList.ForEach(user => user.R01F06 = BLCrypto.Decrypt(user.R01F06));

                return userList;
            }
        }

        public bool VerifyUserCredentials(string userName, string password)
        {
            using (var db = dbFactory.Open())
            {
                // Verify user credentials
                return db.Count<USR01>(x => x.R01F02 == userName && x.R01F06 == BLCrypto.Encrypt(password)) > 0;
            }
        }

        public void DeleteUser(int userId)
        {
            using (var db = dbFactory.Open())
            {
                // Delete user by userId
                db.DeleteById<USR01>(userId);
            }
        }
    }
}
