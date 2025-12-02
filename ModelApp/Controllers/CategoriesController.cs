using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelApp.Data;

namespace ModelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(ApplicationContextInMemory.Categories);
        }
    }
}
