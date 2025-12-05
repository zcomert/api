using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Api;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly IServiceManager _manager;

    public CategoriesController(IServiceManager manager)
    {
        _manager = manager;
    }

    // api/categories
    [HttpGet]
    public IActionResult GetAllCategories()
    {
        var categories = _manager
            .CategoryService
            .GetAllCategories();
        return Ok(categories);
    }

    // api/categories/{id}
    [HttpGet("{id:int}")]
    public IActionResult GetCategoryById([FromRoute(Name = "id")] int id)
    {
        var category = _manager
            .CategoryService    
            .GetCategoryById(id);
        return Ok(category);
    }

    // api/categories
    [HttpPost]
    public IActionResult CreateCategory([FromBody] Category category)
    {
        var createdCategory = _manager
            .CategoryService
            .CreateCategory(category);
        return CreatedAtAction(nameof(GetCategoryById),
            new { id = createdCategory.CategoryId }, createdCategory);
    }

    // api/categories/{id} : Body: Category
    [HttpPut("{id:int}")]
    public IActionResult UpdateCategory([FromRoute(Name = "id")] int id,
        [FromBody] Category category)
    {
        _manager
            .CategoryService
            .UpdateCategory(id, category);
        return NoContent(); // 204
    }

    // api/categories/{id}
    [HttpDelete("{id:int}")]
    public IActionResult DeleteCategory([FromRoute(Name = "id")] int id)
    {
        _manager
            .CategoryService
            .DeleteCategory(id);
        return NoContent(); // 204
    }
}
