using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelApp.Data;
using ModelApp.Models;

namespace ModelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = ApplicationContextInMemory.Categories;
            if (categories.Count == 0)
            {
                return NotFound("Herhangi bir kategori bulunamadı.");
            }
            return Ok(ApplicationContextInMemory.Categories);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneCategory([FromRoute(Name = "id")] int id)
        {
            var category = ApplicationContextInMemory
                .Categories
                .FirstOrDefault(c => c.CategoryId == id);

            if (category is null)
            {
                return NotFound($"ID'si {id} olan kategori bulunamadı.");
            }

            return Ok(category);
        }

        [HttpPost] // api/categories
        public IActionResult CreateOneCategory([FromBody] Category category)
        {
            try
            {
                // dot per line 
                category.CategoryId = ApplicationContextInMemory
                    .Categories
                    .Max(c => c.CategoryId) + 1;

                ApplicationContextInMemory.Categories.Add(category);
                return CreatedAtAction(
                    nameof(GetOneCategory),
                    new { id = category.CategoryId },
                    category);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = "Kategori oluşturulurken bir hata oluştu.",
                    Error = ex.Message,
                    Time = DateTime.Now,
                    URI = HttpContext.Request.Path
                });
                
            }
            

        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneCategory([FromRoute(Name = "id")] int id,
            [FromBody] Category category)
        {
            var existingCategory = ApplicationContextInMemory
                .Categories
                .FirstOrDefault(c => c.CategoryId == id);
            if (existingCategory is null)
            {
                return NotFound($"ID'si {id} olan kategori bulunamadı.");
            }
            existingCategory.CategoryName = category.CategoryName;
            return NoContent(); // 204
        }

        [HttpDelete("{id:int}")] // api/categories/{id}
        public IActionResult DeleteOneCategory([FromRoute(Name = "id")] int id)
        {
            var category = ApplicationContextInMemory
                .Categories
                .FirstOrDefault(c => c.CategoryId == id);
            if (category is null)
            {
                return NotFound($"ID'si {id} olan kategori bulunamadı.");
            }
            ApplicationContextInMemory.Categories.Remove(category);
            return NoContent(); // 204
        }
    }
}
