using Microsoft.AspNetCore.Mvc;
using ModelApp.Data;
using ModelApp.Models;

namespace ModelApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    [HttpGet] // api/books
    public IActionResult GetAllBooks()
    {
        var list = ApplicationContextInMemory.Books;
        if (list.Count.Equals(0))
            return NoContent(); // 204
        return Ok(list);
    }

    [HttpGet("{id:int}")] // api/books/{id}
    public IActionResult GetOneBook([FromRoute(Name ="id")] int id)
    {
        var book = ApplicationContextInMemory
            .Books
            .FirstOrDefault(b => b.Id == id);
        if (book is null)
            return NotFound(); // 404
        return Ok(book); // 200
    }

    [HttpPost]
    public IActionResult CreateOneBook([FromBody] Book book)
    {
        try
        {
            int max = 0;
            foreach (Book b in ApplicationContextInMemory.Books)
            {
                if (b.Id > max)
                    max = b.Id;
            }

            book.Id = max + 1;
            ApplicationContextInMemory.Books.Add(book);
            return StatusCode(201, book); // 201
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // 400
        }
    }

    [HttpPut("{id:int}")] // api/books/{id}
    public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id,
        [FromBody] Book book)
    {
        if (book is null)
            return BadRequest(); // 400

        var existing = ApplicationContextInMemory
            .Books
            .FirstOrDefault(b => b.Id == id);
        if (existing is null)
            return NotFound(); // 404

        // If client supplied an Id in the body, ensure it matches the route id
        if (book.Id != 0 && book.Id != id)
            return BadRequest("Route id and body id must match."); // 400

        // Update allowed properties
        existing.Title = book.Title;
        existing.Price = book.Price;

        return Ok(existing); // 200
    }
}
