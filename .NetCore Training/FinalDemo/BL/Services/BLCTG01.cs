using System.Collections.Generic;
using FinalDemo.BL.Interface.Service;
using FinalDemo.Models;
using FinalDemo.Models.POCO;
using static FinalDemo.BL.BLHelper;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace FinalDemo.BL.Services
{
    /// <summary>
    /// Service class providing operations related to categories (CTG01).
    /// </summary>
    public class BLCTG01 : IBLCTG01
    {
        #region Private Fields

        private readonly IDbConnectionFactory _dbFactory;
        private CTG01 _objCTG01;
        #endregion

        #region Constructor
        public BLCTG01(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;

        }
        #endregion

        #region Public methods
        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="CategoryName">The name of the category to add.</param>
        public Response AddCategory(string CategoryName)
        {
            if (CategoryName == null)
            {
                CTG01 objCTG01 = new CTG01() { G01F02 = CategoryName };
                using var db = _dbFactory.OpenDbConnection();
                db.Insert(objCTG01);
                return OkResponse();
            }
            return PreConditionFailedResponse("Category name should not be empty");
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="category">The category object containing updated information.</param>
        public Response UpdateCategory(CTG01 category)
        {
            if (CategoryExists(category.G01F01))
            {
                using var db = _dbFactory.OpenDbConnection();
                db.Update(category);
                return OkResponse("category updated");
            }
            return PreConditionFailedResponse("not Found");
        }


        /// <summary>
        /// Removes a category by its ID.
        /// </summary>
        /// <param name="categoryId">The ID of the category to remove.</param>
        public Response RemoveCategory(int categoryId)
        {
            if (CategoryExists(categoryId))
            {

                using var db = _dbFactory.OpenDbConnection();
                db.DeleteById<CTG01>(categoryId);
                return OkResponse("Category Removed");
            }
            return PreConditionFailedResponse("Category not found");
        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="categoryId">The ID of the category to retrieve.</param>
        /// <returns>The category object.</returns>
        public Response GetCategoryById(int categoryId)
        {
            using var db = _dbFactory.OpenDbConnection();
            _objCTG01 = db.SingleById<CTG01>(categoryId);
            db.Close();
            return OkResponse("Category found", _objCTG01);
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A list of all categories.</returns>
        public Response GetAllCategories()
        {
            List<CTG01> lstCTG01;
            using var db = _dbFactory.OpenDbConnection();
            lstCTG01 = db.Select<CTG01>();
            return OkResponse("data fetched", lstCTG01);
        }

        /// <summary>
        /// Checks if a category exists by its ID.
        /// </summary>
        /// <param name="G01F01">The ID of the category to check.</param>
        /// <returns>True if the category exists; otherwise, false.</returns>
        public bool CategoryExists(int G01F01)
        {
            using var db = _dbFactory.OpenDbConnection();
            return db.Exists<CTG01>(G01 => G01.G01F01 == G01F01);
        }
        #endregion
    }
}
