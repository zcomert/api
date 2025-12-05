using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Api;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet] // api/books
    public IActionResult GetAllBooks()
    {
        var books = _bookService.GetAllBooks();
        return Ok(books);
    }

    // api/books/{id}
    [HttpGet("{id:int}")]
    public IActionResult GetBookById(int id)
    { 
        var book = _bookService.GetBookById(id);
        return Ok(book);
    }

    // api/books/{id}
    [HttpPut("{id:int}")]
    public IActionResult UpdateBook([FromRoute(Name ="id")] int id,
        [FromBody] Book tobeUpdatedBook)
    {
        _bookService
            .UpdateBook(id, tobeUpdatedBook, true);
        return Ok(tobeUpdatedBook);
    }

    // api/books/{id}
    [HttpDelete("{id:int}")]
    public IActionResult DeleteBook([FromRoute(Name ="id")] int id)
    {
        _bookService.DeleteBook(id, true);
        return NoContent();
    }

    [HttpPost] // api/books
    public IActionResult CreateOneBook([FromBody] Book newBook)
    {
        _bookService.CreateBook(newBook);
        return CreatedAtAction(nameof(GetBookById),
            new { id = newBook.Id }, newBook);
    }

}
