using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.Others;
using System;
using System.Runtime.Remoting;

namespace ATM_Simulation_Demo.BAL
{
    /// <summary>
    /// Service class for managing user-related operations in the business logic layer.
    /// </summary>
    public class UserService : IBLUserService
    {
        #region private field
        /// <summary>
        /// The user repository used for accessing user data.
        /// </summary>
        private readonly IBLUserRepository _userRepo;
        #endregion

        #region public field
        /// <summary>
        /// Response object used for returning operation results.
        /// </summary>
        public Response objResponse = new Response();
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepo">The user repository.</param>
        public UserService(IBLUserRepository userRepo)
        {
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }
        #endregion

        #region public methods
        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        public Response GetUserByID(int userId)
        {
            UserModel user = _userRepo.GetUser(userId);
            if (user == null)
            {
                objResponse.IsError = true;
                objResponse.Message = "Id does not exists";
            }
            objResponse.Data = user;
            return objResponse;

        }

        /// <summary>
        /// Retrieves a user by their credentials (username and password).
        /// </summary>
        public UserModel GetUserByCredentials(string userName, string password)
        {
            return _userRepo.GetUserByCredentials(userName, password);
        }

        /// <summary>
        /// Creates a new user with the provided information.
        /// </summary>
        public Response CreateUser(string userName, string mobileNumber, string password, DateTime DOB, UserRole role)
        {
            // Create a new user and add to the database
            UserModel newUser = new UserModel
            {
                UserName = userName,
                MobileNumber = mobileNumber,
                Password = password,
                DateOfBirth = DOB,
                Role = role
            };
            objResponse.Data = _userRepo.CreateUser(newUser);
            return objResponse;
        }


        /// <summary>
        /// Changes the password of a user.
        /// </summary>
        public Response ChangePassword(int userId, string currentPassword, string newPassword)
        {
            UserModel user = _userRepo.GetUser(userId);
            if (user == null)
            {
                objResponse.IsError = true;
                objResponse.Message = "USer does not exists.";
            }
            else
            {
                if (user.Password == currentPassword) // Actual implementation may involve hashing and checking
                {
                    _userRepo.SingleFieldUpdate(userId,"Password",newPassword);
                    objResponse.Message = "Password changed succesfully";
                    return objResponse;
                }
                else
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Invalid password. Unable to change password.";
                }
            }
            return objResponse;
        }


        /// <summary>
        /// Updates the role of a user.
        /// </summary>
        public Response UpdateRole(int userId, UserRole newRole)
        {
            UserModel user = _userRepo.GetUser(userId);
            if (user == null)
            {
                objResponse.IsError = true;
                objResponse.Message = "User does not exists";
                return objResponse;
            }
            user.Role = newRole;
            //_userRepo.UpdateUser(user);
            _userRepo.SingleFieldUpdate(userId,"Role", newRole);
            objResponse.Message = "USer role updated succesfully";
            return objResponse;
        }


        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        public Response DeleteUser(int id)
        {
            if (_userRepo.DeleteUser(id))
            {
                objResponse.IsError = false;
                objResponse.Message = "Deleted user successfully";
            }
            else
            {
                objResponse.IsError = true;
                objResponse.Message = "user not found";
            }
            return objResponse;

        }


        /// <summary>
        /// Retrieves all users.
        /// </summary>
        public Response GetAllUsers()
        {
            objResponse.Data = _userRepo.GetAllUsers();
            return objResponse;
        }
        #endregion
    }
}
