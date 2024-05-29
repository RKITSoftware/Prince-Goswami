using System.Collections.Generic;
using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.Models.POCO;
using Microsoft.AspNetCore.Mvc;

namespace DealerManagementSystem.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CTG01Controller : ControllerBase
    {
        private readonly IBLCTG01 _categoryService;

        public CTG01Controller(IBLCTG01 categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("All")]
        [HttpGet]
        public IEnumerable<CTG01> AllCategorys()
        {
            return _categoryService.GetAllCategories();
        }

        // GET api/veh01/5
        [Route("{id}")]
        [HttpGet]
        public IActionResult CategoryById(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST api/veh01
        [Route("Add")]
        [HttpPost]
        public IActionResult AddCategory(CTG01 category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _categoryService.AddCategory(category);
            return StatusCode(StatusCodes.Status200OK);
        }

        // PUT api/veh01/5
        [HttpPut("Update")]
        public IActionResult UpdateCategory(int id, CTG01 category)
        {
            if (!ModelState.IsValid || id != category.T01F01)
            {
                return BadRequest();
            }
            _categoryService.UpdateCategory(category);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("id")]
        public IActionResult DeleteCategory(int id)
        {
            var existingCategory = _categoryService.GetCategoryById(id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            _categoryService.RemoveCategory(id);
            return Ok(existingCategory);
        }
    }
}
