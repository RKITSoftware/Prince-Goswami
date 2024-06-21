using System.Collections.Generic;
using FinalDemo.BL.Interface.Service;
using static FinalDemo.BL.BLHelper;
using FinalDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Microsoft.Extensions.Configuration;
using FinalDemo.Models.DTO;
using FinalDemo.Extension;
using FinalDemo.Models;
using Microsoft.AspNetCore.Components.Web;

namespace FinalDemo.BL.Services
{
    /// <summary>
    /// Service class for managing Sale Deal operations (BLSAL01).
    /// </summary>
    public class BLSAL01 : IBLSAL01
    {
        #region Private Fields

        /// <summary>
        /// Database connection factory for ORM Lite.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Holds the instance of SAL01 being operated on.
        /// </summary>
        private SAL01 _objSAL01;

        public enmOperation Type { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the BLSAL01 class with configuration.
        /// </summary>
        /// <param name="configuration">The configuration interface.</param>
        public BLSAL01(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares the internal SAL01 object for saving based on the provided POCO.
        /// </summary>
        /// <param name="objDTOSAL01">The POCO containing data to be saved.</param>
        public void PreSave(DTOSAL01 objDTOSAL01)
        {
            _objSAL01 = objDTOSAL01.Convert<SAL01>();
        }

        /// <summary>
        /// Validates the SAL01 object before saving based on the operation type.
        /// </summary>
        /// <returns>A boolean indicating success or failure.</returns>
        public Response ValidationOnSave()
        {
            if (Type == enmOperation.A)
            {
                if (_objSAL01.L01F02 == 0 || _objSAL01.L01F03 == 0)
                {
                    return PreConditionFailedResponse("There was a problem while performing operation");
                }
            }
            if (Type == enmOperation.E)
            {
                if (_objSAL01.L01F01 == 0 || _objSAL01.L01F02 == 0 || _objSAL01.L01F03 == 0)
                {
                    return PreConditionFailedResponse("There was a problem while performing operation");
                }
            }
            return OkResponse();
        }

        /// <summary>
        /// Saves the SAL01 object to the database based on the operation type.
        /// </summary>
        public Response Save()
        {
            string message = "";
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (_objSAL01.L01F01 == 0)
                {
                    db.Insert(_objSAL01);
                    message = "Sale deal entered succesfully";
                }
                else
                {
                    db.Update(_objSAL01);
                    message = "Sale deal updated succesfully";
                }
            }
            return OkResponse(message);
        }

        /// <summary>
        /// Removes a sale deal from the database by its ID.
        /// </summary>
        /// <param name="dealId">The ID of the sale deal to remove.</param>
        public Response RemoveSaleDeal(int dealId)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<SAL01>(dealId);
            }
            return OkResponse("sale deal removed");
        }

        /// <summary>
        /// Retrieves a sale deal from the database by its ID.
        /// </summary>
        /// <param name="dealId">The ID of the sale deal to retrieve.</param>
        /// <returns>The sale deal if found, otherwise null.</returns>
        public Response GetSaleDealById(int dealId)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                _objSAL01 = db.SingleById<SAL01>(dealId);
            }
            return OkResponse("", _objSAL01);
        }

        /// <summary>
        /// Retrieves all sale deals from the database.
        /// </summary>
        /// <returns>A list of all sale deals.</returns>
        public Response GetAllSaleDeals()
        {
            List<SAL01> lstSAL01;
            using (var db = _dbFactory.OpenDbConnection())
            {
                lstSAL01 = db.Select<SAL01>();
            }
            if (lstSAL01.Count > 0)
            {
                return OkResponse("data fetched ", lstSAL01);
            }
            return PreConditionFailedResponse("data not found");
        }

        /// <summary>
        /// Checks if a sale deal exists in the database by its ID.
        /// </summary>
        /// <param name="dealId">The ID of the sale deal to check.</param>
        /// <returns>True if the sale deal exists, otherwise false.</returns>
        public bool SaleDealExists(int dealId)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.SingleById<SAL01>(dealId) != null;
            }
        }

      

        #endregion
    }
}
