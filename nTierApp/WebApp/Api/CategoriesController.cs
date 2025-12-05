using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Api;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // api/categories
    [HttpGet]
    public IActionResult GetAllCategories()
    {
        var categories = _categoryService
               .GetAllCategories();
        return Ok(categories);
    }

    // api/categories/{id}
    [HttpGet("{id:int}")]
    public IActionResult GetCategoryById([FromRoute(Name = "id")] int id)
    {
        var category = _categoryService
            .GetCategoryById(id);
        return Ok(category);
    }

    // api/categories
    [HttpPost]
    public IActionResult CreateCategory([FromBody] Category category)
    {
        var createdCategory = _categoryService
            .CreateCategory(category);
        return CreatedAtAction(nameof(GetCategoryById),
            new { id = createdCategory.CategoryId }, createdCategory);
    }

    // api/categories/{id} : Body: Category
    [HttpPut("{id:int}")]
    public IActionResult UpdateCategory([FromRoute(Name = "id")] int id,
        [FromBody] Category category)
    {
        _categoryService.UpdateCategory(id, category);
        return NoContent(); // 204
    }

    // api/categories/{id}
    [HttpDelete("{id:int}")]
    public IActionResult DeleteCategory([FromRoute(Name = "id")] int id)
    {
        _categoryService.DeleteCategory(id);
        return NoContent(); // 204
    }
}
