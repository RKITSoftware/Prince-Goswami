using System.Collections.Generic;
using FinalDemo.Models;
using FinalDemo.Models.POCO;

namespace FinalDemo.BL.Interface.Service
{
    public interface IBLCTG01
    {
        ///<summary>
        ///Adds a new category to the inventory.
        ///</summary>
        ///<param name="category">The category object to be added.</param>
        Response AddCategory(string categoryName);

        ///<summary>
        ///Updates an existing category in the inventory.
        ///</summary>
        ///<param name="category">The updated category object.</param>
        ///<returns>A boolean indicating whether the update was successful or not.</returns>
        Response UpdateCategory(CTG01 category);

        ///<summary>
        ///Removes a category from the inventory.
        ///</summary>
        ///<param name="categoryId">The ID of the category to be removed.</param>
        ///<returns>A boolean indicating whether the removal was successful or not.</returns>
        Response RemoveCategory(int categoryId);

        ///<summary>
        ///Retrieves details of a specific category from the inventory.
        ///</summary>
        ///<param name="categoryId">The ID of the category to retrieve.</param>
        ///<returns>The category object corresponding to the provided ID.</returns>
        Response GetCategoryById(int categoryId);

        ///<summary>
        ///Retrieves all categories from the inventory.
        ///</summary>
        ///<returns>A list of all categories in the inventory.</returns>
        Response GetAllCategories();

        ///<summary>
        ///Checks if a category exists in the inventory.
        ///</summary>
        ///<param name="categoryId">The ID of the category to check.</param>
        ///<returns>A boolean indicating whether the category exists or not.</returns>
        bool CategoryExists(int categoryId);
    }
}
