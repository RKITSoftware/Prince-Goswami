using System.Collections.Generic;
using FinalDemo.BL.Interface.Service;
using FinalDemo.Filters;
using static FinalDemo.BL.BLHelper;
using FinalDemo.Models;
using FinalDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Microsoft.Extensions.Configuration;
using FinalDemo.Models.DTO;
using FinalDemo.Extension;

namespace FinalDemo.BL.Services
{
    /// <summary>
    /// Service class for managing Vehicle operations (BLVEH01).
    /// </summary>
    public class BLVEH01 : IBLVEH01
    {
        #region Private Fields

        private VEH01 _objVEH01;
        private readonly IDbConnectionFactory _dbFactory;

        #endregion

        #region Public Fields

        /// <summary>
        /// Specifies the type of operation (Add or Update).
        /// </summary>
        public enmOperation Type { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the BLVEH01 class with configuration.
        /// </summary>
        /// <param name="configuration">The configuration interface.</param>
        public BLVEH01(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares the internal VEH01 object for saving based on the provided vehicle.
        /// </summary>
        /// <param name="objDTOVEH01">The vehicle to convert and save.</param>
        public void PreSave(DTOVEH01 objDTOVEH01)
        {
            _objVEH01 = objDTOVEH01.Convert<VEH01>();
        }

        /// <summary>
        /// Validates the vehicle before saving based on the operation type.
        /// </summary>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        public Response ValidationOnSave()
        {
           if (Type == enmOperation.E)
            {
                if (!VehicleExists(_objVEH01.H01F01).Data)
                {
                    return PreConditionFailedResponse("Vehicle not found");
                }
            }
            return OkResponse();
        }

        /// <summary>
        /// Saves the vehicle to the repository based on the operation type.
        /// </summary>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        public Response Save()
        {
            string message = "";
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (Type == enmOperation.A)
                {
                    db.Insert(_objVEH01);
                    message = "Vehicle added successfully..!!";
                }
                else if (Type == enmOperation.E)
                {
                    db.Update(_objVEH01);
                    message = "Vehicle updated successfully..!!";
                }
            }
            return OkResponse(message);
        }

        /// <summary>
        /// Checks if a vehicle exists in the repository by its ID.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle to check.</param>
        /// <returns>True if the vehicle exists; otherwise, false.</returns>
        public Response VehicleExists(int vehicleId)
        {
            if (vehicleId <= 0) return OkResponse("Vehicle not found", false);
            bool exists;
            using (var db = _dbFactory.OpenDbConnection())
            {
                exists = db.SingleById<VEH01>(vehicleId) != null;
                return exists ? OkResponse("Vehicle found", true) : OkResponse("Vehicle not found", false);
            }
        }

        /// <summary>
        /// Retrieves a vehicle from the repository by its ID.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle to retrieve.</param>
        /// <returns>The retrieved vehicle or null if not found.</returns>
        public Response GetVehicleById(int vehicleId)
        {
            if (vehicleId <= 0) return null;

            using (var db = _dbFactory.OpenDbConnection())
            {
                _objVEH01 = db.SingleById<VEH01>(vehicleId);
            }

            return _objVEH01 == null ? PreConditionFailedResponse("vehicle not found") : OkResponse("vehicle found", _objVEH01);
        }

        /// <summary>
        /// Retrieves all vehicles from the repository.
        /// </summary>
        /// <returns>A list of all vehicles.</returns>
        public Response GetAllVehicles()
        {
            List<VEH01> lstVEH01 = new List<VEH01>();
            using (var db = _dbFactory.OpenDbConnection())
            {
                lstVEH01 = db.Select<VEH01>();
            }
            return OkResponse("All vehicles", lstVEH01);
        }

        /// <summary>
        /// Removes a vehicle from the repository by its ID.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle to remove.</param>
        /// <returns>A Response object indicating success or failure with a message.</returns>
        public Response RemoveVehicle(int vehicleId)
        {
            var rowsAffected = 0;
            using (var db = _dbFactory.OpenDbConnection())
            {
                rowsAffected = db.DeleteById<VEH01>(vehicleId);
            }
                return rowsAffected > 0 ? OkResponse("Vehicle removed successfully..!!") : PreConditionFailedResponse("Vehicle not found or could not be deleted.");
        }

        #endregion
    }
}
