using System.Collections.Generic;
using FinalDemo.BL.Interface.Service;
using FinalDemo.Models;
using FinalDemo.Models.POCO;
using Microsoft.AspNetCore.Mvc;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller for managing categories.
    /// </summary>
    [Route("api/Category")]
    [ApiController]
    public class CLCTG01Controller : ControllerBase
    {
        private readonly IBLCTG01 _categoryService;
        private Response _objResponse;
        /// <summary>
        /// Initializes a new instance of the <see cref="CLCTG01Controller"/> class.
        /// </summary>
        /// <param name="categoryService">The category service.</param>
        public CLCTG01Controller(IBLCTG01 categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>A collection of categories.</returns>
        [HttpGet("All")]
        public IActionResult AllCategorys()
        {
            _objResponse = _categoryService.GetAllCategories();
            return Ok(_objResponse);
        }

        /// <summary>
        /// Gets a category by ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>The category if found; otherwise, NotFound result.</returns>
        [HttpGet("{id}")]
        public IActionResult CategoryById(int id)
        {
            _objResponse = _categoryService.GetCategoryById(id);
            
            return Ok(_objResponse);
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="G01102">The name of the category to add.</param>
        /// <returns>Status 200 OK if the category is added successfully; otherwise, BadRequest.</returns>
        [HttpPost("Add")]
        public IActionResult AddCategory(string G01102)
        {
            _objResponse =_categoryService.AddCategory(G01102);
            return Ok(_objResponse);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="category">The updated category object.</param>
        /// <returns>Status 200 OK if the category is updated successfully; otherwise, BadRequest.</returns>
        [HttpPut("Update")]
        public IActionResult UpdateCategory(int id, CTG01 category)
        {
            _objResponse =  _categoryService.UpdateCategory(category);
            return Ok( _objResponse);
        }

        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>The deleted category if found and deleted; otherwise, NotFound result.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
           
            _objResponse = _categoryService.RemoveCategory(id);
            return Ok(_objResponse);
        }
    }
}
