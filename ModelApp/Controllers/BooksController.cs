using Microsoft.AspNetCore.Mvc;
using ModelApp.Data;
using ModelApp.Models;

namespace ModelApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var list = ApplicationContextInMemory.Books;
            if (list.Count.Equals(0))
                return NoContent(); // 204
            return Ok(list);
        }
    }
}