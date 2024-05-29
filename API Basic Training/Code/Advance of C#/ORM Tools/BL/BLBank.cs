using ORM_Tools.Extension;
using ORM_Tools.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Web;

namespace ORM_Tools.BL
{
    /// <summary>
    /// Business logic class for managing BNK01 (Bank) data.
    /// </summary>
    public class BLBank
    {
        #region private properties
        /// <summary>
        /// Retrieves IDbConnectionFactory from the application context.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// The model object representing a bank entity.
        /// </summary>
        private BNK01 _objBNK01;
        #endregion

        #region public properties
        /// <summary>
        /// The response object used to communicate the result of operations.
        /// </summary>
        public Response objResponse;

        /// <summary>
        /// The enumoperation object used to communicate the result of operations.
        /// </summary>
        public EnmOperation Operation;
        #endregion

        #region constructor        
        /// <summary>
        /// Initializes a new instance of the BLBank class.
        /// </summary>
        public BLBank()
        {
            // Initialize the response object.
            objResponse = new Response();

            // Retrieve IDbConnectionFactory from the application context.
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            // Throw an exception if IDbConnectionFactory is not found.
            if (_dbFactory == null)
            {
                throw new Exception("IDbConnectionFactory not found");
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// Prepares the DTO object for saving by converting it to the corresponding model object.
        /// </summary>
        /// <param name="objDTOBNK01">The DTO object to be converted.</param>
        public void presave(DTOBNK01 objDTOBNK01)
        {
            // This method prepares the DTO object for saving by converting it to the corresponding model object.
            _objBNK01 = objDTOBNK01.Convert<BNK01>();
        }

        /// <summary>
        /// Validates the data before performing a specific Operation (Add, Edit).
        /// </summary>
        /// <param name="Operation">The Operation type (Add or Edit).</param>
        /// <returns>A response object indicating success or failure of the validation.</returns>
        public Response Validation()
        {
            // This method validates the data before performing a specific Operation (Add, Edit).
            Response objResponse = new Response();

            if (Operation == EnmOperation.A)
            {
                // Check if short name is unique for adding a new record.
                if (IsShortNameUnique(_objBNK01.K01F03))
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Bank Short Name must be unique.";
                    return objResponse;
                }
            }
            else if (Operation == EnmOperation.E)
            {
                // Check if ID exists for editing an existing record.
                if (!IsIdExists(_objBNK01.K01F01))
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Bank with the provided ID does not exist.";
                    return objResponse;
                }

                // Check if short name is unique excluding the current bank for editing.
                if (IsShortNameUnique(_objBNK01.K01F03, _objBNK01.K01F01))
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Bank Short Name must be unique.";
                    return objResponse;
                }
            }
            else
            {
                // Invalid Operation type.
                objResponse.IsError = true;
                objResponse.Message = "Invalid Operation type.";
                return objResponse;
            }

            return objResponse;
        }

        /// <summary>
        /// Validates the data before deleting a record.
        /// </summary>
        /// <param name="id">The ID of the record to be deleted.</param>
        /// <returns>A response object indicating success or failure of the validation.</returns>
        public Response ValidationOnDelete(int id)
        {
            // This method validates the data before deleting a record.
            Response objResponse = new Response();

            // Check if ID exists before deleting.
            if (!IsIdExists(id))
            {
                objResponse.IsError = true;
                objResponse.Message = "Bank with the provided ID does not exist.";
            }
            return objResponse;
        }

        /// <summary>
        /// Saves the bank entity based on the specified Operation type (Add, Edit, Delete).
        /// </summary>
        /// <param name="Operation">The type of Operation to perform (Add, Edit, Delete).</param>
        /// <returns>A response object indicating the outcome of the save Operation.</returns>
        public Response Save()
        {
            // Initialize the response message.
            string message = "";

            // Perform the Operation based on the specified type.
            switch (Operation)
            {
                case EnmOperation.A:
                    // Add Operation: Insert the bank entity into the database.
                    using (System.Data.IDbConnection db = _dbFactory.OpenDbConnection())
                    {
                        db.Insert(_objBNK01);
                    }
                    message = "data inserted successfully";
                    break;

                case EnmOperation.E:
                    // Edit Operation: Update the existing bank entity in the database.
                    using (System.Data.IDbConnection db = _dbFactory.OpenDbConnection())
                    {
                        db.Update(_objBNK01);
                    }
                    message = "data updated successfully";
                    break;

               
                default:
                    // Invalid Operation type.
                    return new Response { IsError = true, Message = "Invalid Operation type." };
            }
            // Update the response message and return the response object.
            objResponse.Message = message;
            return objResponse;
        }

        /// <summary>
        /// Retrieves all bank from the database.
        /// </summary>
        /// <returns>List of bank</returns>
        public Response GetAll()
        {
            using (System.Data.IDbConnection db = _dbFactory.OpenDbConnection())
            {
                List<BNK01> banks = db.Select<BNK01>();
                if (banks == null)
                {
                    objResponse.Message = "no banks found";
                }
                else
                {
                    objResponse.Data = banks;
                }
                return objResponse;
            }
        }

        public Response Delete()
        {
            // Delete Operation: Remove the bank entity from the database.
            using (System.Data.IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<BNK01>(_objBNK01.K01F01);
            }
            objResponse .Message = "data deleted successfully";
            return objResponse;
        }

        /// <summary>
        /// Retrieves an bank by ID from the database.
        /// </summary>
        /// <param name="id">Bank ID</param>
        /// <returns>Bank with the specified ID</returns>
        public Response Get(int id)
        {
            if (id >= 0)
            {
                using (System.Data.IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    BNK01 bank = db.SingleById<BNK01>(id);
                    if (bank == null)
                    {
                        objResponse.Message = "invalid id";
                    }
                    else
                    {
                        objResponse.Data = bank;
                    }
                }
            }
            else
            {
                objResponse.Message = "invalid id";
            }

            return objResponse;
        }
        #endregion

        #region Helper Methods
        // Method to check if the ID exists
        private bool IsIdExists(int id)
        {
            using (System.Data.IDbConnection db = _dbFactory.OpenDbConnection())
            {
                // Check if any record with the given ID exists in the database
                return db.Exists<BNK01>(e => e.K01F01 == id);
            }
        }

        // Method to check if the short name is unique
        private bool IsShortNameUnique(string shortName, int? currentId = 0)
        {
            using (System.Data.IDbConnection db = _dbFactory.OpenDbConnection())
            {
                // Check if any record with the given ID exists in the database
                return !db.Exists<BNK01>(e => e.K01F03 == shortName && e.K01F01 != currentId);

            }
        }
        #endregion

    }
}