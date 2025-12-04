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

}
