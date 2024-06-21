using System.Collections.Generic;
using System.Linq;
using FinalDemo.BL.Interface.Service;
using FinalDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Microsoft.Extensions.Configuration;
using FinalDemo.Models;
using static FinalDemo.BL.BLHelper;
using FinalDemo.Models.DTO;
using FinalDemo.Extension;
using FinalDemo.Filters;

namespace FinalDemo.BL.Services
{
    /// <summary>
    /// Service class for managing User operations (BLUSR01).
    /// </summary>
    public class BLUSR01 : IBLUSR01
    {
        #region Private Fields

        private readonly IDbConnectionFactory _dbFactory;
        private USR01 _objUSR01;

        #endregion

        #region Public Fields

        /// <summary>
        /// Specifies the type of operation (Add or Update).
        /// </summary>
        public enmOperation Type { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the BLUSR01 class with configuration.
        /// </summary>
        /// <param name="configuration">The configuration interface.</param>
        public BLUSR01(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares the internal USR01 object for saving based on the provided user.
        /// </summary>
        /// <param name="_objDTOUSR01">The user to convert and save.</param>
        public void PreSave(DTOUSR01 _objDTOUSR01)
        {
            _objUSR01 = _objDTOUSR01.Convert<USR01>();
        }

        /// <summary>
        /// Validates the user before saving based on the operation type.
        /// </summary>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        public Response ValidationOnSave()
        {
            if (Type == enmOperation.A)
            {
                // Add validation logic for new user (Type A)
            }
            else if (Type == enmOperation.E)
            {
                if (!UserExists(_objUSR01.R01F01).Data)
                {
                    return PreConditionFailedResponse("User not found");
                }
            }
            return OkResponse();
        }

        /// <summary>
        /// Validates the user before saving based on the operation type.
        /// </summary>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        public Response ValidationOnDelete(int R01101)
        {
            if (!UserExists(R01101).Data)
            {
                return PreConditionFailedResponse("User not found");
            }
            return OkResponse();
        }

        /// <summary>
        /// Saves the user to the repository based on the operation type.
        /// </summary>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        public Response Save()
        {
            string message = string.Empty;
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (Type == enmOperation.A)
                {
                    db.Insert(_objUSR01);
                    message = "User added successfully..!!";
                }
                else if (Type == enmOperation.E)
                {
                    db.Update(_objUSR01);
                    message = "User updated successfully..!!";
                }
            }
            return OkResponse(message);
        }

        /// <summary>
        /// Checks if a user exists in the repository by its ID.
        /// </summary>
        /// <param name="R01F01">The ID of the user to check.</param>
        /// <returns>True if the user exists; otherwise, false.</returns>
        public Response UserExists(int R01F01)
        {
            if (R01F01 <= 0) return OkResponse("User not found", false);

            using (var db = _dbFactory.OpenDbConnection())
            {
                bool exists = db.Exists<USR01>(user => user.R01F01 == R01F01);
                return exists ? OkResponse("User found", true) : OkResponse("User not found", false);
            }
        }

        /// <summary>
        /// Retrieves a user from the repository by its ID.
        /// </summary>
        /// <param name="R01101">The ID of the user to retrieve.</param>
        /// <returns>The retrieved user or null if not found.</returns>
        public Response GetUserById(int R01101)
        {
            if (R01101 <= 0)
                return PreConditionFailedResponse("Invalid user id");
            
            
            using (var db = _dbFactory.OpenDbConnection())
            {
                _objUSR01 = db.SingleById<USR01>(R01101);
            }
            if (_objUSR01 != null)
            {
                return OkResponse("user Found", _objUSR01);
            }
            else
            {
                return OkResponse("user not Found");
            }
        }

        /// <summary>
        /// Retrieves all users from the repository.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public Response GetAllUsers()
        {
            List<USR01> lstUSR01 = new List<USR01>();
            using (var db = _dbFactory.OpenDbConnection())
            {
                lstUSR01 = db.Select<USR01>();
            }
            return OkResponse("All users", lstUSR01);
        }

        ///// <summary>
        ///// Retrieves all users from the repository.
        ///// </summary>
        ///// <returns>A list of all users.</returns>
        //public Response AuthorizeUser(string userName)
        //{
        //    List<USR01> lstUSR01 = new List<USR01>();
        //    using (var db = _dbFactory.OpenDbConnection())
        //    {
        //        lstUSR01 = db.Select<USR01>();
        //    }
        //    return OkResponse("All users", lstUSR01);
        //}

        /// <summary>
        /// Removes a user from the repository by its ID.
        /// </summary>
        /// <param name="R01F01">The ID of the user to remove.</param>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        public Response RemoveUser(int R01F01)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var rowsAffected = db.DeleteById<USR01>(R01F01);
                return rowsAffected > 0 ? OkResponse("User removed successfully..!!") : PreConditionFailedResponse("User not found or could not be deleted.");
            }
        }

        public Response AuthorizeUser(string R01F02, string R01F03)
        {
            R01F03 = BLCrypto.Encrypt(R01F03);
            using (var db = _dbFactory.OpenDbConnection())
            {
                _objUSR01 = db.Select<USR01>(R01 => (R01.R01F02 == R01F02 && R01.R01F03 == R01F03)).FirstOrDefault();
            }
            if(_objUSR01 != null)
            {
                return OkResponse("Authenticated", _objUSR01);
            }
            return PreConditionFailedResponse("User Not Found");
        }

        #endregion
    }
}
