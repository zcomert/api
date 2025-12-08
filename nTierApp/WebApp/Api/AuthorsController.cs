using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Api;

[ApiController]
[Route("api/authors")]
public class AuthorsController : ControllerBase
{
    private readonly IServiceManager _manager;

    public AuthorsController(IServiceManager manager)
    {
        _manager = manager;
    }

    // api/authors : Tüm yazarları getir
    [HttpGet]
    public IActionResult GetAuthors()
    {
        var authors = _manager
                .AuthorService
                .GetAllAuthors(trackChanges: false);
        return Ok(authors); // 200
    }

    // api/authors/{id} : Id'ye göre yazar getir
    [HttpGet("{id:int}")]
    public IActionResult GetAuthorById(int id)
    {
        var author = _manager
                .AuthorService
                .GetAuthorById(id, trackChanges: false);
        return Ok(author); // 200
    }

    // api/authors : Yeni yazar ekle
    [HttpPost]
    public IActionResult CreateAuthor([FromBody] Author author)
    {
        var createdAuthor = _manager
                .AuthorService
                .CreateAuthor(author);
        
        return CreatedAtAction(nameof(GetAuthorById),
            new { id = createdAuthor.AuthorId }, createdAuthor); // 201
    }

    // api/authors/{id} : Id'ye göre yazar sil
    [HttpDelete("{id:int}")]
    public IActionResult DeleteAuthor([FromRoute(Name ="id")] int id)
    {
        _manager
            .AuthorService
            .DeleteAuthor(id, trackChanges: false);
        
        return NoContent(); // 204
    }

    // api/authors/{id} : Id'ye göre yazar güncelle Body: Author
    [HttpPut("{id:int}")]
    public IActionResult UpdateAuthor([FromRoute(Name = "id")] int id,
        [FromBody] Author author)
    {
        _manager
            .AuthorService
            .UpdateAuthor(id, author, trackChanges: true);
        
        return NoContent(); // 204
    }
}
