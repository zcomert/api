using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Api;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("api/books")]
[Authorize(Roles ="Admin")]
public class BooksController : ControllerBase
{
    private readonly IServiceManager _manager;

    public BooksController(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpGet] // api/books
    public IActionResult GetAllBooks()
    {
        var books = _manager.BookService.GetAllBooks();
        return Ok(books);
    }

    // api/books/{id}
    [HttpGet("{id:int}")]
    public IActionResult GetBookById(int id)
    { 
        var book = _manager.BookService.GetBookById(id);
        return Ok(book);
    }

    // api/books/{id}
    [HttpPut("{id:int}")]
    public IActionResult UpdateBook([FromRoute(Name ="id")] int id,
        [FromBody] Book tobeUpdatedBook)
    {
        _manager
            .BookService
            .UpdateBook(id, tobeUpdatedBook, true);
        return NoContent(); // 204
    }

    // api/books/{id}
    [HttpDelete("{id:int}")]
    public IActionResult DeleteBook([FromRoute(Name ="id")] int id)
    {
        _manager.BookService.DeleteBook(id, true);
        return NoContent();
    }

    [HttpPost] // api/books
    public IActionResult CreateOneBook([FromBody] Book newBook)
    {
        _manager.BookService.CreateBook(newBook);
        return CreatedAtAction(nameof(GetBookById),
            new { id = newBook.Id }, newBook);
    }

}
