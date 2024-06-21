using System;
using System.Collections.Generic;
using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Others.Security;
using ATM_Simulation_Demo.Models;
using ATM_Simulation_Demo.Models.POCO;
using static ATM_Simulation_Demo.BAL.BLHelper;
using ATM_Simulation_Demo.Models.DTO;
using ATM_Simulation_Demo.Others.Auth;
using ATM_Simulation_Demo.Extension;
using ATM_Simulation_Demo.DAL;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Web;
using System.Net.PeerToPeer;
using Google.Protobuf.Compiler;

namespace ATM_Simulation_Demo.BAL.Services
{
    /// <summary>
    /// Service class for managing user-related operations in the business logic layer.
    /// </summary>
    public class UserService : IBLUserService
    {
        #region Private Fields

        private readonly IBLUserRepository _userRepository;
        private USR01 _objUSR01;
        private readonly string _connectionString;
        private readonly IDbConnectionFactory dbFactory;

        #endregion

        #region Public Properties

        /// <summary>
        /// Specifies the operation to perform.
        /// </summary>
        public enmOperation Type { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepo">The user repository.</param>
        public UserService(IBLUserRepository userRepo)
        {
            _connectionString = DateRepository.connectionString;
            dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            _userRepository = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Checking if the ID exists or not for a user.
        /// </summary>
        /// <param name="objDTOUSR01">DTO for USR01 Model.</param>
        /// <returns>Success response if no error occurs else response with a specific status code with a message.</returns>
        public Response PreValidation(DTOUSR01 objDTOUSR01)
        {
            // Check if the ID is valid for the operation
            if (!IsIDValid(Type, objDTOUSR01.R01F01))
            {
                return PreConditionFailedResponse("ID is invalid for the operation");
            }

            if (Type == enmOperation.E)
            {
                // Check if the user exists
                if (_userRepository.IsUserExists(objDTOUSR01.R01F01))
                {
                    return NotFoundResponse("User not found.");
                }
            }
            return OkResponse();
        }

        /// <summary>
        /// Prepares the objects for create or update operation.
        /// </summary>
        /// <param name="objUSR01DTO">The DTO object representing the user.</param>
        public void PreSave(DTOUSR01 objUSR01DTO)
        {
            //objUSR01DTO.R01F01 = TokenManager.AccountSId;
            objUSR01DTO.R01F06 = BLCrypto.Encrypt(objUSR01DTO.R01F06);

            _objUSR01 = objUSR01DTO.Convert<USR01>();
        }

        /// <summary>
        /// Validates user information.
        /// </summary>
        /// <returns>Success response if no error occurs else response with a specific status code with a message.</returns>
        public Response Validation()
        {
            if (!_userRepository.IsUserExists(_objUSR01.R01F01))
            {
                return PreConditionFailedResponse("ID invalid for operation");
            }
            return OkResponse();
        }

        /// <summary>
        /// Saves changes according to the operation.
        /// </summary>
        /// <returns>Success response if no error occurs else response with a specific status code with a message.</returns>
        public Response Save()
        {
            if (Type == enmOperation.A)
            {
                using (var db = dbFactory.Open())
                {
                    // Insert user into the database
                    _objUSR01.R01F01 = (int)db.Insert(_objUSR01);
                }
                return OkResponse("Account added successfully..!!", _objUSR01);
            }

            using (var db = dbFactory.Open())
            {
                // Insert user into the database
                db.Update(_objUSR01);
            }
            return OkResponse("Account updated successfully..!!", _objUSR01);
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user information if found, otherwise null.</returns>
        public Response GetUserByID(int userId)
        {
            using (var db = dbFactory.Open())
            {
                // Retrieve user by userId
                _objUSR01 = db.SingleById<USR01>(userId);
                _objUSR01.R01F06 = BLCrypto.Decrypt(_objUSR01.R01F06);
                return OkResponse("Data fetched succesfully", _objUSR01);
            }
        }

        /// <summary>
        /// Retrieves a user by their credentials (username and password).
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The user information if found, otherwise null.</returns>
        public USR01 GetUserByCredentials(string userName, string password)
        {
            password = BLCrypto.Encrypt(password);
            using (var db = dbFactory.Open())
            {
                // Retrieve user by credentials
                _objUSR01 = db.Single<USR01>(x => x.R01F02 == userName && x.R01F06 == password);
            }
            if (_objUSR01 == null)
            {
                throw new Exception("User not Exist");
            }
            _objUSR01.R01F06 = BLCrypto.Decrypt(_objUSR01.R01F06);
            return _objUSR01;
        }

     
        /// <summary>
        /// Changes the password for a user.
        /// </summary>
        /// <param name="userId">The ID of the user whose password to change.</param>
        /// <param name="currentPassword">The current password of the user.</param>
        /// <param name="newPassword">The new password to set.</param>
        /// <returns>A response indicating the outcome of the password change operation.</returns>
        public Response ChangePassword(int userId, string currentPassword, string newPassword)
        {
            USR01 objUSR01 = _userRepository.GetUser(userId);
            if (objUSR01 == null)
            {
                return PreConditionFailedResponse("User does not exist");
            }
            if (BLCrypto.Decrypt(objUSR01.R01F06) == currentPassword) // Actual implementation may involve hashing and checking
            {
                objUSR01.R01F06 = BLCrypto.Encrypt(newPassword); // Update password (you may want to hash it)
                if (UpdateSpecificField(objUSR01, "R01F06", newPassword))
                    return OkResponse();
                else
                    return PreConditionFailedResponse("Something went wrong while updating field");
            }
            else
            {
                throw new InvalidOperationException("Invalid password. Unable to change password.");
            }
        }

        /// <summary>
        /// Updates the role of a user.
        /// </summary>
        /// <param name="userId">The ID of the user whose role to update.</param>
        /// <param name="newRole">The new role to assign to the user.</param>
        public Response UpdateRole(int userId, enmUserRole newRole)
        {
            USR01 objUSR01 = _userRepository.GetUser(userId);
            if (objUSR01 == null)
            {
                return PreConditionFailedResponse("User does not exist");
            }
            if (UpdateSpecificField(objUSR01, "R01F05", newRole))
                return OkResponse("role upadted");

            return PreConditionFailedResponse("There was some error while updating role");                
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        public Response DeleteUser(int userId)
        {
            if (userId > 0)
            {
                using (var db = dbFactory.Open())
                {
                    // Delete user by userId
                    int rowsAffected = db.DeleteById<USR01>(userId);
                    if (rowsAffected > 0)
                    {
                        return OkResponse("user deleted succesfully");
                    }
                    else 
                        return PreConditionFailedResponse("there was some error while deleting user");
                }
            }
            return PreConditionFailedResponse("Invalid ID");

        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public Response GetAllUsers()
        {
            List<USR01> userList;
            using (var db = dbFactory.Open())
            {
                // Retrieve all users
                userList = db.Select<USR01>();
            }
            if (userList.Count > 0)
            {
                // Decrypt passwords
                userList.ForEach(user => user.R01F06 = BLCrypto.Decrypt(user.R01F06));

                return OkResponse("Record fetched", userList);
            }
            else
                return PreConditionFailedResponse("No data found");
        }

        #endregion

        #region private methods
        private bool UpdateSpecificField<T>(USR01 objUSR01, string fieldName, T val)
        {
            using (var db = dbFactory.Open())
            {
                if (objUSR01 != null)
                {
                    var property = typeof(USR01).GetProperty(fieldName);
                    if (property != null)
                    {
                        property.SetValue(objUSR01, val);
                        db.Update(objUSR01);
                        return true;
                    }
                    else
                    {
                        // Property not found
                        return false;
                    }
                }
                else
                {
                    // User not found
                    return false;
                }
            }
        }
        #endregion

    }
}
